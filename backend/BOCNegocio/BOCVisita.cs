using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using Etna.DBCAccesoDatos;

namespace Etna.BOCNegocio
{
    public interface IBOCVisita
    {
        List<DTORespuestaVisita> fn_Visita_Buscar(string pVendedor, string pCliente, string pAlmacen, string pFecha);

        List<DTORespuesta> fn_Visita_Registrar(DTOVisitaCab Visita);
    }

    public class BOCVisita : IBOCVisita
    {
        public List<DTORespuesta> fn_Visita_Registrar(DTOVisitaCab Visita)
        {
            List<DTORespuesta> oLista = new List<DTORespuesta>();
            DTORespuesta dto = new DTORespuesta(); 

            try
            {
                IDBCVisita dbc = new DBCVisita();
                //DTOVisitaCab dtoCab = Json.fn_DeserializarVisitaJson(Visita);

                //string xResult = dbc.fn_Visita_Registrar(dtoCab);
                string xResult = dbc.fn_Visita_Registrar(Visita);
                string[] xValor = xResult.Split('|');

                dto.Estado = xValor[0];
                dto.Mensaje = xValor[1];

                oLista.Add(dto);
            }
            catch (Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = ex.Message;
                BOCErrorControl.RegistraError(ex.Message);
                oLista.Add(dto);
            }

            return oLista;
        }

        public List<DTORespuestaVisita> fn_Visita_Buscar(string pVendedor, string pCliente, string pAlmacen, string pFecha)
        {
            IDBCVisita dbc = new DBCVisita();
            List<DTORespuestaVisita> oLista = dbc.fn_Visita_Buscar(pVendedor,pCliente,pAlmacen,pFecha);
            return oLista;
        }
    }
}
