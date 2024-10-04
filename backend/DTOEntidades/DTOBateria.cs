using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Etna.DTOEntidades
{
    [DataContract]
    public class DTOBateria
    {
        [DataMember(Order = 0)]
        public int IdProducto { get; set; }
        [DataMember(Order = 1)]
        public string CodAnt { get; set; }
        [DataMember(Order = 2)]
        public string Descripcion { get; set; }
        [DataMember(Order = 3)]
        public string Almacen { get; set; }
        [DataMember(Order = 4)]
        public int Stock { get; set; }
        [DataMember(Order = 5)]
        public string UnidadMed { get; set; }
        [DataMember(Order = 6)]
        public string PrecUnit { get; set; }
        [DataMember(Order = 7)]
        public string PrecioPromo1 { get; set; }
        [DataMember(Order = 8)]
        public string FecIniPromo1 { get; set; }
        [DataMember(Order = 9)]
        public string FecFinPromo1 { get; set; }
        [DataMember(Order = 10)]
        public string PrecioPromo2 { get; set; }
        [DataMember(Order = 11)]
        public string FecIniPromo2 { get; set; }
        [DataMember(Order = 12)]
        public string FecFinPromo2 { get; set; }
        [DataMember(Order = 13)]
        public string PrecioPromo3 { get; set; }
        [DataMember(Order = 14)]
        public string FecIniPromo3 { get; set; }
        [DataMember(Order = 15)]
        public string FecFinPromo3 { get; set; }
        [DataMember(Order = 16)]
        public string PrecioPromo4 { get; set; }
        [DataMember(Order = 17)]
        public string FecIniPromo4 { get; set; }
        [DataMember(Order = 18)]
        public string FecFinPromo4 { get; set; }
        [DataMember(Order = 19)]
        public string Imagen { get; set; }
    }
}
