using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTORespuesta
    {
        [DataMember(Order = 0, Name = "estado")]
        public bool Estado { get; set; }

        [DataMember(Order = 1, Name = "mensaje")]
        public string Mensaje { get; set; }

        [DataMember(Order = 2, Name = "key")]
        public string key { get; set; }

        [DataMember(Order = 3, Name = "rpt")]
        public byte[] rpt { get; set; }
    }
}
