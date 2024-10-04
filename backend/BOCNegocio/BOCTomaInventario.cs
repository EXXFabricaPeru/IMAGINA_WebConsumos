using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using Etna.DBCAccesoDatos;

namespace Etna.BOCNegocio
{
    public interface IBOCTomaInventario 
    {
        List<DTORespuestaTomaInventario> fn_TomaInventario_Buscar(string pCliente, string pAlmacen, string pArticulo, string pFecha);
        List<DTORespuesta> fn_TomaInventario_Registrar(DTOTomaInventarioCab oTomaInventario);
    }
    public class BOCTomaInventario:IBOCTomaInventario
    {
        public List<DTORespuestaTomaInventario> fn_TomaInventario_Buscar(string pCliente, string pAlmacen, string pArticulo, string pFecha)
        {
            IDBCTomaInventario oDbTomaInventario = new DBCTomaInventario();
            return oDbTomaInventario.fn_TomaInventario_Buscar(pCliente, pAlmacen, pArticulo, pFecha);
        }

        public List<DTORespuesta> fn_TomaInventario_Registrar(DTOTomaInventarioCab oTomaInventario)
        {
            List<DTORespuesta> oListaRespuesta = new List<DTORespuesta>();
            DTORespuesta oToRespuesta = new DTORespuesta();

            try
            {
                IDBCTomaInventario oDbTomaInventario = new DBCTomaInventario();
                string xResult = oDbTomaInventario.fn_TomaInventario_Registrar(oTomaInventario);
                string[] xValor = xResult.Split('|');

                oToRespuesta.Estado = xValor[0];
                oToRespuesta.Mensaje = xValor[1];

                oListaRespuesta.Add(oToRespuesta);
            }
            catch(Exception ex)
            {
                oToRespuesta.Estado = "false";
                oToRespuesta.Mensaje = ex.Message;
                BOCErrorControl.RegistraError(ex.Message);
                oListaRespuesta.Add(oToRespuesta);
            }

            return oListaRespuesta;
        }
    }
}
