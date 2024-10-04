using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOTomaInventarioCab
    {
        [DataMember(Order = 0)]
        public int IdToma { get; set; }

        [DataMember(Order = 1)]
        public string CodCliente { get; set; }

        [DataMember(Order = 2)]
        public string CodAlmacen { get; set; }

        [DataMember(Order = 3)]
        public string FechaToma { get; set; }

        [DataMember(Order = 4)]
        public int CodVendedor { get; set; }

        [DataMember(Order = 5)]
        public string UserReg { get; set; }

        [DataMember(Order = 6)]
        public string Latitud { get; set; }

        [DataMember(Order = 7)]
        public string Longitud { get; set; }

        [DataMember(Order = 8)]
        public List<DTOTomaInventarioDet> ListDetalle { get; set; }
    }
}
