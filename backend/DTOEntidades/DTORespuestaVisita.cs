using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    public class DTORespuestaVisita
    {
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public DTOVisitaCab Visita { get; set; }

        [DataMember]
        public List<DTOVisitaDet> ListaDetalle { get; set; }
    }
}
