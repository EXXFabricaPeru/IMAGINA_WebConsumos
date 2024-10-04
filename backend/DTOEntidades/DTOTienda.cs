using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOTienda
    {
        [DataMember(Order = 0)]
        public string Tienda { get; set; }

        [DataMember(Order = 1)]
        public string Direccion { get; set; }

        [DataMember(Order = 2)]
        public string CodLider { get; set; }

        [DataMember(Order = 3)]
        public string NomLider { get; set; }

        [DataMember(Order = 4)]
        public string CodSupervisor { get; set; }

        [DataMember(Order = 5)]
        public string NomSupervisor { get; set; }

        [DataMember(Order = 6)]
        public string Categoria { get; set; }

        [DataMember(Order = 7)]
        public string SkuCodificado { get; set; }

        [DataMember(Order = 8)]
        public string FrenteRack_Pro { get; set; }

        [DataMember(Order = 9)]
        public string FrenteRack_AD { get; set; }

        [DataMember(Order = 10)]
        public string FrenteRack_PL { get; set; }

        [DataMember(Order = 11)]
        public string FrenteRack_SS { get; set; }

        [DataMember(Order = 12)]
        public string FrenteRack_CP { get; set; }

        [DataMember(Order = 13)]
        public string FrenteRackHori { get; set; }

        [DataMember(Order = 14)]
        public string NroLinea { get; set; }

        [DataMember(Order = 15)]
        public string Latitud { get; set; }

        [DataMember(Order = 16)]
        public string Longitud { get; set; }
    }
}
