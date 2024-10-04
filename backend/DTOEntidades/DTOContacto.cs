using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOContacto
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string Apellido{ get; set; }
        [DataMember]
        public string Celular{ get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Cargo { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool FlagEditar { get; set; }
        [DataMember]
        public bool FlagEliminar { get; set; }
    }
}
