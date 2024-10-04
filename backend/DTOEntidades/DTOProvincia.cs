using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOProvincia
    {
        [DataMember(Order = 0)]
        public string pr_CodProvincia { get; set; }

        [DataMember(Order = 1)]
        public string pr_Provincia { get; set; }

        [DataMember(Order = 2)]
        public List<DTODistrito> ListaDistrito { get; set; }
    }
}
