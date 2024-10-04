using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTORespuestaEmpleado
    {
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
        
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public List<DTOEmpleado> ListaEmpleado { get; set; }
    }
}
