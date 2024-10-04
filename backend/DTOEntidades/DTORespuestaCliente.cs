using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTORespuestaCliente
    {
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public List<DTOCliente> ListaCLiente { get; set; }
    }
}