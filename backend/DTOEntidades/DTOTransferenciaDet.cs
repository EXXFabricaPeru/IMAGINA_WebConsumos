using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOTransferenciaDet
    {
        [DataMember]
        public string IdProducto { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public double Cantidad { get; set; }
        
        [DataMember]
        public double CantidadOpen { get; set; }

        [DataMember]
        public double CantidadAten { get; set; }
        
        [DataMember]
        public string Unidad { get; set; }

        [DataMember]
        public string CodAlmacenOri { get; set; }

        [DataMember]
        public string CodAlmacenDest { get; set; }
        
        [DataMember]
        public string Dimension1 { get; set; }
               
        [DataMember]
        public string Dimension2 { get; set; }
               
        [DataMember]
        public string Dimension3 { get; set; }
               
        [DataMember]
        public string Dimension4 { get; set; }
                
        [DataMember]
        public string Proyecto { get; set; }

        [DataMember]
        public int CodUndMed { get; set; }

        [DataMember]
        public int LineNum { get; set; }

        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string IdPartida { get; set; }
        [DataMember]
        public string Partida { get; set; }
        [DataMember]
        public string NroTransferencia { get; set; }

    }
}
