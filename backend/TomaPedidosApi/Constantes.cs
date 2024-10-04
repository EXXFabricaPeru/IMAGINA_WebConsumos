using DTOEntidades;
using Microsoft.Extensions.Configuration;

namespace TomaPedidosApi
{
    public class Constantes
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        public csCompany GetConexion()
        {
            csCompany conSap = new csCompany
            {
                ServerBD = config.GetValue<string>("Conexion:Server").ToString(),
                UserBD = config.GetValue<string>("Conexion:UserDb").ToString(),
                PwBD = config.GetValue<string>("Conexion:PassDb").ToString(),
                ServerLic = config.GetValue<string>("Conexion:ServerLicencia").ToString(),
                NameBD = config.GetValue<string>("Conexion:BaseDatos").ToString(),
                UserSAP = config.GetValue<string>("Conexion:UserSap").ToString(),
                PwSAP = config.GetValue<string>("Conexion:PassSap").ToString(),
                ServerType = config.GetValue<string>("Conexion:TipoBD").ToString()
            };

            return conSap;
        }

        
    }
}
