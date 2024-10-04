using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTODistrito
    {
        [DataMember(Order = 0)]
        public string pr_CodDistrito { get; set; }

        [DataMember(Order = 1)]
        public string pr_Distrito { get; set; }

        [DataMember(Order = 2)]
        public List<DTOTienda> ListaTienda { get; set; }
    }
}
