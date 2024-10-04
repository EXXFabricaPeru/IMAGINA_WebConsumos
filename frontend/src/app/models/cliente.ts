
export interface Cliente {
    codigoCliente: string;
    nombre: string;
    ruc: string;
    direccion: string;
    codListaPrecio: string;
    codVendedor: string;
    codCondPago: string;
    lineaCredito: Number;
    saldoDisponible: Number;
    nombreComercial: string;
    tpoPersona: string;
    tpoDocumento: string;
    primerNombre: string;
    segundoNombre: string;
    apellidoPaterno: string;
    apellidoMaterno: string;    
    telefonoCliente: string;
    celularCliente: string;
    correoCliente: string;
    grupoCliente: string;
    agenteRetencion: boolean;
    buenContribuyente: boolean;
    flag: boolean;    
    currency: string;
    industryC: Number;
    codDireccion: string;
    zona: string;
    shipType: string;
}