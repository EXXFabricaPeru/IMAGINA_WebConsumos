using DTOEntidades;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tools;

namespace DBCAccesoDatos
{
    public interface IDBCPedidoVenta
    {
        DTORespuestaPedido fn_PedidoVenta_Listar(string CodVendedor, string FecIni, string FecFin, string Tipo, string cliente, string Estado, string product, csCompany xCompany);
        DTORespuestaPedido fn_TrackingPedidoVenta_Listar(string FecIni, string FecFin, string CodVendedor, string CodCliente, csCompany xCompany);
        DTORespuestaPedido fn_PedidoVenta_Buscar(string pNro, string pTipo, csCompany xCompany);
        DTORespuestaPedido fn_OcDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany);
        DTORespuestaPedido fn_FTDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany);
        string fn_PedidoVentaRegistrar(DTOPedidoVentaCab Pedido, csCompany xCompany);
        DTORespuesta fn_PedidoVenta_Anular(DTOPedidoVentaCab pedido, csCompany xCompany);
    }

    public class DBCPedidoVenta : IDBCPedidoVenta
    {
        Company oCompany;

        #region Metodos No Transaccionales
        public DTORespuestaPedido fn_OcDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany)
        {
            List<DTOPedidoVentaCab> oListaPed = new List<DTOPedidoVentaCab>();
            DTORespuestaPedido dtoRes = new DTORespuestaPedido();

            string _xresult = Connect(xCompany);

            if(_xresult != "")
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + _xresult;
                dtoRes.ListaPedido = null;
                return dtoRes;
            }

            Recordset oRs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            try
            {
                string xQuery = $"CALL \"EXX_SCT_ObtenerOC\" ({empresa},'{proyecto}','{etapa}','{articulo}')";
                oRs.DoQuery(xQuery);

                for (int i = 0; i < oRs.RecordCount; i++) 
                {
                    DTOPedidoVentaCab dto = new DTOPedidoVentaCab();
                    dto.IdPedido = oRs.Fields.Item("DocEntry").Value;
                    dto.NroPedido = oRs.Fields.Item("DocNum").Value.ToString();
                    dto.FecPedido = oRs.Fields.Item("DocDate").Value;
                    dto.LineId = oRs.Fields.Item("N° Línea").Value;
                    dto.CodPartida = oRs.Fields.Item("U_EXC_PARTPRES").Value;
                    dto.Partida = oRs.Fields.Item("U_EXC_NOMPPR").Value;
                    dto.Saldo = oRs.Fields.Item("Cantidad Disponible").Value;
                    oListaPed.Add(dto);

                    oRs.MoveNext();
                }

                if (oListaPed.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se recibio la lista de pedidos";
                    dtoRes.ListaPedido = oListaPed;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se han encontrado ningún pedido con los filtros ingresados";
                    dtoRes.ListaPedido = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaPedido = null;
            }
            return dtoRes;
        }
        public DTORespuestaPedido fn_FTDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany)
        {
            List<DTOPedidoVentaCab> oListaPed = new List<DTOPedidoVentaCab>();
            DTORespuestaPedido dtoRes = new DTORespuestaPedido();

            string _xresult = Connect(xCompany);

            if(_xresult != "")
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + _xresult;
                dtoRes.ListaPedido = null;
                return dtoRes;
            }

            Recordset oRs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            try
            {
                string xQuery = $"CALL \"EXX_SCT_ObtenerFT\" ({empresa},'{proyecto}','{etapa}','{articulo}')";
                oRs.DoQuery(xQuery);

                for (int i = 0; i < oRs.RecordCount; i++) 
                {
                    DTOPedidoVentaCab dto = new DTOPedidoVentaCab();
                    dto.IdPedido = oRs.Fields.Item("DocEntry").Value;
                    dto.NroPedido = oRs.Fields.Item("DocNum").Value.ToString();
                    dto.FecPedido = oRs.Fields.Item("DocDate").Value;
                    dto.LineId = oRs.Fields.Item("N° Línea").Value;
                    dto.CodPartida = oRs.Fields.Item("U_EXC_PARTPRES").Value;
                    dto.Partida = oRs.Fields.Item("U_EXC_NOMPPR").Value;
                    dto.Saldo = oRs.Fields.Item("Cantidad Disponible").Value;
                    oListaPed.Add(dto);
                }

                if (oListaPed.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se recibio la lista de pedidos";
                    dtoRes.ListaPedido = oListaPed;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se han encontrado ningún pedido con los filtros ingresados";
                    dtoRes.ListaPedido = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaPedido = null;
            }
            return dtoRes;
        }

        public DTORespuestaPedido fn_PedidoVenta_Listar(string CodVendedor, string FecIni, string FecFin, string Tipo, string cliente, string Estado, string product, csCompany xCompany)
        {
            List<DTOPedidoVentaCab> oListaPed = new List<DTOPedidoVentaCab>();
            DTOPedidoVentaCab dto = null;

            DTORespuestaPedido dtoRes = new DTORespuestaPedido();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandText = $"CALL \"EXX_SCT_PedidoVentaxVendedor_Listar\" ('{CodVendedor}','{FecIni}','{FecFin}','{cliente}','{Estado}','{product}')";
                cm.CommandType = CommandType.Text;
                cm.Connection = cn;
                cn.Open();
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOPedidoVentaCab();
                    dto.IdPedido = Convert.ToInt32(dr["DocEntry"]);
                    dto.NroPedido = dr["NroPed"].ToString();
                    dto.NomCliente = dr["NombreCliente"].ToString();
                    //dto.NroOC = dr["NroOc"].ToString();
                    //dto.Descuento = Convert.ToDouble(dr["Descuento"]);
                    dto.ImporteTotal = Convert.ToDouble(dr["Total"]);
                    dto.Moneda = dr["Moneda"].ToString();
                    dto.FecPedido = Convert.ToDateTime(dr["FechaPed"]);
                    dto.FecSolicitado = Convert.ToDateTime(dr["FecEntrega"]);
                    dto.Estado = dr["Estado"].ToString();
                    dto.EstadoPed = dr["DocStatus"].ToString();
                    dto.NombreTrabajador = dr["U_EXX_SCT_NOMBRETRABAJADOR"].ToString();
                    dto.UserReg = dr["U_EXX_SCT_NOMBREUSRREG"].ToString();
                    dto.Compania = dr["BPLName"].ToString();
                    oListaPed.Add(dto);
                }

                if (oListaPed.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se recibio la lista de pedidos";
                    dtoRes.ListaPedido = oListaPed;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se han encontrado ningún pedido con los filtros ingresados";
                    dtoRes.ListaPedido = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaPedido = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return dtoRes;
        }

        public DTORespuestaPedido fn_TrackingPedidoVenta_Listar(string FecIni, string FecFin, string CodVendedor, string CodCliente, csCompany xCompany)
        {
            List<DTOPedidoVentaCab> oListaPed = new List<DTOPedidoVentaCab>();
            DTOPedidoVentaCab dto = null;

            DTORespuestaPedido dtoRes = new DTORespuestaPedido();
            
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_TrakingCab_Listar]";
                cm.CommandType = CommandType.Text;
                cm.Parameters.Add("@P_FecIni", OdbcType.VarChar).Value = FecIni;
                cm.Parameters.Add("@P_FecFin", OdbcType.VarChar).Value = FecFin;
                cm.Parameters.Add("@P_Vendedor", OdbcType.Int).Value = CodVendedor;
                //cm.Parameters.Add("@P_Cliente", OdbcType.VarChar).Value = CodCliente;
                cm.Connection = cn;
                cn.Open();
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOPedidoVentaCab();
                    dto.NroPedido = dr["NroPedido"].ToString();
                    dto.FecDespacho = Convert.ToDateTime(dr["FecGuia"]);
                    dto.FecFacturacion = Convert.ToDateTime(dr["FecFactBol"]);
                    dto.ImporteTotal = Convert.ToDouble(dr["ImpTotal"]);
                    dto.Estado = dr["EstadoTr"].ToString();
                    dto.EstadoPed = dr["DocStatus"].ToString();
                    oListaPed.Add(dto);
                }

                if (oListaPed.Count > 0)
                {
                    dtoRes.Estado = "True";
                    dtoRes.Mensaje = "Se recibio la lista de pedidos";
                    dtoRes.ListaPedido = oListaPed;
                }
                else
                {
                    dtoRes.Estado = "False";
                    dtoRes.Mensaje = "No se ah encontrado ningun pedido con los filtros ingresados";
                    dtoRes.ListaPedido = null;
                }
            }
            catch (Exception ex)
            {
                dtoRes.Estado = "False";
                dtoRes.Mensaje = "Se produjo un error: " + ex.Message;
                dtoRes.ListaPedido = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return dtoRes;
        }

        public DTORespuestaPedido fn_PedidoVenta_Buscar(string pNro, string pTipo, csCompany xCompany)
        {
            DTOPedidoVentaCab dtoPed = new DTOPedidoVentaCab();
            List<DTOPedidoVentaCab> oListaCab = new List<DTOPedidoVentaCab>();
            List<DTOPedidoVentaDet> oListaDet = new List<DTOPedidoVentaDet>();
            DTOPedidoVentaDet dtodet = null;

            DTORespuestaPedido dto = new DTORespuestaPedido();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandText = $"CALL \"EXX_SCT_OrdenVenta_Buscar\" ('{pNro}')";
                cm.CommandType = CommandType.Text;
                //cm.Parameters.Add("@P_DocEntry", OdbcType.VarChar).Value = pNro;
                //cm.Parameters.Add("@P_Tipo", OdbcType.VarChar).Value = pTipo;
                cm.Connection = cn;
                cn.Open();
                dr = cm.ExecuteReader();

                int i = 0;
                double xSubTotal = 0;
                double xIgv = 0;

                while (dr.Read())
                {
                    if (i == 0)
                    {
                        dtoPed = new DTOPedidoVentaCab();
                        dtoPed.IdPedido = Convert.ToInt32(dr["DocEntry"]);
                        dtoPed.NroPedido = dr["DocNum"].ToString();
                        dtoPed.CodCliente = dr["CardCode"].ToString();
                        dtoPed.NomCliente = dr["CardName"].ToString();
                        dtoPed.FecContabilizacion = Convert.ToDateTime(dr["DocDate"]);
                        dtoPed.FecPedido = Convert.ToDateTime(dr["TaxDate"]);
                        dtoPed.FecSolicitado = Convert.ToDateTime(dr["DocDueDate"]);
                        dtoPed.CodDireccion = dr["ShipToCode"].ToString();
                        dtoPed.Direccion = dr["Address2"].ToString();
                        dtoPed.Moneda = dr["DocCur"].ToString();
                        dtoPed.CondPago = dr["GroupNum"].ToString();
                        dtoPed.Direccion = dr["Address2"].ToString();
                        dtoPed.Comentario = dr["Comments"].ToString();
                        dtoPed.Sucursal = Convert.ToInt32(dr["BPLId"]);
                        dtoPed.Estado = dr["Estado"].ToString();
                        dtoPed.Series = dr["Series"].ToString();
                        dtoPed.TipoOperacion = dr["TpoOperacion"].ToString();                        
                        dtoPed.MedioEnvio = Convert.ToInt32(dr["TrnspCode"]);
                        dtoPed.Descuento = Convert.ToDouble(dr["Descuento"]);
                        dtoPed.EstadoPed = dr["DocStatus"].ToString();
                        dtoPed.NombreTrabajador = dr["NombreTrabajador"].ToString();
                        dtoPed.Compania = dr["BPLName"].ToString();
                        dtoPed.MotivoAnulacion = dr["U_EXC_MOTANU"].ToString();
                        dtoPed.JefeAlm = dr["JefeAlm"].ToString();
                    }

                    dtodet = new DTOPedidoVentaDet();
                    dtodet.IdProducto = dr["ItemCode"].ToString();
                    dtodet.Descripcion = dr["Dscription"].ToString();
                    dtodet.Cantidad = Convert.ToDouble(dr["Quantity"]);
                    dtodet.PrecioUnit = Convert.ToDouble(dr["Price"]);
                    //dtodet.PrecioBruto = Math.Round(dtodet.PrecioUnit * 1.18, 2);
                    dtodet.PrecioTotal = Convert.ToDouble(dr["LineTotal"]);
                    dtodet.TipoImpuesto = dr["TaxCode"].ToString();
                    dtodet.CodUndMed = Convert.ToInt32(dr["UomEntry"]);
                    dtodet.Unidad = dr["UomCode"].ToString();
                    dtodet.CodAlmacen = dr["WhsCode"].ToString();
                    dtodet.Dimension1 = dr["OcrCode"].ToString();
                    dtodet.Dimension2 = dr["OcrCode2"].ToString();
                    dtodet.Dimension3 = dr["OcrCode3"].ToString();
                    dtodet.Dimension4 = dr["OcrCode4"].ToString();
                    dtodet.Dimension5 = dr["OcrCode5"].ToString();
                    dtodet.Proyecto = dr["Project"].ToString();
                    dtodet.LineNum = Convert.ToInt32(dr["LineNum"]);
                    dtodet.Descuento = Convert.ToDouble(dr["DiscPrcnt"]);
                    dtodet.CantOpen = Convert.ToDouble(dr["OpenQty"]);
                    dtodet.CantAten = dtodet.Cantidad - dtodet.CantOpen;
                    xSubTotal += (dtodet.PrecioUnit * dtodet.Cantidad);
                    dtodet.Estado = dr["Estado"].ToString();
                    dtodet.IdPartida = dr["CodPartida"].ToString();
                    dtodet.Partida = dr["Partida"].ToString();
                    dtodet.NroEntrega = dr["NroEntrega"].ToString();
                    dtodet.DocEntryOc = dr["DocEntryOc"].ToString();
                    dtodet.DocNumOc = dr["DocNumOc"].ToString();
                    dtodet.NumLine = dr["NumLine"].ToString();
                    dtodet.NumFacOC = dr["NumFacOC"].ToString();

                    oListaDet.Add(dtodet);

                    i++;
                }

                if (i > 0)
                {
                    dtoPed.ListaDetalle = oListaDet;
                    oListaCab.Add(dtoPed);
                    dto.Estado = "True";
                    dto.Mensaje = "Se recibio el pedido de venta";
                    dto.ListaPedido = oListaCab;
                }
                else
                {
                    dto.Estado = "False";
                    dto.Mensaje = "No se ah encontrado ningun pedido con los filtros ingresados";
                    dto.ListaPedido = null;
                }
            }
            catch(Exception ex)
            {
                dto.Estado = "False";
                dto.Mensaje = "Se produjo un error:" + ex.Message;
                dto.ListaPedido = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return dto;
        }

        #endregion

        #region Metodos Transaccionales

        public string fn_PedidoVentaRegistrar(DTOPedidoVentaCab pPedido, csCompany xCompany)
        {
            string _xresult = "";
            try
            {
                _xresult = Connect(xCompany);

                if (_xresult == "")
                {
                    Documents oDoc = oCompany.GetBusinessObject(BoObjectTypes.oOrders); //---->Objeto Pedido
                    oDoc.DocObjectCode = BoObjectTypes.oOrders; //----> Indico que es un Orden de Venta

                    oDoc.DocType = BoDocumentTypes.dDocument_Items;
                    oDoc.DocDate = DateTime.Now;
                    oDoc.DocDueDate = pPedido.FecSolicitado;
                    oDoc.TaxDate = pPedido.FecPedido;
                    oDoc.Comments = pPedido.Comentario;
                    oDoc.CardCode = pPedido.CodCliente;
                    oDoc.Indicator = "CI";
                    oDoc.NumAtCard = pPedido.NroOC;
                    oDoc.DocCurrency = pPedido.Moneda;
                    oDoc.Series = Convert.ToInt32(pPedido.Series);

                    if (pPedido.Sucursal != 0) oDoc.BPL_IDAssignedToInvoice = pPedido.Sucursal;
                    oDoc.SalesPersonCode = Convert.ToInt32(pPedido.JefeAlm);
                    oDoc.DocumentsOwner = Convert.ToInt32(pPedido.CodVendedor) == 0 ? 15 : Convert.ToInt32(pPedido.CodVendedor);
                    oDoc.UserFields.Fields.Item("U_EXX_TIPOOPER").Value = pPedido.TipoOperacion;
                    oDoc.UserFields.Fields.Item("U_EXX_SCT_USERREG").Value = pPedido.UserReg;

                    string sQuery = $"SELECT \"lastName\" || ' ' || \"firstName\" || ' ' || IFNULL(\"middleName\",'') FROM \"@EXX_SCT_USER\" T0 LEFT JOIN OHEM T1 ON T0.\"U_EXX_EMPLEADO\" = T1.\"empID\" " +
                                    $"WHERE \"U_EXX_USER\" = '{pPedido.UserReg}'";
                    Recordset xRs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    xRs.DoQuery(sQuery);

                    oDoc.UserFields.Fields.Item("U_EXX_SCT_NOMBREUSRREG").Value = xRs.Fields.Item(0).Value;
                    oDoc.UserFields.Fields.Item("U_EXX_SCT_NOMBRETRABAJADOR").Value = pPedido.NombreTrabajador;
                    oDoc.UserFields.Fields.Item("U_EXC_CONSINTE").Value = "Y";

                    for (int i = 0; i < pPedido.ListaDetalle.Count; i++)
                    {
                        oDoc.Lines.ItemCode = pPedido.ListaDetalle[i].IdProducto;
                        oDoc.Lines.ItemDescription = pPedido.ListaDetalle[i].Descripcion;
                        oDoc.Lines.Quantity = pPedido.ListaDetalle[i].Cantidad;
                        oDoc.Lines.UnitPrice = pPedido.ListaDetalle[i].PrecioUnit;
                        oDoc.Lines.UoMEntry = pPedido.ListaDetalle[i].CodUndMed;
                        oDoc.Lines.TaxCode = pPedido.ListaDetalle[i].TipoImpuesto;
                        oDoc.Lines.WarehouseCode = pPedido.ListaDetalle[i].CodAlmacen;
                        oDoc.Lines.ProjectCode = pPedido.ListaDetalle[i].Proyecto;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_PARTPRES").Value = pPedido.ListaDetalle[i].IdPartida;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_NOMPPR").Value = pPedido.ListaDetalle[i].Partida;

                        string xQuery = $"SELECT COALESCE(U_EXX_GRUPODET, '099'), COALESCE(U_EXX_GRUPOPER, '0000') FROM OITM WHERE \"ItemCode\" = '{pPedido.ListaDetalle[i].IdProducto}'";
                        Recordset oRs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                        oRs.DoQuery(xQuery);

                        string xGrupoDet = oRs.Fields.Item(0).Value == string.Empty ? "099" : oRs.Fields.Item(0).Value;
                        string xGrupoPer = oRs.Fields.Item(1).Value == string.Empty ? "0000" : oRs.Fields.Item(1).Value;

                        oDoc.Lines.UserFields.Fields.Item("U_EXX_GRUPOPER").Value = xGrupoPer;
                        oDoc.Lines.UserFields.Fields.Item("U_EXX_GRUPODET").Value = xGrupoDet;

                        if(pPedido.ListaDetalle[i].DocEntryOc != "") oDoc.Lines.UserFields.Fields.Item("U_EXC_IDOC").Value = pPedido.ListaDetalle[i].DocEntryOc;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_NUMOPDN").Value = pPedido.ListaDetalle[i].DocNumOc;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_EMLINEA").Value = pPedido.ListaDetalle[i].NumLine;
                        if(pPedido.ListaDetalle[i].NumFacOC != "") oDoc.Lines.UserFields.Fields.Item("U_EXC_IDFACT").Value = pPedido.ListaDetalle[i].NumFacOC;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_PROYEC").Value = pPedido.ListaDetalle[i].Proyecto;
                        oDoc.Lines.UserFields.Fields.Item("U_EXC_ETAPA").Value = pPedido.ListaDetalle[i].Dimension1;
                        oDoc.Lines.CostingCode = pPedido.ListaDetalle[i].Dimension1;
                        oDoc.Lines.CostingCode2 = pPedido.ListaDetalle[i].Dimension2;
                        oDoc.Lines.CostingCode3 = pPedido.ListaDetalle[i].Dimension3;
                        oDoc.Lines.CostingCode4 = pPedido.ListaDetalle[i].Dimension4;
                        //oDoc.Lines.CostingCode5 = pPedido.ListaDetalle[i].Dimension5;
                        oDoc.Lines.DiscountPercent = pPedido.ListaDetalle[i].Descuento;
                        oDoc.Lines.COGSAccountCode = pPedido.ListaDetalle[i].CuentaContable;
                        
                        if ((i + 1) < pPedido.ListaDetalle.Count)
                            oDoc.Lines.Add();
                    }

                    if (oDoc.Add() != 0) 
                    {
                        _xresult = oCompany.GetLastErrorDescription().Replace("-", " ");
                    }
                    else
                    {
                        string _xRespuesta = "";
                        oCompany.GetNewObjectCode(out _xRespuesta);

                        oDoc.GetByKey(Convert.ToInt32(_xRespuesta));

                        _xresult = "El pedido de venta se registró con éxito con número " + oDoc.DocNum.ToString() + "-" + _xRespuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                _xresult = ex.Message;
            }
            finally
            {
                //if (oCompany != null)
                //    oCompany.Disconnect();
            }
            return _xresult;
        }

        public DTORespuesta fn_PedidoVenta_Anular(DTOPedidoVentaCab pedido, csCompany xCompany)
        {
            DTORespuesta dto = new DTORespuesta();

            try
            {
                if (Connect(xCompany) == "")
                {
                    Documents st = oCompany.GetBusinessObject(BoObjectTypes.oOrders);

                    st.GetByKey(pedido.IdPedido);

                    oCompany.StartTransaction();

                    if (st.Cancel() != 0)
                    {
                        dto.Estado = false;
                        dto.Mensaje = oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        st.GetByKey(pedido.IdPedido);

                        st.UserFields.Fields.Item("U_EXC_MOTANU").Value = pedido.Comentario;

                        if(st.Update() != 0)
                        {
                            dto.Estado = false;
                            dto.Mensaje = oCompany.GetLastErrorDescription();
                        }
                        else
                        {
                            dto.Estado = true;
                            dto.Mensaje = "Se anuló con exito";
                        }                        
                    }

                    if (dto.Estado)
                    {
                        if (oCompany.InTransaction)
                            oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    }
                    else
                    {
                        if (oCompany.InTransaction)
                            oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                    }
                }
            }
            catch (Exception ex)
            {
                if (oCompany.InTransaction)
                    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                dto.Estado = false;
                dto.Mensaje = ex.Message;
            }finally
            {
                //oCompany.Disconnect();
            }

            return dto;
        }
        
        public DTORespuesta fn_PedidoVentaLinea_Anular(DTOPedidoVentaCab pedido, csCompany xCompany)
        {
            DTORespuesta dto = new DTORespuesta();

            try
            {
                if (Connect(xCompany) == "")
                {
                    Documents st = oCompany.GetBusinessObject(BoObjectTypes.oOrders);

                    st.GetByKey(pedido.IdPedido);

                    int i = pedido.ListaDetalle[0].LineNum;

                    st.Lines.SetCurrentLine(i);
                    st.Lines.LineStatus = BoStatus.bost_Close;
                    if (st.Update() != 0)
                    {
                        dto.Estado = false;
                        dto.Mensaje = oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        dto.Estado = true;
                        dto.Mensaje = "Se anuló con exito";
                    }
                }
            }
            catch (Exception ex)
            {
                dto.Estado = false;
                dto.Mensaje = ex.Message;
            }

            return dto;
        }

        #endregion

        private string Connect(csCompany xCompany)
        {
            string _Error = "";

            try
            {
                oCompany = new ConexionSAP().LoginSAP(xCompany);
                if (oCompany != null)
                    _Error = "";
            }
            catch (Exception ex)
            {
                _Error = ex.Message;
            }

            return _Error;
        }

    }
}
