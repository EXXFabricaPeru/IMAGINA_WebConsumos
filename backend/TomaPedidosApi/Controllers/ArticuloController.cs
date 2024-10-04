using BOCNegocio;
using DTOEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TomaPedidosApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ArticuloController : Controller
    {
        [HttpGet]
        public DTORespuestaArticulo Get(string valor, string almacen, string listprice, string moneda, string flagStock)
        {
            csCompany conSap = new Constantes().GetConexion();
            DTORespuestaArticulo dto = new DTORespuestaArticulo();
            try
            {
                //IBOCArticulo boc = new BOCArticulo();
                dto = new BOCArticulo().fn_Articulo_Listar(valor, almacen, listprice, moneda, flagStock, conSap);
            }
            catch(Exception ex)
            {
                dto.Mensaje = ex.Message;
                dto.Estado = "False";
            }

            return dto;
            //return boc.fn_Articulo_Listar(valor, almacen, listprice, moneda, flagStock, conSap);
        }
        
        [HttpGet("precio")]
        public DTORespuestaArticulo GetPrecio(string listPrice, string moneda, string undMed, string codigoArt)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCArticulo boc = new BOCArticulo();
            return boc.fn_ObtenerPrecio(listPrice, moneda, undMed, codigoArt, conSap);
        }
        
        [HttpGet("stockund")]
        public DTORespuestaArticulo GetStockUnd(string almacen, string undMed, string codigoArt)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCArticulo boc = new BOCArticulo();
            return boc.fn_ObtenerStockUndMed(almacen, undMed, codigoArt, conSap);
        }

        [HttpGet("stock")]
        public DTORespuestaArticulo GetStock(string codigoArt, string sucursal)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCArticulo boc = new BOCArticulo();
            return boc.fn_StockAlmacen(codigoArt, sucursal, conSap);
        }
        
    }
}
