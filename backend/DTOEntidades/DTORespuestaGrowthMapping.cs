using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    public class DTORespuestaGrowthMapping
    {
        [DataMember(Order = 0)]
        public string Estado { get; set; }

        [DataMember(Order = 1)]
        public string Mensaje { get; set; }

        [DataMember(Order = 2)]
        public List<DTOGrowthMapping> ListaMedicion { get; set; }
    }
}
