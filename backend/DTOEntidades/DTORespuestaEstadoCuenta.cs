using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTORespuestaEstadoCuenta
    {
        [DataMember]
        public bool Estado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public List<DTOEstadoCuenta> ListaEstadoCuenta { get; set; }
    }
}