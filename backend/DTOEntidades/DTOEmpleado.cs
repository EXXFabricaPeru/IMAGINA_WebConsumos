using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOEmpleado
    {
        [DataMember]
        public string IdEmpleado { get; set; }
        
        [DataMember]
        public int CodVendedor { get; set; }

        [DataMember]
        public string Perfil { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string NombreEmpleado { get; set; }

        [DataMember]
        public string Contrasenia { get; set; }

        [DataMember]
        public string ListaPrecio { get; set; }

        [DataMember]
        public string Moneda { get; set; }
        
        [DataMember]
        public int Sucursal { get; set; }

        [DataMember]
        public string Sucursales { get; set; }

        [DataMember]
        public string Activo { get; set; }
        
        [DataMember]
        public bool ListaPrecioBruto { get; set; }
        
        [DataMember]
        public string DiasVencim { get; set; }
        
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<DTOConfiguracion> Configuracion { get; set; }
    }
}
