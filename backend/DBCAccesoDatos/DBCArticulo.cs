using DTOEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace DBCAccesoDatos
{
    public interface IDBCArticulo
    {
        DTORespuestaArticulo fn_Articulo_Listar(string valor, string almacen, string listprice, string moneda, string flagStock, csCompany xCompany);
        DTORespuestaArticulo fn_ObtenerPrecio(string listPrice, string moneda, string undMed, string codigoArt, csCompany xCompany);
        DTORespuestaArticulo fn_ObtenerStockUndMed(string almacen, string undMed, string codigoArt, csCompany xCompany);
        DTORespuestaArticulo fn_StockAlmacen(string codigoArt, string sucursal, csCompany xCompany);
    }

    public class DBCArticulo : IDBCArticulo
    {
        public DTORespuestaArticulo fn_Articulo_Listar(string valor, string almacen, string listprice, string moneda, string flagStock, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            List<DTOArticulo> oListaArt = new List<DTOArticulo>();
            DTOArticulo dto = new DTOArticulo();

            DTORespuestaArticulo dtoRes = new DTORespuestaArticulo();

            try
            {
                if (valor == null) valor = "";
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_ArticuloVenta_Listar\" ('{valor}','{almacen}',{listprice},'{moneda}',{flagStock})";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOArticulo();

                    dto.CodArticulo = dr["ItemCode"].ToString().Trim();
                    dto.Descripcion = dr["ItemName"].ToString().Trim();
                    dto.PrecioUnit = Convert.ToDouble(dr["PriceUnit"]);
                    dto.CodAlmacen = dr["WhsCode"].ToString();
                    dto.CantActual = Convert.ToDouble(dr["OnHand"]);
                    dto.StockDisponible = Convert.ToDouble(dr["Disponible"]);
                    dto.CantSolicitada = Convert.ToDouble(dr["Solicitado"]);
                    dto.CodUndMed = Convert.ToInt32(dr["UomEntry"]);
                    dto.UnidadMedida = dr["SalUnitMsr"].ToString();
                    dto.CentroCosto = dr["U_EXK_CENCOSTO"].ToString();
                    oListaArt.Add(dto);
                }

                if (oListaArt.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaArticulo = oListaArt;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ha encontrado ningún artículo con los filtros ingresados";
                    dtoRes.ListaArticulo = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaArticulo = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoRes;
        }

        public DTORespuestaArticulo fn_ObtenerPrecio(string listPrice, string moneda, string undMed, string codigoArt, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            List<DTOArticulo> oListaArt = new List<DTOArticulo>();
            DTOArticulo dto = new DTOArticulo();

            DTORespuestaArticulo dtoRes = new DTORespuestaArticulo();

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_PrecioArticuloUnd_Buscar\" ('{codigoArt}','{undMed}','{listPrice}','{moneda}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOArticulo();

                    dto.CodArticulo = dr["ItemCode"].ToString().Trim();
                    dto.PrecioUnit = Convert.ToDouble(dr["Price"]);
                    oListaArt.Add(dto);
                }
                dr.Close();

                if (oListaArt.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaArticulo = oListaArt;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaArticulo = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaArticulo = null;
            }
            finally
            {
                cn.Close();
            }

            return dtoRes;
        }
        
        public DTORespuestaArticulo fn_ObtenerStockUndMed(string almacen, string undMed, string codigoArt, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            List<DTOArticulo> oListaArt = new List<DTOArticulo>();
            DTOArticulo dto = new DTOArticulo();

            DTORespuestaArticulo dtoRes = new DTORespuestaArticulo();

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_StockArticuloUnd_Buscar\" ('{codigoArt}','{undMed}','{almacen}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOArticulo();

                    dto.CantActual = Convert.ToDouble(dr["Stock"]);
                    oListaArt.Add(dto);
                }
                dr.Close();

                if (oListaArt.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido el stock";
                    dtoRes.ListaArticulo = oListaArt;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaArticulo = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaArticulo = null;
            }
            finally
            {
                cn.Close();
            }

            return dtoRes;
        }
        
        public DTORespuestaArticulo fn_StockAlmacen(string codigoArt, string sucursal, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            List<DTOArticulo> oListaArt = new List<DTOArticulo>();
            DTOArticulo dto = new DTOArticulo();

            DTORespuestaArticulo dtoRes = new DTORespuestaArticulo();

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_StockArticulos_Listar\" ('{codigoArt}','{sucursal}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOArticulo();

                    dto.Descripcion = dr["ItemName"].ToString().Trim();
                    dto.CodAlmacen = dr["WhsCode"].ToString().Trim();
                    dto.CantActual = Convert.ToDouble(dr["OnHand"]);
                    dto.StockDisponible = Convert.ToDouble(dr["Disponible"]);
                    dto.StockMin = Convert.ToDouble(dr["MinStock"]);
                    dto.CantComprom = Convert.ToDouble(dr["IsCommited"]);
                    dto.CantSolicitada = Convert.ToDouble(dr["OnOrder"]);
                    oListaArt.Add(dto);
                }

                if (oListaArt.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se ha recibido la lista de precio";
                    dtoRes.ListaArticulo = oListaArt;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun articulo con los filtros ingresados";
                    dtoRes.ListaArticulo = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaArticulo = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoRes;
        }

    }
}
