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
    public interface IDBCVisita
    {
        string fn_Visita_Registrar(DTOVisitaCab dtoCab);
        List<DTORespuestaVisita> fn_Visita_Buscar(string pVendedor, string pCliente, string pAlmacen, string pFecha);
    }

    public class DBCVisita : IDBCVisita
    {
        SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["EtnaSap"].ConnectionString));

        public List<DTORespuestaVisita> fn_Visita_Buscar(string pVendedor, string pCliente, string pAlmacen, string pFecha)
        {
            List<DTORespuestaVisita> oLista = new List<DTORespuestaVisita>();
            DTORespuestaVisita dto = new DTORespuestaVisita();
            List<DTOVisitaCab> oListaCab = new List<DTOVisitaCab>();
            List<DTOVisitaDet> oListaDet = new List<DTOVisitaDet>();
            DTOVisitaCab dtoCab = new DTOVisitaCab();
            DTOVisitaDet dtoDet = null;

            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_VisitaCliente_Buscar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_Vendedor", SqlDbType.VarChar).Value = pVendedor;
                cm.Parameters.Add("@P_Cliente", SqlDbType.VarChar).Value = pCliente;
                cm.Parameters.Add("@P_Almacen", SqlDbType.VarChar).Value = pAlmacen;
                cm.Parameters.Add("@P_Fecha", SqlDbType.VarChar).Value = pFecha;
                cm.Connection = cn;

                cn.Open();
                dr = cm.ExecuteReader();

                int i = 0;

                while (dr.Read())
                {
                    if (i == 0)
                    {
                        dtoCab.IdVisita = dr["vic_nIdVisita"].ToString() == "" ? 0 : Convert.ToInt32(dr["vic_nIdVisita"]);
                        dtoCab.CodCliente = dr["vic_cCodCliente"].ToString();
                        dtoCab.CodVendedor = dr["vic_nVendedor"].ToString() == "" ? 0 : Convert.ToInt32(dr["vic_nVendedor"]);
                        dtoCab.CodAlmacen = dr["vic_cAlmacen"].ToString();
                        dtoCab.Comentario = dr["vic_cComentario"].ToString();
                        dtoCab.FecVisita = dr["vic_fFecReg"].ToString();
                    }
                    dtoDet = new DTOVisitaDet();
                    dtoDet.IdMotivo = Convert.ToInt32(dr["mvi_nIdMotivo"]);
                    dtoDet.Motivo = dr["Motivo"].ToString();
                    dtoDet.Estado = Convert.ToInt32(dr["Estado"]);

                    oListaDet.Add(dtoDet);

                    i++;
                }

                if (oListaDet.Count > 0)
                {
                    //dtoCab.ListDetalle = oListaDet;
                    //oListaCab.Add(dtoCab);

                    dto.Estado = "true";
                    dto.Mensaje = "Se encontro la visita";

                    var xLista = oListaDet.OrderBy(t=>t.IdMotivo).ToList<DTOVisitaDet>();

                    //dto.ListaVista = oListaCab;
                    dto.ListaDetalle = xLista;
                    dto.Visita = dtoCab;
                    oLista.Add(dto);
                }
                else
                {
                    dto.Estado = "false";
                    dto.Mensaje = "No se encontro la visita";
                    //dto.ListaVista = null;
                    dto.ListaDetalle = null;
                    dto.Visita = null;
                    oLista.Add(dto);
                }
            }
            catch(Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = ex.Message;
                //dto.ListaVista = null;
                dto.ListaDetalle = null;
                dto.Visita = null;
                oLista.Add(dto);

                dr.Close();
                cn.Close();                
            }

            return oLista;
        }

        public string fn_Visita_Registrar(DTOVisitaCab dtoCab)
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
                cm.CommandText = "[dbo].[PA_VEN_VisitaClienteCab_Registrar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_cCodCliente", SqlDbType.VarChar).Value = dtoCab.CodCliente;
                cm.Parameters.Add("@P_nVendedor", SqlDbType.Int).Value = dtoCab.CodVendedor;
                cm.Parameters.Add("@P_cAlmacen", SqlDbType.VarChar).Value = dtoCab.CodAlmacen;
                cm.Parameters.Add("@P_cComentario", SqlDbType.Text).Value = dtoCab.Comentario;
                cm.Parameters.Add("@P_nLatitud", SqlDbType.VarChar).Value = dtoCab.Latitud == null ? "0" : dtoCab.Latitud;
                cm.Parameters.Add("@P_nLongitud", SqlDbType.VarChar).Value = dtoCab.Longitud == null ? "0" : dtoCab.Longitud;
                cm.Parameters.Add("@P_cUserReg", SqlDbType.VarChar).Value = dtoCab.UserReg;
                cm.Parameters.Add("@P_nIdVisita", SqlDbType.Int).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                dtoCab.IdVisita = Convert.ToInt32(cm.Parameters["@P_nIdVisita"].Value);

                foreach (DTOVisitaDet item in dtoCab.ListDetalle)
                {
                    item.IdVisita = dtoCab.IdVisita;
                    item.UserReg = dtoCab.UserReg;

                    fn_VisitaDet_Registrar(item, oTransaction);
                }

                xResultado = "true|Se ha enviado el informe de visita N°" + dtoCab.IdVisita.ToString();

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

        bool fn_VisitaDet_Registrar(DTOVisitaDet dtoDet, SqlTransaction oTransaction)
        {
            bool xResult = false;
            SqlCommand cm = new SqlCommand();

            cm.Connection = cn;
            cm.Transaction = oTransaction;

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_VisitaClienteDet_Registrar]";
                cm.Parameters.Add("@P_nIdVisita", SqlDbType.Int).Value = dtoDet.IdVisita;
                cm.Parameters.Add("@P_nIdMotivo", SqlDbType.Int).Value = dtoDet.IdMotivo;
                cm.Parameters.Add("@P_cUserReg", SqlDbType.VarChar).Value = dtoDet.UserReg;

                cm.ExecuteNonQuery();

                xResult = true;

            }
            catch (Exception ex)
            {
                xResult = false;
                throw ex;
            }

            return xResult;
        }
    }
}
