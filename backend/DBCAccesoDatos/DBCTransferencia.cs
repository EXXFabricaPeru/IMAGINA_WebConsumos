using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOEntidades;
using SAPbobsCOM;

namespace DBCAccesoDatos
{
    public interface IDBCTransferencia
    {
        DTORespuesta fn_SolicitudTransferencia_Registrar(DTOTransferenciaCab transferencia, csCompany xCompany);
        DTOTransferenciaCab fn_SolicitudTransferencia_Buscar(string docEntry, csCompany xCompany);
        List<DTOTransferenciaCab> fn_SolicitudTransferencia_Listar(string fecDesde, string fecHasta, string estado, string user, string compania, string product, csCompany xCompany);
        DTORespuesta fn_SolicitudTransferencia_Anular(DTOTransferenciaCab transferencia, csCompany xCompany);
    }

    public class DBCTransferencia : IDBCTransferencia
    {
        Company oCompany;
        public DTORespuesta fn_SolicitudTransferencia_Registrar(DTOTransferenciaCab transferencia, csCompany xCompany)
        {
            DTORespuesta dto = new DTORespuesta();

            try
            {
                if(Connect(xCompany) == "")
                {
                    StockTransfer st = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest);
                    st.DocDate = transferencia.DocDate;
                    st.DueDate = transferencia.DocDueDate;
                    st.TaxDate = transferencia.TaxDate;
                    st.SalesPersonCode = Convert.ToInt32(transferencia.JefeAlm);
                    st.Series = Convert.ToInt32(transferencia.Series);
                    st.FromWarehouse = transferencia.CodAlmacenOri;
                    st.ToWarehouse = transferencia.CodAlmacenDest;
                    st.Comments = transferencia.Comentario;

                    st.UserFields.Fields.Item("U_EXX_SCT_USERREG").Value = transferencia.UserReg;
                    st.UserFields.Fields.Item("U_EXX_SCT_NOMBRETRABAJADOR").Value = transferencia.NombreTrabajador;
                    st.UserFields.Fields.Item("U_EXX_TIPOOPER").Value = transferencia.TipoOperacion;
                    st.UserFields.Fields.Item("U_EXC_CONSINTE").Value = "Y";
                    string sQuery = $"SELECT \"lastName\" || ' ' || \"firstName\" || ' ' || IFNULL(\"middleName\",'') FROM \"@EXX_SCT_USER\" T0 LEFT JOIN OHEM T1 ON T0.\"U_EXX_EMPLEADO\" = T1.\"empID\" " +
                                    $"WHERE \"U_EXX_USER\" = '{transferencia.UserReg}'";
                    Recordset xRs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    xRs.DoQuery(sQuery);

                    st.UserFields.Fields.Item("U_EXX_SCT_NOMBREUSRREG").Value = xRs.Fields.Item(0).Value;
                    //st.UserFields.Fields.Item("U_EXX_SCT_JEFEALM").Value = transferencia.JefeAlm;

                    int i = 0;

                    foreach(DTOTransferenciaDet item in transferencia.ListaDetalle) 
                    {
                        st.Lines.ItemCode = item.IdProducto;
                        st.Lines.ItemDescription = item.Descripcion;
                        st.Lines.Quantity = item.Cantidad;
                        st.Lines.FromWarehouseCode = item.CodAlmacenOri;
                        st.Lines.WarehouseCode = item.CodAlmacenDest;
                        st.Lines.DistributionRule = item.Dimension1;
                        st.Lines.DistributionRule2 = item.Dimension2;
                        st.Lines.DistributionRule3 = item.Dimension3;
                        st.Lines.DistributionRule4 = item.Dimension4;
                        st.Lines.ProjectCode = item.Proyecto;

                        st.Lines.UserFields.Fields.Item("U_EXC_PROYEC").Value = item.Proyecto;
                        st.Lines.UserFields.Fields.Item("U_EXC_ETAPA").Value = item.Dimension1;
                        st.Lines.UserFields.Fields.Item("U_EXC_PARTPRES").Value = item.IdPartida;
                        st.Lines.UserFields.Fields.Item("U_EXC_NOMPPR").Value = item.Partida;

                        i++;
                        if (i < transferencia.ListaDetalle.Count)
                            st.Lines.Add();
                    }

                    if(st.Add() != 0)
                    {
                        dto.Estado = false;
                        dto.Mensaje = oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        dto.Estado = true;
                        dto.key = oCompany.GetNewObjectKey();

                        st = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest);
                        st.GetByKey(Convert.ToInt32(dto.key));

                        dto.Mensaje = "Se registró con exitó la solicitud de transferencia N° " + st.DocNum.ToString();
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

        public DTORespuesta fn_SolicitudTransferencia_Anular(DTOTransferenciaCab transferencia, csCompany xCompany)
        {
            DTORespuesta dto = new DTORespuesta();

            try
            {
                if (Connect(xCompany) == "")
                {
                    StockTransfer st = (StockTransfer)oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest);

                    st.GetByKey(transferencia.DocEntry);

                    oCompany.StartTransaction();

                    if (st.Close() != 0)
                    {
                        dto.Estado = false;
                        dto.Mensaje = oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        st.GetByKey(transferencia.DocEntry);

                        st.UserFields.Fields.Item("U_EXC_MOTANU").Value = transferencia.Comentario;

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
                dto.Estado = false;
                dto.Mensaje = ex.Message;
            }

            return dto;
        }

        public DTOTransferenciaCab fn_SolicitudTransferencia_Buscar(string docEntry, csCompany xCompany)
        {
            DTOTransferenciaCab dto = new DTOTransferenciaCab();
            List<DTOTransferenciaDet> oLista = new List<DTOTransferenciaDet>();
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandText = $"CALL \"EXX_SCT_SolTransferencia_Buscar\" ('{docEntry}')";
                cm.CommandType = CommandType.Text;
                cm.Connection = cn;
                cn.Open();
                dr = cm.ExecuteReader();

                int i = 0;
                while (dr.Read())
                {
                    if(i == 0)
                    {
                        dto.DocEntry = Convert.ToInt32(dr["DocEntry"]);
                        dto.DocNum = Convert.ToInt32(dr["DocNum"]);
                        dto.DocDate = Convert.ToDateTime(dr["DocDate"]);
                        dto.TaxDate = Convert.ToDateTime(dr["TaxDate"]);
                        dto.DocDueDate = Convert.ToDateTime(dr["DocDueDate"]);
                        dto.IdSucursal = Convert.ToInt32(dr["BPLId"]);
                        dto.Sucursal = dr["BPLName"].ToString();
                        dto.TipoOperacion = dr["TipoOpe"].ToString();
                        dto.Estado = dr["DocStatus"].ToString();
                        dto.Comentario   = dr["Comments"].ToString();
                        dto.NombreTrabajador   = dr["NombreTrabajador"].ToString();
                        dto.MotivoAnulacion = dr["MotivoAnulacion"].ToString();
                        dto.JefeAlm = dr["JefeAlm"].ToString();
                    }

                    DTOTransferenciaDet item = new DTOTransferenciaDet();
                    item.IdProducto = dr["ItemCode"].ToString();
                    item.Descripcion = dr["Dscription"].ToString();
                    item.Dimension1 = dr["OcrCode"].ToString();
                    item.Dimension2 = dr["OcrCode2"].ToString();
                    item.Dimension3 = dr["OcrCode3"].ToString();
                    item.Dimension4 = dr["OcrCode4"].ToString();
                    item.Proyecto = dr["Project"].ToString();
                    item.CodAlmacenOri = dr["FromWhsCod"].ToString();
                    item.CodAlmacenDest = dr["WhsCode"].ToString();
                    item.Cantidad = Convert.ToDouble(dr["Quantity"]);
                    item.CantidadOpen = Convert.ToDouble(dr["OpenQty"]);
                    item.CantidadAten = Convert.ToDouble(dr["CantidadAtendida"]);
                    item.IdPartida = dr["U_EXC_PARTPRES"].ToString();
                    item.Partida = dr["U_EXC_NOMPPR"].ToString();
                    item.NroTransferencia = dr["NroTransferencia"].ToString();
                    item.Estado = dr["Estado"].ToString();

                    oLista.Add(item);
                    i++;
                }

                int total = oLista.Count;
                int tt = oLista.Where(t=>t.Estado == "CERRADO").ToList().Count();

                if (total == tt)
                    dto.Estado = "CERRADO";
                else
                    dto.Estado = "ABIERTO";



                dto.ListaDetalle = oLista;
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return dto;
        }

        public List<DTOTransferenciaCab> fn_SolicitudTransferencia_Listar(string fecDesde, string fecHasta, string estado, string user, string compania, string product, csCompany xCompany)
        {
            List<DTOTransferenciaCab> oLista = new List<DTOTransferenciaCab>();
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandText = $"CALL \"EXX_SCT_SolTransferencia_Listar\" ('{fecDesde}','{fecHasta}','{estado}','{user}','{compania}','{product}')";
                cm.CommandType = CommandType.Text;
                cm.Connection = cn;
                cn.Open();
                dr = cm.ExecuteReader();

                while(dr.Read())
                {
                    DTOTransferenciaCab item = new DTOTransferenciaCab();
                    item.DocEntry = Convert.ToInt32(dr["DocEntry"]);
                    item.DocNum = Convert.ToInt32(dr["DocNum"]);
                    item.DocDate = Convert.ToDateTime(dr["DocDate"]);
                    //item.UserReg = dr["U_EXX_SCT_USERREG"].ToString();
                    item.Estado = dr["Estado"].ToString();
                    item.CodAlmacenDest = dr["WhsCode"].ToString();
                    item.CodAlmacenOri = dr["FromWhsCod"].ToString();
                    item.Sucursal = dr["BPLName"].ToString();
                    item.UserReg = dr["U_EXX_SCT_NOMBREUSRREG"].ToString();
                    oLista.Add(item);
                }

                dr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return oLista;
        }

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
