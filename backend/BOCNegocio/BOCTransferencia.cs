using DBCAccesoDatos;
using DTOEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOCNegocio
{
    public interface IBOCTransferencia
    {
        DTORespuesta fn_SolicitudTransferencia_Registrar(DTOTransferenciaCab transferencia, csCompany xCompany);
        DTORespuesta fn_SolicitudTransferencia_Anular(DTOTransferenciaCab transferencia, csCompany xCompany);
        DTORespuestaTransferencia fn_SolicitudTransferencia_Buscar(string docEntry, csCompany xCompany);
        DTORespuestaTransferencia fn_SolicitudTransferencia_Listar(string fecDesde, string fecHasta, string estado, string user, string compania, string product, csCompany xCompany);
    }
    public class BOCTransferencia : IBOCTransferencia
    {
        public DTORespuestaTransferencia fn_SolicitudTransferencia_Buscar(string docEntry, csCompany xCompany)
        {
            IDBCTransferencia dbc = new DBCTransferencia();
            DTORespuestaTransferencia rpta = new DTORespuestaTransferencia();
            List<DTOTransferenciaCab> oLista = new List<DTOTransferenciaCab>();
            DTOTransferenciaCab transferencia;
            try
            {
                transferencia = dbc.fn_SolicitudTransferencia_Buscar(docEntry, xCompany);
                oLista.Add(transferencia);
                rpta.Mensaje = "";
                rpta.Estado = true;
                rpta.ListaTransferencia = oLista;
            }
            catch(Exception ex)
            {
                transferencia = null;
                rpta.Mensaje = ex.Message;
                rpta.Estado = false;
                rpta.ListaTransferencia = null;
            }
            
            return rpta;
        }

        public DTORespuestaTransferencia fn_SolicitudTransferencia_Listar(string fecDesde, string fecHasta, string estado, string user, string compania, string product, csCompany xCompany)
        {
            IDBCTransferencia dbc = new DBCTransferencia();
            DTORespuestaTransferencia rpta = new DTORespuestaTransferencia();
            
            try
            {
                List<DTOTransferenciaCab> oLista = dbc.fn_SolicitudTransferencia_Listar(fecDesde, fecHasta, estado, user, compania, product, xCompany);                
                rpta.Mensaje = "";
                rpta.Estado = true;
                rpta.ListaTransferencia = oLista;
            }
            catch (Exception ex)
            {
                rpta.Mensaje = ex.Message;
                rpta.Estado = false;
                rpta.ListaTransferencia = null;
            }

            return rpta;
        }

        public DTORespuesta fn_SolicitudTransferencia_Registrar(DTOTransferenciaCab transferencia, csCompany xCompany)
        {
            IDBCTransferencia dbc = new DBCTransferencia();
            DTORespuesta dto = dbc.fn_SolicitudTransferencia_Registrar(transferencia, xCompany);
            return dto;
        }

        public DTORespuesta fn_SolicitudTransferencia_Anular(DTOTransferenciaCab transferencia, csCompany xCompany)
        {
            IDBCTransferencia dbc = new DBCTransferencia();
            DTORespuesta dto = dbc.fn_SolicitudTransferencia_Anular(transferencia, xCompany);
            return dto;
        }
    }
}
