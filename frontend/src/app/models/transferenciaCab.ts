import { TransferenciaDet } from "./transferenciaDet";

export interface TransferenciaCab {
    docEntry: number,
    docNum: number,
    codCliente: string,
    docDate: Date,
    taxDate: Date,
    docDueDate: Date,
    estado: string,
    comentario: string,
    codEmpleado: string,
    userReg: string,
    nomCliente: string,
    tipoOperacion: string,
    series: string,
    idSucursal: number,
    sucursal: string,
    compania: string,
    codAlmacenOri: string,
    codAlmacenDest: string,
    nombreTrabajador: string,
    jefeAlm: string,
    listaDetalle: TransferenciaDet[]
}