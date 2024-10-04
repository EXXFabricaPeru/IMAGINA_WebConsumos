using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTOEntidades;
using DBCAccesoDatos;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Configuration;

namespace BOCNegocio
{
    public interface IBOCPedidoVenta
    {
        DTORespuestaPedido fn_PedidoVenta_Listar(string CodVendedor, string FecIni, string FecFin, string Tipo, string cliente, string Estado, string product, csCompany xCompany);
        DTORespuestaPedido fn_TrackingPedidoVenta_Listar(string FecIni, string FecFin, string CodVendedor, string CodCliente, csCompany xCompany);
        DTORespuestaPedido fn_PedidoVenta_Buscar(string pNro, string pTipo, csCompany xCompany);
        DTORespuestaPedido fn_OcDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany);
        DTORespuestaPedido fn_FTDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany);
        DTORespuesta fn_PedidoVentaRegistrar(DTOPedidoVentaCab Pedido, csCompany xCompany);
        DTORespuesta fn_PedidoVenta_Anular(DTOPedidoVentaCab pedido, csCompany xCompany);
    }

    public class BOCPedidoVenta : IBOCPedidoVenta
    {
        #region Metodos No Transaccionales
        public DTORespuestaPedido fn_PedidoVenta_Listar(string CodVendedor, string FecIni, string FecFin, string Tipo, string cliente, string Estado, string product, csCompany xCompany)
        {
            DTORespuestaPedido oLista = new DTORespuestaPedido();
            IDBCPedidoVenta dbcPedido = new DBCPedidoVenta();
            try
            {
                oLista = dbcPedido.fn_PedidoVenta_Listar(CodVendedor, FecIni, FecFin, Tipo, cliente, Estado, product, xCompany);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }
            
            return oLista;
        }

        public DTORespuestaPedido fn_TrackingPedidoVenta_Listar(string FecIni, string FecFin, string CodVendedor, string CodCliente, csCompany xCompany)
        {
            DTORespuestaPedido oLista = new DTORespuestaPedido();
            IDBCPedidoVenta dbcPedido = new DBCPedidoVenta();
            try
            {
                oLista = dbcPedido.fn_TrackingPedidoVenta_Listar(FecIni, FecFin, CodVendedor, CodCliente, xCompany);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }
            
            return oLista;
        }

        public DTORespuestaPedido fn_PedidoVenta_Buscar(string pNro, string pTipo, csCompany xCompany)
        {
            DTORespuestaPedido oLista = new DTORespuestaPedido();
            IDBCPedidoVenta dbcPedido = new DBCPedidoVenta();
            try
            {
                oLista = dbcPedido.fn_PedidoVenta_Buscar(pNro, pTipo, xCompany);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }

            return oLista;
        }
        #endregion

        #region Metodos Transaccionales
        public DTORespuesta fn_PedidoVentaRegistrar(DTOPedidoVentaCab Pedido, csCompany xCompany)
        {
            DTORespuesta dto = new DTORespuesta();
            IDBCPedidoVenta dbcPedido = new DBCPedidoVenta();

            string _xResult;

            try
            {
                _xResult = dbcPedido.fn_PedidoVentaRegistrar(Pedido, xCompany);

                if (!_xResult.Contains('-'))
                {
                    dto.Estado = false;
                    dto.Mensaje = _xResult;
                }
                else
                {
                    string[] xValor = _xResult.Split('-');

                    dto.Estado = true;
                    dto.Mensaje = xValor[0];
                    dto.key = xValor[1];
                }
            }
            catch (Exception ex)
            {
                dto.Estado = false;
                dto.Mensaje = "Se produjo un error: " + ex.Message;
                //BOCErrorControl.RegistraError(dto.Mensaje);
            }

            return dto;
        }

        public DTORespuesta fn_PedidoVenta_Anular(DTOPedidoVentaCab pedido, csCompany xCompany)
        {
            IDBCPedidoVenta dbc = new DBCPedidoVenta();
            return dbc.fn_PedidoVenta_Anular(pedido, xCompany);
        }

        public DTORespuestaPedido fn_OcDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany)
        {
            IDBCPedidoVenta dbc = new DBCPedidoVenta();
            return dbc.fn_OcDisponible_Listar(empresa, proyecto, etapa, articulo, xCompany);
        }

        public DTORespuestaPedido fn_FTDisponible_Listar(string empresa, string proyecto, string etapa, string articulo, csCompany xCompany)
        {
            IDBCPedidoVenta dbc = new DBCPedidoVenta();
            return dbc.fn_FTDisponible_Listar(empresa, proyecto, etapa, articulo, xCompany);
        }
        #endregion

    }
}
