using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOCliente
    {
        [DataMember]
        public string CodigoCliente { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Ruc { get; set; }

        [DataMember]
        public string Direccion { get; set; }

        [DataMember]
        public string GrupoCliente { get; set; }

        [DataMember]
        public string CodListaPrecio { get; set; }

        [DataMember]
        public string CodVendedor { get; set; }

        [DataMember]
        public string CodCondPago { get; set; }

        [DataMember]
        public double LineaCredito { get; set; }

        [DataMember]
        public double SaldoDisponible { get; set; }
        
        [DataMember]
        public string NombreComercial { get; set; }
        
        [DataMember]
        public string TpoPersona { get; set; }
        
        [DataMember]
        public string TpoDocumento { get; set; }
        
        [DataMember]
        public string PrimerNombre { get; set; }
        
        [DataMember]
        public string SegundoNombre { get; set; }
        
        [DataMember]
        public string ApellidoPaterno { get; set; }
        
        [DataMember]
        public string ApellidoMaterno { get; set; }
        
        [DataMember]
        public string TelefonoCliente{ get; set; }
        
        [DataMember]
        public string CelularCliente { get; set; }
        
        [DataMember]
        public string CorreoCliente { get; set; }

        [DataMember]
        public bool AgenteRetencion { get; set; }

        [DataMember]
        public bool BuenContribuyente { get; set; }

        [DataMember]
        public string U_EK_NMCONDPG { get; set; }
        [DataMember]
        public string U_EXK_SOLCRED { get; set; }
        [DataMember]
        public string U_EXK_IMPCRED { get; set; }
        [DataMember]
        public string U_EXK_MNDCRED { get; set; }
        [DataMember]
        public string U_EXK_NOTCRED { get; set; }
        [DataMember]
        public string U_EXK_AUTCRED { get; set; }
        [DataMember]
        public string U_EXK_SOLSOB { get; set; }
        [DataMember]
        public string U_EXK_PORSOB { get; set; }
        [DataMember]
        public string U_EXK_NCRESOB { get; set; }
        [DataMember]
        public string U_EXK_AUTSOB { get; set; }
        [DataMember]
        public string U_EXK_SCCONPAG { get; set; }
        [DataMember]
        public string U_EXK_CONDPAGO { get; set; }
        [DataMember]
        public string U_EXK_NCCONDPAG { get; set; }
        [DataMember]
        public string U_EXK_CDPAGAUT { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string CodDireccion { get; set; }
        [DataMember]
        public string Zona { get; set; }
        [DataMember]
        public string ShipType { get; set; }
        [DataMember]
        public int IndustryC { get; set; }
        [DataMember]
        public bool Flag { get; set; }
        [DataMember]
        public List<DTODireccion> ListaDireccion { get; set; }

        [DataMember]
        public List<DTOContacto> ListaContacto { get; set; }

    }
}
