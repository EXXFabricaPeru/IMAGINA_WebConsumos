using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Etna.DTOEntidades;
using EtnaEncryptor;
using System.Configuration;

namespace Etna.DBCAccesoDatos
{
    public interface IDBCBateria
    {
        List<DTORespuestaBateria> fn_BateriaPedido_Listar(string CodCliente, string CodMarca, string CodMoneda, string pFecha);
        List<DTORespuestaBateria> fn_ArticuloVenta_Listar(string CodMarca);
        List<DTORespuestaBateria> fn_ModelosPedido_Listar(string CodCliente, string CodMoneda, string pFecha);
    }
    
    public class DBCBateria : IDBCBateria
    {
        SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["EtnaSap"].ConnectionString));

        public List<DTORespuestaBateria> fn_BateriaPedido_Listar(string CodCliente, string CodMarca, string CodMoneda, string pFecha)
        {
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            List<DTOBateria> oListaBat = new List<DTOBateria>();
            DTOBateria dto = new DTOBateria();

            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();
            DTORespuestaBateria dtoRes = new DTORespuestaBateria();

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_ListaPrecioXClienteXMarca_Buscar]";
                cm.Parameters.Add("@P_CodCliente", SqlDbType.VarChar).Value = CodCliente;
                cm.Parameters.Add("@P_CodMarca", SqlDbType.Int).Value = CodMarca;
                cm.Parameters.Add("@P_CodMoneda", SqlDbType.VarChar).Value = CodMoneda;
                cm.Parameters.Add("@P_Fecha", SqlDbType.VarChar).Value = pFecha;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOBateria();

                    dto.CodAnt = dr["CodAnterior"].ToString().Trim();
                    dto.IdProducto = Convert.ToInt32(dr["CodArticulo"]);
                    dto.Descripcion = dr["DescArticulo"].ToString().Trim();
                    dto.UnidadMed = dr["Unidad"].ToString();
                    dto.PrecUnit = Convert.ToDecimal(dr["PrecioEstructural"]).ToString("N2");
                    dto.Almacen = dr["Almacen"].ToString();
                    dto.Stock = Convert.ToInt32(dr["Stock"]);
                    dto.Imagen = dr["Imagen"].ToString();
                    //dto.PrecioMensual = Convert.ToDecimal(dr["PrecioMensual"]).ToString("N2");
                    //dto.PrecioDiaLocura = Convert.ToDecimal(dr["DiaLocura"]).ToString("N2");
                    //dto.PrecioEspecial = Convert.ToDecimal(dr["Especial"]).ToString("N2");
                    //dto.FecIniPrecMen = dr["FecDesdePrecMensual"].ToString();
                    //dto.FecFinPrecMen = dr["FecHastaPrecMensual"].ToString();
                    //dto.FecIniPrecDiaLoc = dr["FecDesdePrecDiaLoc"].ToString();
                    //dto.FecFinPrecDiaLoc = dr["FecHastaPrecDiaLoc"].ToString();
                    //dto.FecIniPrecEsp = dr["FecDesdePrecEspecial"].ToString();
                    //dto.FecFinPrecEsp = dr["FecHastaPrecEspecial"].ToString();
                    oListaBat.Add(dto);
                }

                if (oListaBat.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaBateria = oListaBat;

                    oLista.Add(dtoRes);
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaBateria = null;

                    oLista.Add(dtoRes);
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaBateria = null;

                oLista.Add(dtoRes);
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return oLista;
        }

        public List<DTORespuestaBateria> fn_ModelosPedido_Listar(string CodCliente, string CodMoneda, string pFecha)
        {
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            List<DTOBateria> oListaBat = new List<DTOBateria>();
            DTOBateria dto = new DTOBateria();

            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();
            DTORespuestaBateria dtoRes = new DTORespuestaBateria();

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_ListaPrecioXCliente_Buscar]";
                cm.Parameters.Add("@P_CodCliente", SqlDbType.VarChar).Value = CodCliente;
                cm.Parameters.Add("@P_CodMoneda", SqlDbType.VarChar).Value = CodMoneda;
                cm.Parameters.Add("@P_Fecha", SqlDbType.VarChar).Value = pFecha;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOBateria();

                    dto.IdProducto = Convert.ToInt32(dr["CodArticulo"]);
                    dto.UnidadMed = dr["Unidad"].ToString();
                    dto.PrecUnit = Convert.ToDecimal(dr["PrecioEstructural"]).ToString("N2");
                    dto.Almacen = dr["Almacen"].ToString();
                    dto.Stock = Convert.ToInt32(dr["Stock"]);
                    dto.PrecioPromo1 = Convert.ToDecimal(dr["PrecioSegmentacion"]).ToString("N2");
                    dto.PrecioPromo2 = Convert.ToDecimal(dr["PrecioMensual"]).ToString("N2");
                    dto.PrecioPromo3 = Convert.ToDecimal(dr["DiaLocura"]).ToString("N2");
                    dto.PrecioPromo4 = Convert.ToDecimal(dr["Especial"]).ToString("N2");
                    dto.FecIniPromo1 = dr["FecDesdePrecSegmenta"].ToString();
                    dto.FecFinPromo1 = dr["FecHastaPrecSegmenta"].ToString();
                    dto.FecIniPromo2 = dr["FecDesdePrecMensual"].ToString();
                    dto.FecFinPromo2 = dr["FecHastaPrecMensual"].ToString();
                    dto.FecIniPromo3 = dr["FecDesdePrecDiaLoc"].ToString();
                    dto.FecFinPromo3 = dr["FecHastaPrecDiaLoc"].ToString();
                    dto.FecIniPromo4 = dr["FecDesdePrecEspecial"].ToString();
                    dto.FecFinPromo4 = dr["FecHastaPrecEspecial"].ToString();
                    oListaBat.Add(dto);
                }

                if (oListaBat.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaBateria = oListaBat;

                    oLista.Add(dtoRes);
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaBateria = null;

                    oLista.Add(dtoRes);
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaBateria = null;

                oLista.Add(dtoRes);
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return oLista;
        }

        public List<DTORespuestaBateria> fn_ArticuloVenta_Listar(string CodMarca)
        {
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            List<DTOBateria> oListaBat = new List<DTOBateria>();
            DTOBateria dto = new DTOBateria();

            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();
            DTORespuestaBateria dtoRes = new DTORespuestaBateria();

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_MAE_ArticulosVenta_Listar]";
                cm.Parameters.Add("@P_Marca", SqlDbType.VarChar).Value = CodMarca;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOBateria();

                    dto.IdProducto = Convert.ToInt32(dr["CodArticulo"]);
                    dto.Descripcion = dr["DescArticulo"].ToString().Trim();
                    dto.UnidadMed = dr["Unidad"].ToString();
                    dto.Stock = Convert.ToInt32(dr["Stock"]);
                    dto.Imagen = dr["Imagen"].ToString();
                    oListaBat.Add(dto);
                }

                if (oListaBat.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaBateria = oListaBat;

                    oLista.Add(dtoRes);
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaBateria = null;

                    oLista.Add(dtoRes);
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaBateria = null;

                oLista.Add(dtoRes);
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return oLista;
        }
    }
}
