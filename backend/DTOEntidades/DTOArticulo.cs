using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOArticulo
    {
        [DataMember]
        public string CodArticulo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string CodAlmacen { get; set; }

        [DataMember]
        public double CantActual { get; set; }

        [DataMember]
        public double PrecioUnit { get; set; }

        [DataMember]
        public string UnidadMedida { get; set; }
        
        [DataMember]
        public int CodUndMed { get; set; }

        [DataMember]
        public string CentroCosto { get; set; }

        [DataMember]
        public double StockDisponible { get; set; }
        
        [DataMember]
        public double StockMin { get; set; }

        [DataMember]
        public double CantComprom { get; set; }
        
        [DataMember]
        public double CantSolicitada { get; set; }
    }
}
