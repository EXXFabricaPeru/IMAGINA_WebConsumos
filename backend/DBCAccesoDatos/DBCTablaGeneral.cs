using DTOEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace DBCAccesoDatos
{
    public interface IDBCTablaGeneral
    {
        DTORespuestaTablaGeneral fn_AlmacenVenta_Listar(string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_AlmacenEtapa_Listar(string sucursal, string etapa, csCompany xCompany);
        DTORespuestaTablaGeneral fn_AlmacenCliente_Listar(string pCodigo, csCompany xCompany);
        DTORespuestaTablaGeneral CondicionPago_Listar(string pCodigo, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Moneda_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_TipoImpuesto_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Dimencion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_SubDimencion_Listar(string pCodigo, string pSucursal, string pProyecto, csCompany xCompany);
        DTORespuestaTablaGeneral fn_TipoOPeracion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Proyecto_Listar(string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_SeriesDocumento_Listar(string tipo, string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_UnidadMedida_Listar(string codArticulo, string moneda, string listaPrecio, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Departamento_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Provincia_Listar(string codDepartamento, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Distrito_Listar(string codProvincia, csCompany xCompany);
        DTORespuestaTablaGeneral fn_GrupoCliente_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Sucursales_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_ListaPrecio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Vendedores_Listar(string xCodVendedor, csCompany xCompany);
        DTORespuestaTablaGeneral fn_GiroNegocio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Zona_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_MedioEnvio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_PartidaPresupuestal_Listar(string proyecto, string etapa, string idEmpleado,string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_MotivoAnulacion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_JefeAlmacen_Listar(csCompany xCompany);
    }

    public class DBCTablaGeneral : IDBCTablaGeneral
    {
        public DTORespuestaTablaGeneral fn_AlmacenEtapa_Listar(string pCodigo, string etapa, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoAlmacen = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaAlmacen = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen = new DTOTablaGeneral();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_Almacen_Etapa_Listar\" ('{pCodigo}','{etapa}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["WhsCode"].ToString();
                    oAlmacen.Descripcion = dr["WhsName"].ToString();
                    oListaAlmacen.Add(oAlmacen);
                }

                dtoAlmacen.Estado = "True";
                dtoAlmacen.Mensaje = "Se recibio la lista de almacenes del cliente";
                dtoAlmacen.ListaTablaGeneral = oListaAlmacen;
            }
            catch (Exception ex)
            {
                dtoAlmacen.Estado = "False";
                dtoAlmacen.Mensaje = "Se produjo un error: " + ex.Message;
                dtoAlmacen.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoAlmacen;
        }public DTORespuestaTablaGeneral fn_AlmacenCliente_Listar(string pCodigo, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoAlmacen = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaAlmacen = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen = new DTOTablaGeneral();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_AlmacenCliente_Listar\" ('{pCodigo}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["Address"].ToString();
                    oAlmacen.Descripcion = dr["Street"].ToString();
                    oListaAlmacen.Add(oAlmacen);
                }

                dtoAlmacen.Estado = "True";
                dtoAlmacen.Mensaje = "Se recibio la lista de almacenes del cliente";
                dtoAlmacen.ListaTablaGeneral = oListaAlmacen;
            }
            catch (Exception ex)
            {
                dtoAlmacen.Estado = "False";
                dtoAlmacen.Mensaje = "Se produjo un error: " + ex.Message;
                dtoAlmacen.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoAlmacen;
        }

        public DTORespuestaTablaGeneral CondicionPago_Listar(string pCodigo, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoCondicion = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaCondicion = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen = new DTOTablaGeneral();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_CondicionPago_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["Codigo"].ToString();
                    oAlmacen.Descripcion = dr["Descripcion"].ToString();
                    oListaCondicion.Add(oAlmacen);
                }

                dtoCondicion.Estado = "True";
                dtoCondicion.Mensaje = "Se recibio la lista de condiciones de pago";
                dtoCondicion.ListaTablaGeneral = oListaCondicion;
            }
            catch (Exception ex)
            {
                dtoCondicion.Estado = "False";
                dtoCondicion.Mensaje = "Se produjo un error: " + ex.Message;
                dtoCondicion.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoCondicion;
        }

        public DTORespuestaTablaGeneral fn_AlmacenVenta_Listar(string sucursal,csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoAlmacen = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaAlmacen = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_Almacen_Listar\" ('{sucursal}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["WhsCode"].ToString();
                    oAlmacen.Descripcion = dr["WhsName"].ToString();
                    oListaAlmacen.Add(oAlmacen);
                }
                dr.Close();
                dtoAlmacen.Estado = "True";
                dtoAlmacen.Mensaje = "Se recibio la lista de almacenes del venta";
                dtoAlmacen.ListaTablaGeneral = oListaAlmacen;
            }
            catch (Exception ex)
            {
                dtoAlmacen.Estado = "False";
                dtoAlmacen.Mensaje = "Se produjo un error: " + ex.Message;
                dtoAlmacen.ListaTablaGeneral = null;
            }
            finally
            {                
                cn.Close();
            }

            return dtoAlmacen;
        }

        public DTORespuestaTablaGeneral fn_Moneda_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoMoneda = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaMoneda = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen = new DTOTablaGeneral();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_Moneda_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["Codigo"].ToString();
                    oAlmacen.Descripcion = dr["Descripcion"].ToString();
                    oListaMoneda.Add(oAlmacen);
                }

                dtoMoneda.Estado = "True";
                dtoMoneda.Mensaje = "Se recibio la lista de monedas del cliente";
                dtoMoneda.ListaTablaGeneral = oListaMoneda;
            }
            catch (Exception ex)
            {
                dtoMoneda.Estado = "False";
                dtoMoneda.Mensaje = "Se produjo un error: " + ex.Message;
                dtoMoneda.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoMoneda;
        }

        public DTORespuestaTablaGeneral fn_TipoImpuesto_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoImpuesto = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaImpuesto = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_TipoImpuesto_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["Code"].ToString();
                    oAlmacen.Descripcion = dr["Name"].ToString();
                    oListaImpuesto.Add(oAlmacen);
                }

                dtoImpuesto.Estado = "True";
                dtoImpuesto.Mensaje = "Se recibio la lista de tipo de impuesto";
                dtoImpuesto.ListaTablaGeneral = oListaImpuesto;
            }
            catch (Exception ex)
            {
                dtoImpuesto.Estado = "False";
                dtoImpuesto.Mensaje = "Se produjo un error: " + ex.Message;
                dtoImpuesto.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoImpuesto;
        }

        public DTORespuestaTablaGeneral fn_Dimencion_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoDim = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaDim = new List<DTOTablaGeneral>();
            DTOTablaGeneral oDimencion;
            
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_Dimenciones_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oDimencion = new DTOTablaGeneral();
                    oDimencion.Codigo = dr["DimCode"].ToString();
                    oDimencion.Descripcion = dr["DimName"].ToString();
                    oDimencion.Valor01 = dr["DimDesc"].ToString();
                    oListaDim.Add(oDimencion);
                }

                dtoDim.Estado = "True";
                dtoDim.Mensaje = "Se recibio la lista de dimenciones";
                dtoDim.ListaTablaGeneral = oListaDim;
            }
            catch (Exception ex)
            {
                dtoDim.Estado = "False";
                dtoDim.Mensaje = "Se produjo un error: " + ex.Message;
                dtoDim.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoDim;
        }

        public DTORespuestaTablaGeneral fn_SubDimencion_Listar(string pCodigo, string pSucursal, string pProyecto, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoSubDim = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaSubDim = new List<DTOTablaGeneral>();
            DTOTablaGeneral oAlmacen = new DTOTablaGeneral();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_SubDimenciones_Listar\" ('{pCodigo}','{pProyecto}','{pSucursal}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oAlmacen = new DTOTablaGeneral();
                    oAlmacen.Codigo = dr["PrcCode"].ToString();
                    oAlmacen.Descripcion = dr["PrcName"].ToString();
                    oListaSubDim.Add(oAlmacen);
                }

                dtoSubDim.Estado = "True";
                dtoSubDim.Mensaje = "Se recibio la lista de subdimenciones";
                dtoSubDim.ListaTablaGeneral = oListaSubDim;
            }
            catch (Exception ex)
            {
                dtoSubDim.Estado = "False";
                dtoSubDim.Mensaje = "Se produjo un error: " + ex.Message;
                dtoSubDim.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoSubDim;
        }
        
        public DTORespuestaTablaGeneral fn_TipoOPeracion_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoTipoOper = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaTipoOper = new List<DTOTablaGeneral>();
            DTOTablaGeneral oDimencion;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_TipoOperacion_listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oDimencion = new DTOTablaGeneral();
                    oDimencion.Codigo = dr["Code"].ToString();
                    oDimencion.Descripcion = dr["Name"].ToString();
                    oListaTipoOper.Add(oDimencion);
                }

                dtoTipoOper.Estado = "True";
                dtoTipoOper.Mensaje = "Se recibio la lista de Tipo de operación";
                dtoTipoOper.ListaTablaGeneral = oListaTipoOper;
            }
            catch (Exception ex)
            {
                dtoTipoOper.Estado = "False";
                dtoTipoOper.Mensaje = "Se produjo un error: " + ex.Message;
                dtoTipoOper.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoTipoOper;
        }

        public DTORespuestaTablaGeneral fn_Proyecto_Listar(string sucursal, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoTipoOper = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaTipoOper = new List<DTOTablaGeneral>();
            DTOTablaGeneral oDimencion;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_Proyectos_Listar\" ({sucursal})";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oDimencion = new DTOTablaGeneral();
                    oDimencion.Codigo = dr["PrjCode"].ToString();
                    oDimencion.Descripcion = dr["PrjName"].ToString();
                    oListaTipoOper.Add(oDimencion);
                }

                dtoTipoOper.Estado = "True";
                dtoTipoOper.Mensaje = "Se recibio la lista de proyectos";
                dtoTipoOper.ListaTablaGeneral = oListaTipoOper;
            }
            catch (Exception ex)
            {
                dtoTipoOper.Estado = "False";
                dtoTipoOper.Mensaje = "Se produjo un error: " + ex.Message;
                dtoTipoOper.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoTipoOper;
        }
        
        public DTORespuestaTablaGeneral fn_SeriesDocumento_Listar(string tipo, string sucursal, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoSeries = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaSeries = new List<DTOTablaGeneral>();
            DTOTablaGeneral oSeries;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_SerieDocumento_Listar\" ('{tipo}','{sucursal}')";
                //cm.Parameters.Add("@P_Tipo", OdbcType.VarChar).Value = tipo;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oSeries = new DTOTablaGeneral();
                    oSeries.Codigo = dr["Series"].ToString();
                    oSeries.Descripcion = dr["SeriesName"].ToString();
                    oListaSeries.Add(oSeries);
                }

                dtoSeries.Estado = "True";
                dtoSeries.Mensaje = "Se recibio la lista de series";
                dtoSeries.ListaTablaGeneral = oListaSeries;
            }
            catch (Exception ex)
            {
                dtoSeries.Estado = "False";
                dtoSeries.Mensaje = "Se produjo un error: " + ex.Message;
                dtoSeries.ListaTablaGeneral = null;
            }
            finally
            {
                //dr.Close();
                cn.Close();
            }

            return dtoSeries;
        }
        
        public DTORespuestaTablaGeneral fn_UnidadMedida_Listar(string codArticulo, string moneda, string listaPrecio, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoUnidades = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaUnidades = new List<DTOTablaGeneral>();
            DTOTablaGeneral oUnidades;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                listaPrecio = listaPrecio == null ? "1" : listaPrecio;
                moneda = moneda == null ? "USD" : moneda;
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_UnidadMedidaArticulo_Listar\" ('{codArticulo}',{listaPrecio},'{moneda}')";
                //cm.Parameters.Add("@P_CodArticulo", OdbcType.VarChar).Value = codArticulo;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oUnidades = new DTOTablaGeneral();
                    oUnidades.Codigo = dr["UomEntry"].ToString(); 
                    oUnidades.Descripcion = dr["UomName"].ToString();
                    oUnidades.Valor01 = dr["UomCode"].ToString();
                    oListaUnidades.Add(oUnidades);
                }

                dtoUnidades.Estado = "True";
                dtoUnidades.Mensaje = "Se recibio la lista de proyectos";
                dtoUnidades.ListaTablaGeneral = oListaUnidades;
            }
            catch (Exception ex)
            {
                dtoUnidades.Estado = "False";
                dtoUnidades.Mensaje = "Se produjo un error: " + ex.Message;
                dtoUnidades.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoUnidades;
        }

        public DTORespuestaTablaGeneral fn_Departamento_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoTipoOper = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaTipoOper = new List<DTOTablaGeneral>();
            DTOTablaGeneral oDimencion;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_Departamento_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oDimencion = new DTOTablaGeneral();
                    oDimencion.Codigo = dr["Code"].ToString();
                    oDimencion.Descripcion = dr["Name"].ToString();
                    oListaTipoOper.Add(oDimencion);
                }

                dtoTipoOper.Estado = "True";
                dtoTipoOper.Mensaje = "Se recibio la lista de departamento";
                dtoTipoOper.ListaTablaGeneral = oListaTipoOper;
            }
            catch (Exception ex)
            {
                dtoTipoOper.Estado = "False";
                dtoTipoOper.Mensaje = "Se produjo un error: " + ex.Message;
                dtoTipoOper.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoTipoOper;
        }
        
        public DTORespuestaTablaGeneral fn_Provincia_Listar(string codDepartamento, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoUnidades = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaUnidades = new List<DTOTablaGeneral>();
            DTOTablaGeneral oUnidades;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_Provincia_Listar\" ('{codDepartamento}')";
                //cm.Parameters.Add("@P_CodDepa", OdbcType.VarChar).Value = codDepartamento;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oUnidades = new DTOTablaGeneral();
                    oUnidades.Codigo = dr["Code"].ToString();
                    oUnidades.Descripcion = dr["Name"].ToString();
                    oListaUnidades.Add(oUnidades);
                }

                dtoUnidades.Estado = "True";
                dtoUnidades.Mensaje = "Se recibio la lista de provincias";
                dtoUnidades.ListaTablaGeneral = oListaUnidades;
            }
            catch (Exception ex)
            {
                dtoUnidades.Estado = "False";
                dtoUnidades.Mensaje = "Se produjo un error: " + ex.Message;
                dtoUnidades.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoUnidades;
        }
        
        public DTORespuestaTablaGeneral fn_Distrito_Listar(string codProvincia, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoUnidades = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaUnidades = new List<DTOTablaGeneral>();
            DTOTablaGeneral oUnidades;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_Distrito_Listar\" ('{codProvincia}')";
                //cm.Parameters.Add("@P_Provincia", OdbcType.VarChar).Value = codProvincia;
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oUnidades = new DTOTablaGeneral();
                    oUnidades.Codigo = dr["Code"].ToString();
                    oUnidades.Descripcion = dr["U_EXX_DESDIS"].ToString();
                    oListaUnidades.Add(oUnidades);
                }

                dtoUnidades.Estado = "True";
                dtoUnidades.Mensaje = "Se recibio la lista de distrito";
                dtoUnidades.ListaTablaGeneral = oListaUnidades;
            }
            catch (Exception ex)
            {
                dtoUnidades.Estado = "False";
                dtoUnidades.Mensaje = "Se produjo un error: " + ex.Message;
                dtoUnidades.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoUnidades;
        }
        
        public DTORespuestaTablaGeneral fn_Sucursales_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoGrupoCliente = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaGrupoCli = new List<DTOTablaGeneral>();
            DTOTablaGeneral oGrupoCli;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_Sucursales_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oGrupoCli = new DTOTablaGeneral();
                    oGrupoCli.Codigo = dr["BPLId"].ToString();
                    oGrupoCli.Descripcion = dr["BPLName"].ToString();
                    oListaGrupoCli.Add(oGrupoCli);
                }

                dtoGrupoCliente.Estado = "True";
                dtoGrupoCliente.Mensaje = "Se recibio la lista de compañias";
                dtoGrupoCliente.ListaTablaGeneral = oListaGrupoCli;
            }
            catch (Exception ex)
            {
                dtoGrupoCliente.Estado = "False";
                dtoGrupoCliente.Mensaje = "Se produjo un error: " + ex.Message;
                dtoGrupoCliente.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoGrupoCliente;
        }
        
        public DTORespuestaTablaGeneral fn_GrupoCliente_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoGrupoCliente = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaGrupoCli = new List<DTOTablaGeneral>();
            DTOTablaGeneral oGrupoCli;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_GrupoClientes_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oGrupoCli = new DTOTablaGeneral();
                    oGrupoCli.Codigo = dr["GroupCode"].ToString();
                    oGrupoCli.Descripcion = dr["GroupName"].ToString();
                    oListaGrupoCli.Add(oGrupoCli);
                }

                dtoGrupoCliente.Estado = "True";
                dtoGrupoCliente.Mensaje = "Se recibio la lista de grupo de clientes";
                dtoGrupoCliente.ListaTablaGeneral = oListaGrupoCli;
            }
            catch (Exception ex)
            {
                dtoGrupoCliente.Estado = "False";
                dtoGrupoCliente.Mensaje = "Se produjo un error: " + ex.Message;
                dtoGrupoCliente.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoGrupoCliente;
        }
        
        public DTORespuestaTablaGeneral fn_ListaPrecio_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoGrupoCliente = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaGrupoCli = new List<DTOTablaGeneral>();
            DTOTablaGeneral oGrupoCli;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_ListaPrecio_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oGrupoCli = new DTOTablaGeneral();
                    oGrupoCli.Codigo = dr["ListNum"].ToString();
                    oGrupoCli.Descripcion = dr["ListName"].ToString();
                    oListaGrupoCli.Add(oGrupoCli);
                }

                dtoGrupoCliente.Estado = "True";
                dtoGrupoCliente.Mensaje = "Se recibio la lista de grupo de clientes";
                dtoGrupoCliente.ListaTablaGeneral = oListaGrupoCli;
            }
            catch (Exception ex)
            {
                dtoGrupoCliente.Estado = "False";
                dtoGrupoCliente.Mensaje = "Se produjo un error: " + ex.Message;
                dtoGrupoCliente.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoGrupoCliente;
        }
        
        public DTORespuestaTablaGeneral fn_Vendedores_Listar(string xCodVendedor, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoGrupoCliente = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaGrupoCli = new List<DTOTablaGeneral>();
            DTOTablaGeneral oGrupoCli;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_Empleado_Listar\"({xCodVendedor})";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oGrupoCli = new DTOTablaGeneral();
                    oGrupoCli.Codigo = dr["SlpCode"].ToString();
                    oGrupoCli.Descripcion = dr["SlpName"].ToString();
                    oListaGrupoCli.Add(oGrupoCli);
                }

                dtoGrupoCliente.Estado = "True";
                dtoGrupoCliente.Mensaje = "Se recibio la lista de vendedores";
                dtoGrupoCliente.ListaTablaGeneral = oListaGrupoCli;
            }
            catch (Exception ex)
            {
                dtoGrupoCliente.Estado = "False";
                dtoGrupoCliente.Mensaje = "Se produjo un error: " + ex.Message;
                dtoGrupoCliente.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoGrupoCliente;
        }
        
        public DTORespuestaTablaGeneral fn_GiroNegocio_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoGrupoCliente = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaGrupoCli = new List<DTOTablaGeneral>();
            DTOTablaGeneral oGrupoCli;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_GriroNegocio_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oGrupoCli = new DTOTablaGeneral();
                    oGrupoCli.Codigo = dr["IndCode"].ToString();
                    oGrupoCli.Descripcion = dr["IndName"].ToString();
                    oListaGrupoCli.Add(oGrupoCli);
                }

                dtoGrupoCliente.Estado = "True";
                dtoGrupoCliente.Mensaje = "Se recibio la lista de vendedores";
                dtoGrupoCliente.ListaTablaGeneral = oListaGrupoCli;
            }
            catch (Exception ex)
            {
                dtoGrupoCliente.Estado = "False";
                dtoGrupoCliente.Mensaje = "Se produjo un error: " + ex.Message;
                dtoGrupoCliente.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoGrupoCliente;
        }

        public DTORespuestaTablaGeneral fn_Zona_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoZona = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaZona = new List<DTOTablaGeneral>();
            DTOTablaGeneral oZona;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_Zona_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oZona = new DTOTablaGeneral();
                    oZona.Codigo = dr["Code"].ToString();
                    oZona.Descripcion = dr["Name"].ToString();
                    oListaZona.Add(oZona);
                }

                dtoZona.Estado = "True";
                dtoZona.Mensaje = "Se recibio la lista de zonas";
                dtoZona.ListaTablaGeneral = oListaZona;
            }
            catch (Exception ex)
            {
                dtoZona.Estado = "False";
                dtoZona.Mensaje = "Se produjo un error: " + ex.Message;
                dtoZona.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoZona;
        }

        public DTORespuestaTablaGeneral fn_MedioEnvio_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoZona = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaZona = new List<DTOTablaGeneral>();
            DTOTablaGeneral oZona;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_TPED_MEDIOENVIO_BUSCAR\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oZona = new DTOTablaGeneral();
                    oZona.Codigo = dr["TrnspCode"].ToString();
                    oZona.Descripcion = dr["TrnspName"].ToString();
                    oListaZona.Add(oZona);
                }

                dtoZona.Estado = "True";
                dtoZona.Mensaje = "Se recibio la lista de medios de envioS";
                dtoZona.ListaTablaGeneral = oListaZona;
            }
            catch (Exception ex)
            {
                dtoZona.Estado = "False";
                dtoZona.Mensaje = "Se produjo un error: " + ex.Message;
                dtoZona.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoZona;
        }
        
        public DTORespuestaTablaGeneral fn_PartidaPresupuestal_Listar(string proyecto, string etapa, string idEmpleado,string sucursal, csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoZona = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaZona = new List<DTOTablaGeneral>();
            DTOTablaGeneral oZona;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                //cm.CommandText = $"CALL \"EXX_SCT_PartidaPresupuestal_Listar\" ('{proyecto}','{etapa}', {idEmpleado})";
                cm.CommandText = $"CALL \"EXX_SCT_PartidaPresupuestal_Listar\" ('{proyecto}','{etapa}', {idEmpleado}, {sucursal})";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oZona = new DTOTablaGeneral();
                    oZona.Codigo = dr["U_EXC_CODPARPR"].ToString();
                    oZona.Descripcion = dr["U_EXC_DESPARPR"].ToString();
                    oZona.Valor01 = dr["DESCRIPCION"].ToString();
                    oZona.Valor02 = dr["CtaContable"].ToString();
                    oZona.Valor03 = dr["CentroCosto"].ToString();
                    oZona.Valor04 = dr["Gerencia"].ToString();
                    oListaZona.Add(oZona);
                }

                dtoZona.Estado = "True";
                dtoZona.Mensaje = "Se recibio la lista de partida presupuestal";
                dtoZona.ListaTablaGeneral = oListaZona;
            }
            catch (Exception ex)
            {
                dtoZona.Estado = "False";
                dtoZona.Mensaje = "Se produjo un error: " + ex.Message;
                dtoZona.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoZona;
        }

        public DTORespuestaTablaGeneral fn_MotivoAnulacion_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoZona = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oListaZona = new List<DTOTablaGeneral>();
            DTOTablaGeneral oZona;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_MotivoAnulacion_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oZona = new DTOTablaGeneral();
                    oZona.Codigo = dr["Code"].ToString();
                    oZona.Descripcion = dr["Name"].ToString();
                    oListaZona.Add(oZona);
                }

                dtoZona.Estado = "True";
                dtoZona.Mensaje = "Se recibio la lista de Motivo de Anulación";
                dtoZona.ListaTablaGeneral = oListaZona;
            }
            catch (Exception ex)
            {
                dtoZona.Estado = "False";
                dtoZona.Mensaje = "Se produjo un error: " + ex.Message;
                dtoZona.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoZona;
        }

        public DTORespuestaTablaGeneral fn_JefeAlmacen_Listar(csCompany xCompany)
        {
            DTORespuestaTablaGeneral dtoZona = new DTORespuestaTablaGeneral();

            List<DTOTablaGeneral> oLista = new List<DTOTablaGeneral>();
            DTOTablaGeneral oZona;

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = "CALL \"EXX_SCT_JefeAlmacen_Listar\"";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oZona = new DTOTablaGeneral();
                    oZona.Codigo = dr["SlpCode"].ToString();
                    oZona.Descripcion = dr["SlpName"].ToString();
                    oLista.Add(oZona);
                }

                dtoZona.Estado = "True";
                dtoZona.Mensaje = "Se recibio la lista de Jefe de Almacén";
                dtoZona.ListaTablaGeneral = oLista;
            }
            catch (Exception ex)
            {
                dtoZona.Estado = "False";
                dtoZona.Mensaje = "Se produjo un error: " + ex.Message;
                dtoZona.ListaTablaGeneral = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return dtoZona;
        }

    }
}
