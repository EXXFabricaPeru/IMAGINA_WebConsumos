using BOCNegocio;
using DTOEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TomaPedidosApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        [HttpGet]
        public DTORespuesta Get(string idSucursal)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_ClienteSucursal(idSucursal, conSap);
        }

        /*
        [HttpGet("Buscar")]
        public DTORespuestaCliente GetBuscar(string codCliente)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_Cliente_Buscar(codCliente, conSap);
        }
        
        [HttpGet("EeCcLista")]
        public DTORespuestaEstadoCuenta GetEecc(string codCliente)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_EstadoCuenta_Listar(codCliente, conSap);
        }
        
        [HttpGet("transportista")]
        public DTORespuestaCliente GetTransportista()
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_Transportista_Buscar(conSap);
        }

        [HttpPost]
        public DTORespuesta Post(DTOCliente cliente)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_Cliente_Registrar(cliente, conSap);
        }

        [HttpPut]
        public DTORespuesta Put(DTOCliente cliente)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCCliente boc = new BOCCliente();
            return boc.fn_Cliente_Editar(cliente, conSap);
        }
        */
    }
}
