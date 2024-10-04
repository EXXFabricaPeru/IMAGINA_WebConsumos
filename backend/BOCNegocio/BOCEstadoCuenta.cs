using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using Etna.DBCAccesoDatos;
using System.Net;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;
using Etna.BOCNegocio;
using EtnaAgenteMaestros;
using EtnaAgenteMaestros.SrvMaestro;

namespace Etna.BOCNegocio
{
    public interface IBOCEstadoCuenta
    {
        List<DTORespuestaEstadoCuenta> fn_EstadoCuenta_Buscar(string CodVendedor, string CodCliente, string Tipo);
        string fn_EstadoCuenta_Enviar(string Vendedor, string CodCliente);
    }

    public class BOCEstadoCuenta : IBOCEstadoCuenta
    {
        public List<DTORespuestaEstadoCuenta> fn_EstadoCuenta_Buscar(string CodVendedor, string CodCliente, string Tipo)
        {
            IDBCEstadoCuenta dbc = new DBCEstadoCuenta();
            List<DTORespuestaEstadoCuenta> oLista = new List<DTORespuestaEstadoCuenta>();

            try
            {
                oLista = dbc.fn_EstadoCuenta_Buscar(CodVendedor, CodCliente, Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oLista;
        }

        public string fn_EstadoCuenta_Enviar(string Vendedor, string CodCliente)
        {
            string xRpt = "";
            string xLinea = "";
            try
            {
                #region Comentado
                //string xCorreoCliente = new DBCTablaGeneral().fn_CorreoCliente_Buscar(CodCliente);
                ////string xCorreoVendedor = "cubillus@etna.com.pe";//Vendedor+"@etna.com.pe";

                ////Uri uriDownload = new Uri("http://192.168.200.238/webwcf/Mod_ConsultasSAP/Cobranzas/VEN_EstadoCuenta_Reporte.aspx?pCliente=" + CodCliente + "&pCorreo=" + xCorreoCliente + "|" + Vendedor);
                //Uri uriDownload = new Uri("http://localhost:29285/Etna.Web/Mod_ConsultasSAP/Cobranzas/VEN_EstadoCuenta_Reporte.aspx?pCliente=" + CodCliente + "&pCorreo=" + xCorreoCliente + "|" + Vendedor);

                //WebClient wcli = new WebClient();

                //WebRequest r = WebRequest.Create(uriDownload);
                //WebResponse resp = r.GetResponse();
                //using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                //{
                //    Console.WriteLine(sr.ReadToEnd());
                //}
                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("P_CodCli", CodCliente);

                //ReportViewer reportViewer1 = new ReportViewer();

                //reportViewer1.ShowParameterPrompts = false;
                //reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                //reportViewer1.ServerReport.ReportServerUrl = new Uri("http://192.168.200.234/ReportServer_ETNABI");
                //reportViewer1.ServerReport.ReportPath = "/Cobranzas/COB035";

                ////reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //NetworkCredential myCred = new NetworkCredential("adminbi", "adminbietna", "etna.com.pe");
                //reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = myCred;

                //reportViewer1.ServerReport.SetParameters(parameters);

                //string mimeType;
                //string encoding;
                //string extension;
                //string[] streams;
                //Warning[] warnings;

                //byte[] bytes = reportViewer1.ServerReport.Render("PDF", string.Empty, out mimeType, out encoding, out extension, out streams, out warnings);
                #endregion
                xLinea = "87 "; List<string> oParametros = new List<string>();
                xLinea = "88 "; string xParametro = "P_CodCli|" + CodCliente;
                xLinea = "89"; oParametros.Add(xParametro);

                xLinea = "91"; byte[] bytes; AgenteMaestros.fn_ReporteParaExtranet_Buscar(210, oParametros, out bytes, out xRpt);

                xLinea = "93"; string fileName = @"C:\Archivos\" + CodCliente + ".pdf";
                xLinea = "95";
                if (File.Exists(fileName))
                    File.Delete(fileName);
                xLinea = "98";
                if(xRpt!="") new Exception(xRpt);

                using (FileStream stream = File.OpenWrite(fileName))
                {
                    xLinea = "102"; stream.Write(bytes, 0, bytes.Length);
                }

                xLinea = "105"; EnviarEmailEstadoCuenta(Vendedor, CodCliente);

            }
            catch(Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
                xRpt = xLinea + xRpt + "||" + ex.Message;
            }

            return xRpt;
        }

        public void EnviarEmailEstadoCuenta(string xVendedor, string xCliente)
        {
            string cuerpo;
            cuerpo = "Estado de cuenta al " + DateTime.Now.ToShortDateString();

            try
            {
                string xCorreoCliente = new DBCTablaGeneral().fn_CorreoCliente_Buscar(xCliente);
                
                List<DTORespuestaCliente> _xCliente = new DBCCliente().fn_ClienteVendedor_Listar(2, xCliente, "0", "19990101");

                string xCorreoVendedor = new DBCTablaGeneral().fn_CorreoVendedor_Buscar(xVendedor);

                //string asunto = "Estado Cuenta del cliente " + _xCliente[0].ListaCLiente[0].Nombre;    //Asunto del correo
                //string User = Properties.Settings.Default.User;
                //string Password = Properties.Settings.Default.Password;
                //string Host = Properties.Settings.Default.Host;
                //string ConCopia = "cubillus@etna.com.pe,rojeda@etna.com.pe";
                //string MailAccount = xCorreoVendedor;                                      //Cuenta a quien se le envia el correo

                string asunto = "Estado Cuenta del cliente " + _xCliente[0].ListaCLiente[0].Nombre;    //Asunto del correo
                string User = "infoetna@etna.com.pe";// ConfigurationManager.AppSettings.Get("User");
                string Password = "$3tInf0n@365$";// ConfigurationManager.AppSettings.Get("Password");
                string Host = "smtp.office365.com";// ConfigurationManager.AppSettings.Get("Host");
                string ConCopia = "cubillus@etna.com.pe,rojeda@etna.com.pe";
                string MailAccount = xCorreoCliente + "," + xCorreoVendedor;                                      //Cuenta a quien se le envia el correo

                //Estructura del mensaje
                MailMessage msg = new MailMessage();
                msg.To.Add(MailAccount);
                msg.Bcc.Add(ConCopia);
                msg.From = new MailAddress(User);
                msg.Subject = asunto;
                msg.IsBodyHtml = true;
                msg.Body = cuerpo;

                string fichero = @"C:\Archivos\" + xCliente + ".pdf";

                // Adjuntado del fichero a la colección Attachments

                msg.Attachments.Add(new Attachment(fichero));

                //Servidor del mensaje
                //SmtpClient clienteSmtp = new SmtpClient(Host);

                //clienteSmtp.Credentials = new NetworkCredential(User, Password);
                //clienteSmtp.Send(msg);
                SmtpClient clienteSmtp = new SmtpClient(Host);
                clienteSmtp.Port = 587;
                clienteSmtp.EnableSsl = true;
                clienteSmtp.UseDefaultCredentials = true;
                clienteSmtp.Credentials = new NetworkCredential(User, Password);
                clienteSmtp.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
