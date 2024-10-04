using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using Etna.DBCAccesoDatos;

namespace Etna.BOCNegocio
{
    public interface IBOCBateria
    {
        List<DTORespuestaBateria> fn_BateriaPedido_Listar(string CodCliente, string CodMarca, string CodMoneda, string pFecha);
        List<DTORespuestaBateria> fn_ArticuloVenta_Listar(string CodMarca);
        List<DTORespuestaBateria> fn_ModelosPedido_Listar(string CodCliente, string CodMoneda, string pFecha);
    }
    public class BOCBateria : IBOCBateria
    {
        public List<DTORespuestaBateria> fn_BateriaPedido_Listar(string CodCliente, string CodMarca, string CodMoneda, string pFecha)
        {
            IDBCBateria dbcBateria = new DBCBateria();
            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();

            try
            {
                oLista = dbcBateria.fn_BateriaPedido_Listar(CodCliente, CodMarca, CodMoneda, pFecha);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }          

            return oLista;
        }

        public List<DTORespuestaBateria> fn_ArticuloVenta_Listar(string CodMarca)
        {
            IDBCBateria dbcBateria = new DBCBateria();
            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();

            try
            {
                oLista = dbcBateria.fn_ArticuloVenta_Listar(CodMarca);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }          

            return oLista;            
        }

        public List<DTORespuestaBateria> fn_ModelosPedido_Listar(string CodCliente, string CodMoneda, string pFecha)
        {
            IDBCBateria dbcBateria = new DBCBateria();
            List<DTORespuestaBateria> oLista = new List<DTORespuestaBateria>();

            try
            {
                oLista = dbcBateria.fn_ModelosPedido_Listar(CodCliente, CodMoneda, pFecha);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }          

            return oLista;
        }
    }
}
