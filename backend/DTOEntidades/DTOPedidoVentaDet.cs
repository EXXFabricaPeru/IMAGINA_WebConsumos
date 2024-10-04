using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOPedidoVentaDet
    {
        [DataMember]
        public string IdProducto { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public double Cantidad { get; set; }

        [DataMember]
        public double CantOpen { get; set; }
        
        [DataMember]
        public double CantAten { get; set; }
        
        [DataMember]
        public double Descuento { get; set; }

        [DataMember]
        public string Unidad { get; set; }

        [DataMember]
        public double  PrecioUnit { get; set; }
        
        [DataMember]
        public double  PrecioBruto { get; set; }

        [DataMember]
        public double  PrecioTotal { get; set; }

        [DataMember]
        public string TipoImpuesto { get; set; }

        [DataMember]
        public string CodAlmacen { get; set; }
        
        [DataMember]
        public string Dimension1 { get; set; }
        
        [DataMember]
        public string Dimension2 { get; set; }
        
        [DataMember]
        public string Dimension3 { get; set; }
        
        [DataMember ]
        public string Dimension4 { get; set; }
        
        [DataMember]
        public string Dimension5 { get; set; }
        
        [DataMember]
        public string Proyecto { get; set; }

        [DataMember]
        public int CodUndMed { get; set; }

        [DataMember]
        public int LineNum { get; set; }

        [DataMember]
        public string IdPartida { get; set; }
        [DataMember]
        public string Partida { get; set; }
        [DataMember]
        public string CuentaContable { get; set; }
        [DataMember]
        public string Estado { get; set; }
        
        [DataMember]
        public string NroEntrega { get; set; }

        [DataMember]
        public string DocEntryOc { get; set; }
        [DataMember]
        public string DocNumOc { get; set; }
        [DataMember]
        public string NumFacOC { get; set; }
        [DataMember]
        public string NumLine { get; set; }
    }
}
