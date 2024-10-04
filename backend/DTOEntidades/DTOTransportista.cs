using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOTransportista
    {
        [DataMember(Order = 0, Name = "ructransportista")]
        public string pr_Ruc { get; set; }
        [DataMember(Order = 1, Name = "nomtransportista")]
        public string pr_RazonSocial { get; set; }
        [DataMember(Order = 2, Name = "dirtransportista")]
        public string pr_Direccion { get; set; }
        [DataMember(Order = 3, Name = "codtransportista")]
        public string pr_CodTransportista{ get; set; }
    }
}
