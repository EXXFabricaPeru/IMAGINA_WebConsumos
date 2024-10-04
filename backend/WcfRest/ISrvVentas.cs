using DTOEntidades;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfRest
{
    [ServiceContract]
    public interface ISrvVentas
    {
        #region Articulo
        /// <summary>
        /// Lista los articulos comprados del mes
        /// </summary>
        /// <param name="Cliente"></param>
        /// <param name="Almacen"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "ListarUtimaCompraArticulos/{Valor}",
            ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaArticulo[] fn_Articulo_Listar(string Valor);
        #endregion

        #region Cliente
        /// <summary>
        /// Busca los clientes de un vendedor
        /// </summary>
        /// <param name="pValor"></param>
        /// <param name="pVendedor"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/BuscarCliente/{pValor},{pVendedor}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaCliente[] BuscarCliente(string pValor, string pVendedor);       
        #endregion

        #region Maestros        
        /// <summary>
        /// Lista los almacenes de los clientes
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>        
        [OperationContract]
        [WebGet(UriTemplate = "/ListarAlmacenCliente/{Codigo}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaTablaGeneral[] ListarAlmacenCliente(string Codigo);

        /// <summary>
        /// Lista las condiciones de pago
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/ListaCondPago/{Codigo}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaTablaGeneral[] CondicionPago_Listar(string Codigo);
        #endregion

        #region Pedido Venta
        /// <summary>
        /// Obtiene los ultimos 10 pedidos del cliente
        /// </summary>
        /// <param name="CodVendedor"></param>
        /// <param name="CodCliente"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerUltimosPedidos/{CodVendedor},{CodCliente}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaPedido[] ObtenerUltimosPedidos(string CodVendedor, string CodCliente);

        
        /// <summary>
        /// Los movimientos de los pedidos del cliente
        /// </summary>
        /// <param name="FecIni"></param>
        /// <param name="FecFin"></param>
        /// <param name="CodVendedor"></param>
        /// <param name="CodCliente"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/TrakingPedvta/{FecIni},{FecFin},{CodVendedor},{CodCliente}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaPedido[] TrakingPedvta(string FecIni, string FecFin, string CodVendedor, string CodCliente);

        /// <summary>
        /// Obtiene el detalle del pedido a duplicar con el precio actual
        /// </summary>
        /// <param name="Pedido"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPedidoDuplicar/{Pedido}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaPedido[] fn_PedidoVenta_Duplicar(string Pedido);
       
        /// <summary>
        /// Graba el pedido de venta
        /// </summary>
        /// <param name="Pedido"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/PedidoVentaEnviar",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   Method = "POST")]
        DTORespuesta[] PedidoVentaEnviar(DTOPedidoVentaCab Pedido);
        #endregion

        #region Transportista
        /// <summary>
        /// Trae los transportistas 
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        //[OperationContract]
        //[WebGet(UriTemplate = "/ConsultarTramo/{Valor},{Fecha}",
        //        ResponseFormat = WebMessageFormat.Json)]
        //DTORespuestaTransportista[] ConsultarTramo(string Valor, string Fecha);

        [OperationContract]
        [WebGet(UriTemplate = "/ConsultarTramo/{Valor}",
                ResponseFormat = WebMessageFormat.Json)]
        DTORespuestaTransportista[] ConsultarTramo(string Valor);
        #endregion

        
    }
}
