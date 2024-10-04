using DBCAccesoDatos;
using DTOEntidades;
using System.Collections.Generic;

namespace BOCNegocio
{
    public interface IBOCTransportista
    {
        List<DTORespuestaTransportista> fn_Transportista_Listar(string pValor);
    }

    public class BOCTransportista : IBOCTransportista
    {
        public List<DTORespuestaTransportista> fn_Transportista_Listar(string pValor)
        {
            IDBCTransportista dbcTransportista = new DBCTransportista();
            return dbcTransportista.fn_Transportista_Listar(pValor);
        }
    }
}
