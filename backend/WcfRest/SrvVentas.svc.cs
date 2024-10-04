using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTOEntidades;
using BOCNegocio;

namespace WcfRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SrvVentas" in code, svc and config file together.
    public class SrvVentas : ISrvVentas
    {
        #region Articulo
        public DTORespuestaArticulo[] fn_Articulo_Listar(string valor)
        {
            IBOCArticulo oBoArticulo = new BOCArticulo();
            return oBoArticulo.fn_Articulo_Listar(valor).ToArray();
        }
        #endregion 

        #region Cliente
        //public DTORespuestaCliente[] BuscarCliente(string pValor, string pVendedor,string pFecha)
        public DTORespuestaCliente[] BuscarCliente(string pValor, string pVendedor)
        {
            IBOCCliente bocCliente = new BOCCliente();            
            return bocCliente.fn_ClienteVendedor_Listar(pValor, pVendedor).ToArray();
        }

        #endregion

        #region Maestros        
        public DTORespuestaTablaGeneral[] ListarAlmacenCliente(string Codigo)
        {
            IBOCTablaGeneral bocPedidoVenta = new BOCTablaGeneral();
            return bocPedidoVenta.fn_AlmacenCliente_Listar(Codigo).ToArray();
        }

        public DTORespuestaTablaGeneral[] CondicionPago_Listar(string Codigo)
        {
            IBOCTablaGeneral bocTabGen = new BOCTablaGeneral();
            return bocTabGen.CondicionPago_Listar(Codigo).ToArray();
        }
        #endregion

        #region Pedido Venta
        public DTORespuestaPedido[] ObtenerUltimosPedidos(string CodVendedor, string CodCliente)
        {
            IBOCPedidoVenta bocPedido = new BOCPedidoVenta();
            return bocPedido.fn_PedidoVenta_Listar(CodVendedor, CodCliente).ToArray();
        }

        public DTORespuestaPedido[] TrakingPedvta(string FecIni, string FecFin, string CodVendedor, string CodCliente)
        {
            IBOCPedidoVenta bocPedido = new BOCPedidoVenta();
            return bocPedido.fn_TrackingPedidoVenta_Listar(FecIni, FecFin, CodVendedor, CodCliente).ToArray();
        }

        public DTORespuestaPedido[] fn_PedidoVenta_Duplicar(string Pedido)
        {
            IBOCPedidoVenta bocPedido = new BOCPedidoVenta();
            return bocPedido.fn_PedidoVenta_Duplicar(Pedido).ToArray();
        }

        public DTORespuesta[] PedidoVentaEnviar(DTOPedidoVentaCab Pedido)
        {
            List<DTORespuesta> oLista = new List<DTORespuesta>();
            DTORespuesta dto = new DTORespuesta();

            try
            {
                if (Pedido == null)
                    new Exception("No llego nada");

                IBOCPedidoVenta bocPedidoVenta = new BOCPedidoVenta();

                oLista = bocPedidoVenta.fn_PedidoVentaRegistrar(Pedido);
            }
            catch (Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = ex.Message;
                oLista.Add(dto);
            }

            return oLista.ToArray();
        }
        #endregion
                
        #region Transportista
        public DTORespuestaTransportista[] ConsultarTramo(string Valor)
        {
            IBOCTransportista bocTrans = new BOCTransportista();
            //return bocTrans.fn_Transportista_Listar(Valor, "D", Fecha).ToArray();
            return bocTrans.fn_Transportista_Listar(Valor).ToArray();
        }
        #endregion

        
    }
}
