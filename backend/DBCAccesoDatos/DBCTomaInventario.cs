using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using System.Data;
using System.Data.SqlClient;
using EtnaEncryptor;
using System.Configuration;

namespace Etna.DBCAccesoDatos
{
    public interface IDBCTomaInventario {
        List<DTORespuestaTomaInventario> fn_TomaInventario_Buscar(string pCliente, string pAlmacen, string pArticulo, string pFecha);
        string fn_TomaInventario_Registrar(DTOTomaInventarioCab TomaInventario);
    }

    public class DBCTomaInventario:IDBCTomaInventario
    {
        SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["EtnaSap"].ConnectionString));

        public List<DTORespuestaTomaInventario> fn_TomaInventario_Buscar(string pCliente, string pAlmacen, string pArticulo, string pFecha)
        {
            DTORespuestaTomaInventario oToRespuesta = new DTORespuestaTomaInventario();
            List<DTORespuestaTomaInventario> oListaRespuesta = new List<DTORespuestaTomaInventario>();
            DTOTomaInventarioCab oToTomaCab = new DTOTomaInventarioCab();
            List<DTOTomaInventarioCab> oListaTomaCab = new List<DTOTomaInventarioCab>();
            DTOTomaInventarioDet oToTomaDet = new DTOTomaInventarioDet();
            List<DTOTomaInventarioDet> oListaTomaDet = new List<DTOTomaInventarioDet>();

            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            int i = 0;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_TomaInventario_Buscar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_Cliente", SqlDbType.VarChar).Value = pCliente;
                cm.Parameters.Add("@P_Almacen", SqlDbType.VarChar).Value = pAlmacen;
                cm.Parameters.Add("@P_Articulo", SqlDbType.VarChar).Value = pArticulo;
                cm.Parameters.Add("@P_Fecha", SqlDbType.VarChar).Value = pFecha;
                cm.Connection = cn;

                cn.Open();
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    if (i == 0)
                    {
                        oToTomaCab.CodCliente = dr.GetString(dr.GetOrdinal("CodigoCliente"));
                        oToTomaCab.CodAlmacen = dr.GetString(dr.GetOrdinal("CodigoAlmacen"));
                        oToTomaCab.FechaToma = dr.GetString(dr.GetOrdinal("Fecha"));
                        oToTomaCab.IdToma = dr.GetInt32(dr.GetOrdinal("IdToma"));
                        oToTomaCab.CodVendedor = dr.GetInt32(dr.GetOrdinal("Vendedor"));
                        oToTomaCab.UserReg = dr.GetString(dr.GetOrdinal("UserReg"));
                    }
                    oToTomaDet = new DTOTomaInventarioDet();
                    oToTomaDet.CodArticulo = dr.GetString(dr.GetOrdinal("CodigoArticulo"));
                    oToTomaDet.FecUltCompra = dr.GetString(dr.GetOrdinal("FechaUltimaCompra"));
                    oToTomaDet.CantComprada = dr.GetInt32(dr.GetOrdinal("CantidadComprada"));
                    oToTomaDet.CantRegistrada = dr.GetInt32(dr.GetOrdinal("CantidadRegistrada"));
                    oToTomaDet.CantActual = dr.GetInt32(dr.GetOrdinal("CantidadActual"));
                    oToTomaDet.FecUltRegistro = dr.GetString(dr.GetOrdinal("FechaUltimoRegistro"));
                    oListaTomaDet.Add(oToTomaDet);
                    i++;
                }

                if (oListaTomaDet.Count > 0)
                {
                    oToRespuesta.Mensaje = "Se encontro la toma de inventario.";
                    oToRespuesta.Estado = "True";
                    oToRespuesta.TomaInventario = oToTomaCab;
                    oToRespuesta.ListaDetalle = oListaTomaDet;
                    oListaRespuesta.Add(oToRespuesta);
                }
                else 
                {
                    oToRespuesta.Mensaje = "No se encontro la toma de inventario.";
                    oToRespuesta.Estado = "False";
                    oToRespuesta.TomaInventario = null;
                    oToRespuesta.ListaDetalle = null;
                    oListaRespuesta.Add(oToRespuesta);
                }

            }
            catch (Exception ex)
            {
                oToRespuesta.Mensaje = ex.Message;
                oToRespuesta.Estado = "False";
                oToRespuesta.TomaInventario = null;
                oToRespuesta.ListaDetalle = null;
                oListaRespuesta.Add(oToRespuesta);
            }
            finally 
            {
                if (dr != null) 
                {
                    dr.Close();
                }           
                cn.Close();
            }

            return oListaRespuesta;
        }

        public string fn_TomaInventario_Registrar(DTOTomaInventarioCab oTomaInventario)
        {
            string xResultado = "";
            SqlCommand cm = new SqlCommand();
            SqlTransaction oTransaction;
            cn.Open();

            oTransaction = cn.BeginTransaction();
            cm.Connection = cn;
            cm.Transaction = oTransaction;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_TomaInventarioCab_Registrar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_cCodCliente", SqlDbType.VarChar).Value = oTomaInventario.CodCliente;
                cm.Parameters.Add("@P_cAlmacen", SqlDbType.VarChar).Value = oTomaInventario.CodAlmacen;
                cm.Parameters.Add("@P_nVendedor", SqlDbType.Int).Value = oTomaInventario.CodVendedor;
                cm.Parameters.Add("@P_nLatitud", SqlDbType.VarChar).Value = oTomaInventario.Latitud == null ? "0" : oTomaInventario.Latitud;
                cm.Parameters.Add("@P_nLongitud", SqlDbType.VarChar).Value = oTomaInventario.Longitud == null ? "0" : oTomaInventario.Longitud;
                cm.Parameters.Add("@P_cUserReg", SqlDbType.VarChar).Value = oTomaInventario.UserReg;
                cm.Parameters.Add("@P_nIdToma", SqlDbType.Int).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                oTomaInventario.IdToma = Convert.ToInt32(cm.Parameters["@P_nIdToma"].Value);

                foreach(DTOTomaInventarioDet oDetalle in oTomaInventario.ListDetalle)
                {
                    oDetalle.IdToma = oTomaInventario.IdToma;
                    oDetalle.UserReg = oTomaInventario.UserReg;
                    fn_TomaInventarioDetalle_Registrar(oDetalle, oTransaction);
                }

                xResultado = "true|Se ha enviado la toma de inventario N°" + oTomaInventario.IdToma.ToString();

                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                oTransaction.Rollback();
                xResultado = "false|" + ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return xResultado;
        }

        private bool fn_TomaInventarioDetalle_Registrar(DTOTomaInventarioDet oTomaInventarioDet, SqlTransaction oTransaction)
        {
            bool xResultado = false;
            SqlCommand cm = new SqlCommand();

            cm.Connection = cn;
            cm.Transaction = oTransaction;

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_TomaInventarioDet_Registrar]";
                cm.Parameters.Add("@P_nIdToma", SqlDbType.Int).Value = oTomaInventarioDet.IdToma;
                cm.Parameters.Add("@P_cCodArticulo", SqlDbType.VarChar).Value = oTomaInventarioDet.CodArticulo;
                cm.Parameters.Add("@P_fFechaUltCompra", SqlDbType.VarChar).Value = oTomaInventarioDet.FecUltCompra;
                cm.Parameters.Add("@P_dCantComprada", SqlDbType.Int).Value = oTomaInventarioDet.CantComprada;
                cm.Parameters.Add("@P_dCantActual", SqlDbType.Int).Value = oTomaInventarioDet.CantActual;
                cm.Parameters.Add("@P_cUserReg", SqlDbType.VarChar).Value = oTomaInventarioDet.UserReg;

                cm.ExecuteNonQuery();

                xResultado = true;
            }
            catch (Exception ex)
            {
                xResultado = false;
                throw ex;
            }

            return xResultado;
        }
    }
}
