using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOPromocionDet
    {
        [DataMember]
        public string CodProducto { get; set; }
        [DataMember]
        public string NomProduto { get; set; }
        [DataMember]
        public double Cantidad { get; set;}
        [DataMember]
        public string TipoDescuento { get; set;}
        [DataMember]
        public double Valor { get; set;}
        [DataMember]
        public int Order { get; set; }
    }
}
