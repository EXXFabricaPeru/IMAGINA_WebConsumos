import { PedidoDet } from "./pedidodet";

export interface PedidoCab {
    idPedido: Number;
    nroPedido: string;
    codCliente: string;
    condPago: string;
    nroOC: string;
    subTotal: Number;
    descuento: Number;
    importeTotal: Number;
    moneda: string;
    fecPedido?: Date;
    fecSolicitado?: Date;
    fecContabilizacion?: Date;
    estado: string;
    comentario: string;
    codVendedor: string;
    codDireccion: string;
    direccion: string;
    userReg: string;
    latitud: string;
    longitud: string;
    nomCliente: string;
    rucCliente: string;
    fecDespacho?: Date;
    fecFacturacion?: Date;
    tipoOperacion: string;
    series: string;
    sucursal: number;
    medioEnvio: number;
    estadoPed: string;
    nombreTrabajador: string;
    motivoAnulacion: string;
    jefeAlm: string;
    listaDetalle: PedidoDet[];
}