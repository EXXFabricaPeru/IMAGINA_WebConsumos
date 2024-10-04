using BOCNegocio;
using DTOEntidades;

namespace WcfRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SrvSeguridad" in code, svc and config file together.
    public class SrvSeguridad : ISrvSeguridad
    {
        public DTORespuestaEmpleado[] Login(string usuario, string contrasenia)
        {
            IBOCSeguridad bocSeg = new BOCSeguridad();
            return bocSeg.fn_BuscarUsuario(usuario, contrasenia).ToArray();
        }

    }
}
