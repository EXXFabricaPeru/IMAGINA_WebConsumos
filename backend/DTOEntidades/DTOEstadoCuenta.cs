using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOEstadoCuenta
    {
        [DataMember]
        public string CodCliente { get; set; }

        [DataMember]
        public string TipoDoc { get; set; }

        [DataMember]
        public string NomCliente { get; set; }

        [DataMember]
        public string NroDocumento { get; set; }

        [DataMember]
        public DateTime  FecEmision { get; set; }

        [DataMember]
        public DateTime  FecVencimiento { get; set; }

        [DataMember]
        public double  Original { get; set; }

        [DataMember]
        public double  Saldo { get; set; }

        [DataMember]
        public string Moneda { get; set; } 

        [DataMember]
        public double  OriginalUsd { get; set; }

        [DataMember]
        public double  SaldoUsd { get; set; }

        [DataMember]
        public string Vendedor { get; set; }

        [DataMember]
        public double  LineaCredito { get; set; }
        
        [DataMember]
        public double  TipoCambio { get; set; }

        [DataMember]
        public string  LineaDisponible { get; set; }
        
        [DataMember]
        public string  Direccion { get; set; }

        [DataMember]
        public int  DiasVencidos { get; set; }
        [DataMember]
        public string Compania { get; set; }
        [DataMember]
        public string CompaniaRuc { get; set; }
        [DataMember]
        public string CompaniaDir { get; set; }
        [DataMember]
        public string CompaniaTel { get; set; }
        [DataMember]
        public string CompaniaMail { get; set; }
    }
}
