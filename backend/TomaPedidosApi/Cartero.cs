using System;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;

namespace TomaPedidosApi
{
    public class Cartero
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
        public string RecuperacionPass(string correo, string user, string code)
        {
            string xRpta = string.Empty;
            try
            {
                string dominio = config.GetValue<string>("Conexion:Dominio").ToString();
                string puerto = config.GetValue<string>("Conexion:Puerto").ToString();
                string correoEnvio = config.GetValue<string>("Conexion:Email").ToString();
                string password = config.GetValue<string>("Conexion:PasswordEmail").ToString();
                string Url = config.GetValue<string>("Conexion:UrlSCT").ToString();

                string asunto = $"WEB CONSUMO - Recuperación Contraseña";
                string _url = $"{Url}lost-password/{user}";
                string mensaje = $"<p>Para reestablecer su contraseña haga clic <a href='{_url}' target='_blank'>AQUI</a> ";
                mensaje += $"<br> Su código para reestablecer es {code} </p>";

                var networkCredential = new NetworkCredential
                {
                    Password = password,
                    UserName = correoEnvio
                };

                var mailMsg = new MailMessage
                {
                    Body = mensaje,
                    Subject = asunto,
                    IsBodyHtml = true // This indicates that message body contains the HTML part as well.
                };
                mailMsg.To.Add(correo);
                mailMsg.From = new MailAddress(correoEnvio, "Imagina SAP");

                var smtpClient = new SmtpClient(dominio)
                {
                    Port = Convert.ToInt32(puerto),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = networkCredential
                };
                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                xRpta = ex.Message;
            }
            return xRpta;
        }

    }
}
