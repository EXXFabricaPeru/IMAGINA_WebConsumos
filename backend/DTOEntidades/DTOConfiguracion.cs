using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public  class DTOConfiguracion
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Campo { get; set; }
        [DataMember]
        public string Formulario { get; set; }
        [DataMember]
        public bool Visible { get; set;}
        [DataMember]
        public bool Editable { get; set;}
        [DataMember]
        public string Valor { get; set; }
    }
}
