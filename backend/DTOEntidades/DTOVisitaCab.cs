using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOVisitaCab
    {
        [DataMember]
        public int IdVisita { get; set; }

        [DataMember]
        public string CodCliente { get; set; }

        [DataMember]
        public int CodVendedor { get; set; }

        [DataMember]
        public string Comentario { get; set; }

        [DataMember]
        public string FecVisita { get; set; }

        [DataMember]
        public string CodAlmacen { get; set; }

        [DataMember]
        public string UserReg { get; set; }

        [DataMember]
        public string Latitud { get; set; }

        [DataMember]
        public string Longitud { get; set; }

        [DataMember]
        public List<DTOVisitaDet> ListDetalle { get; set; }
    }
}
