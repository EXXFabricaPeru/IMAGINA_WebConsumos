using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOTransportistaDet
    {
        [DataMember]
        public string pr_Ruc { get; set; }
        [DataMember]
        public string pr_Departamento { get; set; }
        [DataMember]
        public string pr_Provincia { get; set; }
        [DataMember]
        public string pr_Distrito { get; set; }
        [DataMember]
        public string pr_Direccion { get; set; }
        [DataMember]
        public string pr_Telefono { get; set; }
        [DataMember]
        public string pr_UserReg { get; set; }
        [DataMember]
        public string pr_FecReg { get; set; }
        [DataMember]
        public string pr_Referencia { get; set; }
        [DataMember]
        public string pr_Flag { get; set; }
        [DataMember]
        public string pr_Item { get; set; }
    }
}
