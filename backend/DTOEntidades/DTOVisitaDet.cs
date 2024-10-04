using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOVisitaDet
    {
        [DataMember]
        public int IdVisita { get; set; }

        [DataMember]
        public int IdMotivo { get; set; }

        [DataMember]
        public string Motivo { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public string UserReg { get; set; }
    }
}
