using DTOEntidades;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace DBCAccesoDatos
{
    public interface IDBCCliente
    {
        List<DTORespuestaCliente> fn_ClienteVendedor_Listar(string pValor, string pVendedor, string flag, csCompany xCompany);
        DTORespuestaCliente fn_Cliente_Buscar(string codCliente, csCompany xCompany);
        string fn_Cliente_Registrar(DTOCliente cliente, csCompany xCompany);
        string fn_Cliente_Editar(DTOCliente cliente, csCompany xCompany);
        DTORespuestaEstadoCuenta fn_EstadoCuenta_Listar(string codCliente, csCompany xCompany);
        DTORespuestaCliente fn_Transportista_Buscar(csCompany xCompany);
        DTORespuesta fn_ClienteSucursal(string idSucursal, csCompany xCompany);
    }

    public class DBCCliente : IDBCCliente
    {
        //SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=SBO_NewTrade_PE;Persist Security Info=True;User ID=sa;Password=b1admin");
        Company oCompany;

        #region Consultas
        public List<DTORespuestaCliente> fn_ClienteVendedor_Listar(string pValor, string pVendedor, string flag, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;
            DTOCliente oDTOCliente = new DTOCliente();
            List<DTOCliente> oListaCli = new List<DTOCliente>();

            List<DTORespuestaCliente> oLista = new List<DTORespuestaCliente>();
            DTORespuestaCliente dto = new DTORespuestaCliente();

            try
            {
                if (pValor == null) pValor = "";
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_ClientexVendedor_Listar\" ('{pValor}','{pVendedor}','{flag}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oDTOCliente = new DTOCliente();
                    oDTOCliente.CodigoCliente = dr["CardCode"].ToString();
                    oDTOCliente.Ruc = dr["LicTradNum"].ToString();
                    oDTOCliente.Nombre = dr["CardName"].ToString();
                    oDTOCliente.CodListaPrecio = dr["ListNum"].ToString();
                    oDTOCliente.CodVendedor = dr["SlpCode"].ToString();
                    oDTOCliente.CodCondPago = dr["GroupNum"].ToString();
                    //oDTOCliente.Canal = dr["TipoCliente"].ToString();
                    oDTOCliente.LineaCredito = Convert.ToDouble(dr["CreditLine"]);
                    oDTOCliente.SaldoDisponible = Convert.ToDouble(dr["Saldo"]);
                    oDTOCliente.ShipType = dr["ShipType"].ToString();
                    oDTOCliente.Flag = dr["Flag"].ToString() == "1" ? true : false;
                    oListaCli.Add(oDTOCliente);
                }

                if (oListaCli.Count > 0)
                {
                    dto.Estado = "True";
                    dto.Mensaje = "Se ha recibido la lista de clientes";
                    dto.ListaCLiente = oListaCli;

                    oLista.Add(dto);
                }
                else
                {
                    dto.Estado = "False";
                    dto.Mensaje = "No se ha encontrado ningún cliente con los filtros ingresados";
                    dto.ListaCLiente = null;

                    oLista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Estado = "False";
                dto.Mensaje = "Error en la consulta: " + ex.Message;
                dto.ListaCLiente = null;

                oLista.Add(dto);    
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return oLista;
        }

        public DTORespuestaCliente fn_Cliente_Buscar(string codCliente, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;
            DTOCliente oDTOCliente;
            List<DTOCliente> oListaCli = new List<DTOCliente>();

            DTORespuestaCliente dto = new DTORespuestaCliente();

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_Cliente_Buscar\" ('{codCliente}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                dr.Read();
                oDTOCliente = new DTOCliente();
                oDTOCliente.CodigoCliente = dr["CardCode"].ToString();
                oDTOCliente.Ruc = dr["LicTradNum"].ToString();
                oDTOCliente.Nombre = dr["CardName"].ToString();
                oDTOCliente.Currency = dr["Currency"].ToString();
                oDTOCliente.GrupoCliente = dr["GroupCode"].ToString();
                oDTOCliente.IndustryC = Convert.ToInt32(dr["IndustryC"]);
                oDTOCliente.TpoPersona = dr["U_EXX_TIPOPERS"].ToString();
                oDTOCliente.TpoDocumento = dr["U_EXX_TIPODOCU"].ToString();
                oDTOCliente.ApellidoPaterno = dr["U_EXX_APELLPAT"].ToString();
                oDTOCliente.ApellidoMaterno = dr["U_EXX_APELLMAT"].ToString();
                oDTOCliente.PrimerNombre = dr["U_EXX_PRIMERNO"].ToString();
                oDTOCliente.SegundoNombre = dr["U_EXX_SEGUNDNO"].ToString();
                oDTOCliente.TelefonoCliente = dr["Phone1"].ToString();
                oDTOCliente.CelularCliente = dr["Cellular"].ToString();
                oDTOCliente.CorreoCliente = dr["E_Mail"].ToString();
                oDTOCliente.AgenteRetencion = dr["QryGroup2"].ToString() == "Y" ? true: false;
                oDTOCliente.BuenContribuyente = dr["QryGroup5"].ToString() == "Y" ? true: false;

                dr.Close();

                oDTOCliente.ListaDireccion = fn_Direccion_Listar(codCliente, cn);
                oDTOCliente.ListaContacto = fn_Contacto_Listar(codCliente, cn);
                oListaCli.Add(oDTOCliente);

                if (oListaCli.Count > 0)
                {
                    dto.Estado = "True";
                    dto.Mensaje = "Se ha recibido la lista de clientes";
                    dto.ListaCLiente = oListaCli;
                }
                else
                {
                    dto.Estado = "False";
                    dto.Mensaje = "No se ah encontrado ningun cliente con los filtros ingresados";
                    dto.ListaCLiente = null;
                }
            }
            catch (Exception ex)
            {
                dto.Estado = "False";
                dto.Mensaje = "Error en la consulta: " + ex.ToString();
                dto.ListaCLiente = null;
            }
            finally
            {
                cn.Close();
            }
            return dto;
        }

        List<DTODireccion> fn_Direccion_Listar(string codCliente, OdbcConnection cn)
        {
            //OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            List<DTODireccion> oLista = new List<DTODireccion>();
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_DireccionCliente_Buscar\" ('{codCliente}')";
                //cm.Parameters.Add("@P_CardCode", OdbcType.VarChar).Value = codCliente;
                cm.Connection = cn;

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    DTODireccion direccion = new DTODireccion();
                    direccion.Nombre = dr["Address"].ToString();
                    direccion.Direccion = dr["Street"].ToString();
                    direccion.Distrito = dr["ZipCode"].ToString();
                    direccion.Provincia = dr["County"].ToString();
                    direccion.Departamento = dr["State"].ToString();
                    direccion.Tipo = dr["AdresType"].ToString();
                    direccion.Ubigeo = dr["Ubigeo"].ToString();
                    oLista.Add(direccion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return oLista;
        }

        List<DTOContacto> fn_Contacto_Listar(string codCliente, OdbcConnection cn)
        {
            List<DTOContacto> oLista = new List<DTOContacto>();
            //OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;
            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_ContactosCliente_Listar\" ('{codCliente}')";
                //cm.Parameters.Add("@P_CodCliente", OdbcType.VarChar).Value = codCliente;
                cm.Connection = cn;

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    DTOContacto contacto = new DTOContacto();
                    contacto.Nombre = dr["Name"].ToString();
                    contacto.PrimerNombre = dr["FirstName"].ToString();
                    contacto.SegundoNombre = dr["MiddleName"].ToString();
                    contacto.Apellido = dr["LastName"].ToString();
                    contacto.Celular = dr["Cellolar"].ToString();
                    contacto.Telefono = dr["Tel1"].ToString();
                    contacto.Cargo = dr["Position"].ToString();
                    contacto.Telefono = dr["Tel1"].ToString();
                    contacto.Email = dr["E_MailL"].ToString();
                    oLista.Add(contacto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return oLista;
        }

        public DTORespuestaEstadoCuenta fn_EstadoCuenta_Listar(string codCliente, csCompany xCompany)
        {
            DTORespuestaEstadoCuenta oRpta = new DTORespuestaEstadoCuenta();
            DTOEstadoCuenta dto = null;
            List<DTOEstadoCuenta> oLista = new List<DTOEstadoCuenta>();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            try
            {
                Connect(xCompany);
                SBObob oSBObob;
                Recordset oRS = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                oSBObob = oCompany.GetBusinessObject(BoObjectTypes.BoBridge);
                oRS = oSBObob.GetCurrencyRate("USD", DateTime.Now);
                double _Rate = oRS.Fields.Item(0).Value;

                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_TPED_EstadoCuentaCliente_Listar\" ('{codCliente}')";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dto = new DTOEstadoCuenta
                    {
                        CodCliente = dr["RUC"].ToString(),
                        DiasVencidos = Convert.ToInt32(dr["Dias Vencidos"]),
                        Direccion = dr["Direccion"].ToString(),
                        FecVencimiento = Convert.ToDateTime(dr["F.Vencimiento"]),
                        Moneda = dr["Moneda"].ToString(),
                        NomCliente = dr["Socio de Negocio"].ToString(),
                        NroDocumento = dr["Nro Legal"].ToString(),
                        Original = Convert.ToDouble(dr["Total Soles"]),
                        OriginalUsd = Convert.ToDouble(dr["Total Dolares"]),
                        TipoDoc = dr["Tipo"].ToString(),
                        Saldo = Convert.ToDouble(dr["Saldo Soles"]),
                        SaldoUsd = Convert.ToDouble(dr["Saldo Dolares"]),
                        LineaCredito = Convert.ToDouble(dr["CreditLine"]),
                        Compania = dr["Compania"].ToString(),
                        CompaniaDir = dr["CompaniaDir"].ToString(),
                        CompaniaRuc = dr["CompaniaRuc"].ToString(),
                        CompaniaMail = dr["CompaniaMail"].ToString(),
                        CompaniaTel = dr["CompaniaTel"].ToString(),
                        TipoCambio = _Rate
                    };
                    oLista.Add(dto);
                }

                dr.Close();

                if (oLista.Count > 0)
                {
                    oRpta.Estado = true;
                    oRpta.Mensaje = "Se recibio la lista de estado de cuenta";
                    oRpta.ListaEstadoCuenta = oLista;
                }
                else
                {
                    oRpta.Estado = false;
                    oRpta.Mensaje = "No hay documentos vencidos";
                    oRpta.ListaEstadoCuenta = null;
                }

            }
            catch (Exception ex)
            {
                oRpta.Estado = false;
                oRpta.Mensaje = ex.Message;
                oRpta.ListaEstadoCuenta = null;
            }
            finally
            {
                cn.Close();
            }
            return oRpta;
        }

        public DTORespuestaCliente fn_Transportista_Buscar(csCompany xCompany)
        {
            DTORespuestaCliente xRpt = new DTORespuestaCliente();

            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            DTOCliente dtoCliente;
            List<DTOCliente> oListaCli = new List<DTOCliente>();

            try
            {
                cm.Connection = cn;
                cm.CommandText = "CALL \"EXX_TPED_AgenciaTransporte_Listar\"";
                cm.CommandType = CommandType.Text;
                cn.Open();
                OdbcDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dtoCliente = new DTOCliente();
                    dtoCliente.CodigoCliente = dr["CardCode"].ToString();
                    dtoCliente.Nombre = dr["CardName"].ToString();
                    dtoCliente.Ruc = dr["LicTradNum"].ToString();
                    dtoCliente.Direccion = dr["Street"].ToString();
                    dtoCliente.CodDireccion = dr["Address"].ToString();
                    //dtoCliente.Zona = dr["U_EXX_TPED_ZONA"].ToString();

                    oListaCli.Add(dtoCliente);
                }

                dr.Close();

                if(oListaCli.Count > 0)
                {
                    xRpt.Estado = "True";
                    xRpt.Mensaje = "Se recibio la lista de transportistas";
                    xRpt.ListaCLiente = oListaCli;
                }
                else
                {
                    xRpt.Estado = "False";
                    xRpt.Mensaje = "No se encontro transportistas";
                    xRpt.ListaCLiente = null;
                }
            }
            catch(Exception ex)
            {
                xRpt.Estado = "False";
                xRpt.Mensaje = ex.Message;
                xRpt.ListaCLiente = null;
            }
            finally
            {
                cn.Close();
            }

            return xRpt;
        }

        public DTORespuesta fn_ClienteSucursal(string idSucursal, csCompany xCompany)
        {
            OdbcConnection cn = new OdbcConnection(new ConexionSAP().ConnexionHana(xCompany));
            OdbcCommand cm = new OdbcCommand();
            OdbcDataReader dr = null;

            DTORespuesta dto = new DTORespuesta();

            try
            {
                cm.CommandType = CommandType.Text;
                cm.CommandText = $"CALL \"EXX_SCT_ClienteSucursal_Listar\" ({idSucursal})";
                cm.Connection = cn;
                cn.Open();

                dr = cm.ExecuteReader();

                dr.Read();
                dto.key = dr["CardCode"].ToString();

                dr.Close();

                if (dto.key != "")
                {
                    dto.Estado = true;
                }
                else
                {
                    dto.Estado = false;
                    dto.Mensaje = "La sucursal no tiene asignado un cliente";
                }
            }
            catch (Exception ex)
            {
                dto.Estado = false;
                dto.Mensaje = "Error en la consulta: " + ex.ToString();
            }
            finally
            {
                cn.Close();
            }
            return dto;
        }
        #endregion

        #region Registros
        public string fn_Cliente_Registrar(DTOCliente cliente, csCompany xCompany)
        {
            string xRpta;
            try
            {
                xRpta = Connect(xCompany);
                if(xRpta == "")
                {
                    BusinessPartners oCliente = oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                    oCliente.CardCode = cliente.CodigoCliente;
                    oCliente.FederalTaxID = cliente.Ruc;
                    oCliente.CardName = cliente.Nombre;
                    oCliente.CardForeignName = cliente.NombreComercial;
                    oCliente.CardType = BoCardTypes.cCustomer;
                    oCliente.SalesPersonCode = Convert.ToInt32(cliente.CodVendedor);
                    oCliente.Phone1 = cliente.TelefonoCliente;
                    oCliente.Cellular = cliente.CelularCliente;
                    oCliente.EmailAddress = cliente.CorreoCliente;
                    oCliente.GroupCode = Convert.ToInt32(cliente.GrupoCliente);
                    oCliente.Currency = cliente.Currency;
                    oCliente.Industry = cliente.IndustryC;

                    oCliente.UserFields.Fields.Item("U_EXX_APELLPAT").Value = cliente.ApellidoPaterno;
                    oCliente.UserFields.Fields.Item("U_EXX_APELLMAT").Value = cliente.ApellidoMaterno;
                    oCliente.UserFields.Fields.Item("U_EXX_PRIMERNO").Value = cliente.PrimerNombre;
                    oCliente.UserFields.Fields.Item("U_EXX_SEGUNDNO").Value = cliente.SegundoNombre;
                    oCliente.UserFields.Fields.Item("U_EXX_TIPOPERS").Value = cliente.TpoPersona;
                    oCliente.UserFields.Fields.Item("U_EXX_TIPODOCU").Value = cliente.TpoDocumento;
                    oCliente.UserFields.Fields.Item("U_EXX_CONVENIO").Value = "00";
                    oCliente.UserFields.Fields.Item("U_EXX_TIPVINECO").Value = "00";

                    if (cliente.AgenteRetencion)
                        oCliente.Properties[2] = BoYesNoEnum.tYES;
                    else
                        oCliente.Properties[2] = BoYesNoEnum.tNO;

                    if (cliente.BuenContribuyente)
                        oCliente.Properties[5] = BoYesNoEnum.tYES;
                    else
                        oCliente.Properties[5] = BoYesNoEnum.tNO;

                    //Persona de contacto
                    for (int j = 0; j < cliente.ListaContacto.Count; j++)
                    {
                        oCliente.ContactEmployees.Name = cliente.ListaContacto[j].Nombre;
                        oCliente.ContactEmployees.FirstName = cliente.ListaContacto[j].PrimerNombre;
                        oCliente.ContactEmployees.MiddleName = cliente.ListaContacto[j].SegundoNombre;
                        oCliente.ContactEmployees.LastName = cliente.ListaContacto[j].Apellido;
                        oCliente.ContactEmployees.MobilePhone = cliente.ListaContacto[j].Celular;
                        oCliente.ContactEmployees.Phone1 = cliente.ListaContacto[j].Telefono;
                        oCliente.ContactEmployees.E_Mail = cliente.ListaContacto[j].Email;
                        oCliente.ContactEmployees.Position = cliente.ListaContacto[j].Cargo;

                        if (j + 1 < cliente.ListaContacto.Count)
                            oCliente.ContactEmployees.Add();
                    }

                    int contador = cliente.ListaDireccion.Select(t => t.Tipo == "B").Count();
                    if (contador == 0)
                    {
                        throw new Exception("Debe tener como minimo una dirección fiscal");
                    }

                    //Direcciones
                    for (int i = 0; i < cliente.ListaDireccion.Count; i++)
                    {
                        oCliente.Addresses.AddressType = cliente.ListaDireccion[i].Tipo == "B" ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
                        oCliente.Addresses.AddressName = cliente.ListaDireccion[i].Nombre;
                        oCliente.Addresses.Country = "PE";
                        oCliente.Addresses.State = cliente.ListaDireccion[i].Departamento;
                        oCliente.Addresses.County = cliente.ListaDireccion[i].Provincia;
                        oCliente.Addresses.ZipCode = cliente.ListaDireccion[i].Distrito;
                        oCliente.Addresses.Street = cliente.ListaDireccion[i].Direccion;
                        oCliente.Addresses.GlobalLocationNumber = cliente.ListaDireccion[i].Ubigeo;

                        if (i + 1 < cliente.ListaDireccion.Count)
                            oCliente.Addresses.Add();
                    }

                    int intrpt = oCliente.Add();

                    if (intrpt < 0)
                    {
                        xRpta = "0-" + oCompany.GetLastErrorDescription().Replace("-", " ");
                    }
                    else
                    {
                        xRpta = "1-Se registró con éxito el cliente";
                    }
                }
            }
            catch(Exception ex)
            {
                xRpta = ex.Message;
            }
            finally
            {
                //oCompany.Disconnect();
            }
            return xRpta;
        }

        public string fn_Cliente_Editar(DTOCliente cliente, csCompany xCompany)
        {
            string xRpta;
            try
            {
                xRpta = Connect(xCompany);
                if (xRpta == "")
                {
                    BusinessPartners oCliente = oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                    oCliente.GetByKey(cliente.CodigoCliente);
                    oCliente.CardName = cliente.Nombre;
                    oCliente.CardForeignName = cliente.NombreComercial;
                    oCliente.CardType = BoCardTypes.cCustomer;
                    oCliente.SalesPersonCode = Convert.ToInt32(cliente.CodVendedor);
                    oCliente.Phone1 = cliente.TelefonoCliente;
                    oCliente.Cellular = cliente.CelularCliente;
                    oCliente.EmailAddress = cliente.CorreoCliente;
                    oCliente.GroupCode = Convert.ToInt32(cliente.GrupoCliente);
                    oCliente.Currency = cliente.Currency;
                    oCliente.Industry = cliente.IndustryC;

                    oCliente.UserFields.Fields.Item("U_EXX_APELLPAT").Value = cliente.ApellidoPaterno;
                    oCliente.UserFields.Fields.Item("U_EXX_APELLMAT").Value = cliente.ApellidoMaterno;
                    oCliente.UserFields.Fields.Item("U_EXX_PRIMERNO").Value = cliente.PrimerNombre;
                    oCliente.UserFields.Fields.Item("U_EXX_SEGUNDNO").Value = cliente.SegundoNombre;
                    oCliente.UserFields.Fields.Item("U_EXX_TIPOPERS").Value = cliente.TpoPersona;
                    oCliente.UserFields.Fields.Item("U_EXX_TIPODOCU").Value = cliente.TpoDocumento;

                    if (cliente.AgenteRetencion)
                        oCliente.Properties[2] = BoYesNoEnum.tYES;
                    else
                        oCliente.Properties[2] = BoYesNoEnum.tNO;

                    if (cliente.BuenContribuyente)
                        oCliente.Properties[5] = BoYesNoEnum.tYES;
                    else
                        oCliente.Properties[5] = BoYesNoEnum.tNO;

                    bool existContacto = false;
                    //Persona de contacto
                    for (int j = 0; j < cliente.ListaContacto.Count; j++)
                    {
                        if (cliente.ListaContacto[j].FlagEliminar)
                            continue;
                        else
                            existContacto = true;

                        try
                        {
                            oCliente.ContactEmployees.SetCurrentLine(j);
                        }
                        catch
                        {
                            oCliente.ContactEmployees.Add();
                        }

                        if(cliente.ListaContacto[j].FlagEditar)
                        {
                            oCliente.ContactEmployees.Name = cliente.ListaContacto[j].Nombre;
                            oCliente.ContactEmployees.FirstName = cliente.ListaContacto[j].PrimerNombre;
                            oCliente.ContactEmployees.MiddleName = cliente.ListaContacto[j].SegundoNombre;
                            oCliente.ContactEmployees.LastName = cliente.ListaContacto[j].Apellido;
                            oCliente.ContactEmployees.MobilePhone = cliente.ListaContacto[j].Celular;
                            oCliente.ContactEmployees.Phone1 = cliente.ListaContacto[j].Telefono;
                            oCliente.ContactEmployees.E_Mail = cliente.ListaContacto[j].Email;
                            oCliente.ContactEmployees.Position = cliente.ListaContacto[j].Cargo;
                        }
                    }

                    if(!existContacto)
                    {
                        throw new Exception("Debe tener minimo un contacto");
                    }

                    //Direcciones
                    bool existDireccion = false;
                    for (int i = 0; i < cliente.ListaDireccion.Count; i++)
                    {
                        if (cliente.ListaDireccion[i].FlagEliminar)
                            continue;
                        else
                            existDireccion = true;

                        try
                        {
                            oCliente.Addresses.SetCurrentLine(i);

                            if (oCliente.Addresses.AddressName == string.Empty)
                                throw new Exception("");
                        }
                        catch
                        {
                            oCliente.Addresses.Add();
                            oCliente.Addresses.AddressName = cliente.ListaDireccion[i].Nombre;
                            string xtipo = cliente.ListaDireccion[i].Tipo == "B" ? BoAddressType.bo_BillTo.ToString() : BoAddressType.bo_ShipTo.ToString();
                            oCliente.Addresses.AddressType = cliente.ListaDireccion[i].Tipo == "B" ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
                        }

                        if(cliente.ListaDireccion[i].FlagEditar)
                        {
                            oCliente.Addresses.Country = "PE";
                            oCliente.Addresses.State = cliente.ListaDireccion[i].Departamento;
                            oCliente.Addresses.County = cliente.ListaDireccion[i].Provincia;
                            oCliente.Addresses.ZipCode = cliente.ListaDireccion[i].Distrito;
                            oCliente.Addresses.Street = cliente.ListaDireccion[i].Direccion;
                            oCliente.Addresses.GlobalLocationNumber = cliente.ListaDireccion[i].Ubigeo;
                        }
                    } 
                    
                    if(!existDireccion)
                    {
                        throw new Exception("Debe tener como minimo una dirección fiscal");
                    }
                    else
                    {
                        int contador = cliente.ListaDireccion.Select(t=>t.Tipo == "B" && t.FlagEditar).Count();
                        if(contador == 0)
                        {
                            throw new Exception("Debe tener como minimo una dirección fiscal");
                        }
                    }

                    int intrpt = oCliente.Update();

                    if (intrpt < 0)
                    {
                        oCompany.GetLastError(out intrpt, out xRpta);
                    }
                    else
                    {
                        //Eliminar Contacto
                        //Se recorre la lista de contactos del cliente  SAP
                        for (int j = 0; j < cliente.ListaContacto.Count; j++)                            
                        {
                            BusinessPartners oClienteDel = oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                            oClienteDel.GetByKey(cliente.CodigoCliente);

                            //Se recorre la lista de contactos a actualizar
                            for (int i = 0; i < oClienteDel.ContactEmployees.Count; i++)
                            {
                                oClienteDel.ContactEmployees.SetCurrentLine(i);
                                if (oClienteDel.ContactEmployees.Name == cliente.ListaContacto[j].Nombre)
                                {
                                    //Se valida que se a eliminar
                                    if (cliente.ListaContacto[j].FlagEliminar)
                                    {
                                        oClienteDel.ContactEmployees.Delete();
                                        if (oClienteDel.Update() != 0)
                                        {
                                            throw new Exception(oCompany.GetLastErrorDescription());
                                        }
                                        else
                                            break;
                                    }
                                }
                            }
                        }

                        //Eliminar Direcciones
                        for (int i = 0; i < cliente.ListaDireccion.Count; i++)
                        {
                            BusinessPartners oClienteDel = oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                            oClienteDel.GetByKey(cliente.CodigoCliente);

                            for (int j = 0; j < oClienteDel.Addresses.Count; j++)
                            {
                                oClienteDel.Addresses.SetCurrentLine(j);
                                if (oClienteDel.Addresses.AddressName == cliente.ListaDireccion[i].Nombre)
                                {
                                    if (cliente.ListaDireccion[i].FlagEliminar)
                                    {
                                        oClienteDel.Addresses.Delete();
                                        if (oClienteDel.Update() != 0)
                                        {
                                            throw new Exception(oCompany.GetLastErrorDescription());
                                        }
                                        else
                                            break;
                                    }
                                }
                            }                            
                        }

                        xRpta = "1-Se actualizó con éxito el cliente";
                    }
                }
            }
            catch (Exception ex)
            {
                xRpta = ex.Message;
            }
            finally
            {
                //oCompany.Disconnect();
            }
            return xRpta;
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
