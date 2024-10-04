using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOPromocion
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public List<DTOPromocionDet> ListaPromocion {  get; set; } 
    }
}
