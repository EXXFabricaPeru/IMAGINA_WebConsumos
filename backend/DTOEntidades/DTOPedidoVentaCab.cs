using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOPedidoVentaCab
    {
        [DataMember]
        public int IdPedido { get; set; }

        [DataMember]
        public string NroPedido { get; set; }

        [DataMember]
        public string CodCliente { get; set; }

        [DataMember]
        public string CondPago { get; set; }

        [DataMember]
        public string NroOC { get; set; }

        [DataMember]
        public double SubTotal { get; set; }

        [DataMember]
        public double Descuento { get; set; }

        [DataMember]
        public double  ImporteTotal { get; set; }

        [DataMember]
        public string Moneda { get; set; }

        [DataMember]
        public DateTime FecPedido { get; set; }

        [DataMember]
        public DateTime FecContabilizacion { get; set; }

        [DataMember]
        public DateTime FecSolicitado { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Comentario { get; set; }

        [DataMember]
        public string CodVendedor { get; set; }

        [DataMember]
        public string CodDireccion { get; set; }

        [DataMember]
        public string Direccion { get; set; }

        [DataMember]
        public string CodTransTramo1 { get; set; }

        [DataMember]
        public string RucTransTramo1 { get; set; }

        [DataMember]
        public string NomTransTramo1 { get; set; }

        [DataMember]
        public string DirTransTramo1 { get; set; }
                
        [DataMember]
        public string UserReg { get; set; } 
        
        [DataMember]
        public string JefeAlm { get; set; }

        [DataMember]
        public string Latitud { get; set; }

        [DataMember]
        public string Longitud { get; set; }

        [DataMember]
        public string NomCliente { get; set; }
        
        [DataMember]
        public string RucCliente { get; set; }
        
        [DataMember]
        public string TipoDoc { get; set; }
        
        [DataMember]
        public DateTime FecDespacho { get; set; }
        
        [DataMember]
        public DateTime FecFacturacion { get; set; }
        
        [DataMember]
        public string TipoOperacion { get; set; }
        
        [DataMember]
        public string Series { get; set; }
        
        [DataMember]
        public int Sucursal { get; set; }

        [DataMember]
        public string Archivo { get; set; }
        
        [DataMember]
        public string NomArchivo { get; set; }

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
        [DataMember]
        public string CodAgTra { get; set; }
        [DataMember]
        public string NomAgTra { get; set; }
        [DataMember]
        public string RucAgTra { get; set; }
        [DataMember]
        public string DirAgTra { get; set; }
        [DataMember]
        public string CdiAgTra { get; set; }
        [DataMember]
        public string ZonAgTra { get; set; }
        [DataMember]
        public string CodOperacion { get; set; }
        [DataMember]
        public int MedioEnvio { get; set; }
        
        [DataMember]
        public string EstadoPed { get; set; }
        
        [DataMember]
        public string NombreTrabajador { get; set; }
        [DataMember]
        public string MotivoAnulacion { get; set; }
        [DataMember]
        public List<DTOPedidoVentaDet> ListaDetalle { get; set; }
        [DataMember]
        public List<DTOPedidoVentaDet> ListaBonificacion { get; set; }

        //Campos nuevos solo para OC y Factura
        [DataMember]
        public int LineId { get; set; }        
        [DataMember]
        public string CodPartida { get; set; }
        [DataMember]
        public string Partida { get; set; }
        public double Saldo { get; set; }
    }
}
