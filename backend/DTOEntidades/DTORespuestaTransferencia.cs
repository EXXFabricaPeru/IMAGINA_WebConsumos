using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTORespuestaTransferencia
    {
        [DataMember]
        public bool Estado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public List<DTOTransferenciaCab> ListaTransferencia { get; set; }
    }
}