using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOTomaInventarioDet
    {
        [DataMember(Order = 0)]
        public int IdToma { get; set; }

        [DataMember(Order = 1)]
        public string CodArticulo { get; set; }

        [DataMember(Order = 2)]
        public string FecUltCompra { get; set; }

        [DataMember(Order = 3)]
        public int CantComprada { get; set; }

        [DataMember(Order = 4)]
        public int CantRegistrada { get; set; }

        [DataMember(Order = 5)]
        public int CantActual { get; set; }

        [DataMember(Order = 6)]
        public string FecUltRegistro { get; set; }

        [DataMember(Order = 7)]
        public string UserReg { get; set; }
    }
}
