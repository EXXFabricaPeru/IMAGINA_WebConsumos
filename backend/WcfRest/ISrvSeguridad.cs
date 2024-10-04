using DTOEntidades;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISrvSeguridad" in both code and config file together.
    [ServiceContract]
    public interface ISrvSeguridad
    {
        [WebGet(UriTemplate = "/Login/{usuario},{contrasenia}",
                ResponseFormat= WebMessageFormat.Json)]
        DTORespuestaEmpleado[] Login(string usuario, string contrasenia);
    }
}
