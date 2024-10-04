using BOCNegocio;
using DTOEntidades;
using Microsoft.AspNetCore.Mvc;

namespace TomaPedidosApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        [HttpGet]
        public DTORespuestaPedido Get(string id, string tipo)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();            
            return boc.fn_PedidoVenta_Buscar(id, tipo, conSap); 
        }

        [HttpGet("ListaOC")]
        public DTORespuestaPedido GetListaOC(string empresa, string proyecto, string etapa, string articulo)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_OcDisponible_Listar(empresa, proyecto, etapa, articulo, conSap);
        }
        [HttpGet("ListaFT")]
        public DTORespuestaPedido GetListaFT(string empresa, string proyecto, string etapa, string articulo)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_FTDisponible_Listar(empresa, proyecto, etapa, articulo, conSap);
        }
        [HttpGet("Lista")]
        public DTORespuestaPedido GetLista(string CodVendedor, string FecIni, string FecFin, string Tipo, string cliente, string Estado, string product)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_PedidoVenta_Listar(CodVendedor, FecIni, FecFin, Tipo, cliente, Estado, product, conSap);
        }

        [HttpGet("Tracking")]
        public DTORespuestaPedido GetTraking(string fecini, string fecfin, string codvendedor, string codcliente)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_TrackingPedidoVenta_Listar(fecini, fecfin, codvendedor, codcliente, conSap);
        }

        [HttpPost]
        public DTORespuesta Post(DTOPedidoVentaCab pedido)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_PedidoVentaRegistrar(pedido, conSap);
        }

        [HttpPut]
        public DTORespuesta Put(DTOPedidoVentaCab pedido)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCPedidoVenta boc = new BOCPedidoVenta();
            return boc.fn_PedidoVenta_Anular(pedido, conSap);
        }
        
    }
}
