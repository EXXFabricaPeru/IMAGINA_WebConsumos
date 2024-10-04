using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOTransferenciaCab
    {
        [DataMember]
        public int DocEntry { get; set; }

        [DataMember]
        public int DocNum { get; set; }

        [DataMember]
        public string CodCliente { get; set; }

        [DataMember]
        public DateTime DocDate { get; set; }

        [DataMember]
        public DateTime TaxDate { get; set; }

        [DataMember]
        public DateTime DocDueDate { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Comentario { get; set; }

        [DataMember]
        public string CodEmpleado { get; set; }
       
        [DataMember]
        public string UserReg { get; set; }

        public string NomCliente { get; set; }
        
        [DataMember]
        public string TipoOperacion { get; set; }
        
        [DataMember]
        public string Series { get; set; }
        
        [DataMember]
        public int IdSucursal { get; set; }

        [DataMember]
        public string Sucursal { get; set; }

        [DataMember]
        public string Compania { get; set; }

        [DataMember]
        public string CodAlmacenOri { get; set; }

        [DataMember]
        public string CodAlmacenDest { get; set; }
        
        [DataMember]
        public string NombreTrabajador { get; set; }
        [DataMember]
        public string MotivoAnulacion { get; set; }
        [DataMember]
        public string JefeAlm { get; set; }

        [DataMember]
        public List<DTOTransferenciaDet> ListaDetalle { get; set; }
    }
}
