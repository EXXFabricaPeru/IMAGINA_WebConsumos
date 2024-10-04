using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTORespuestaBateria
    {
        [DataMember(Order = 0)]
        public string Estado { get; set; }

        [DataMember(Order = 1)]
        public string Mensaje { get; set; }

        [DataMember(Order = 2)]
        public List<DTOBateria> ListaBateria { get; set; }
    }
}