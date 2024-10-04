using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTODepartamento
    {
        [DataMember(Order = 0)]
        public string pr_CodDepartamento { get; set; }

        [DataMember(Order = 1)]
        public string pr_Departamento { get; set; }

        [DataMember(Order = 2)]
        public List<DTOProvincia> ListaProvincia { get; set; }
    }
}
