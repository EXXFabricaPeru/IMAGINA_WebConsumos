using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTOEntidades
{
    [DataContract]
    public class DTODireccion
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Departamento { get; set; }
        [DataMember]
        public string Provincia { get; set; }
        [DataMember]
        public string Distrito { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Ubigeo { get; set; }
        [DataMember]
        public bool FlagEditar { get; set; }
        [DataMember]
        public string U_EXX_TPED_ZONA { get; set; }
        [DataMember]
        public bool FlagEliminar { get; set; }
    }
}
