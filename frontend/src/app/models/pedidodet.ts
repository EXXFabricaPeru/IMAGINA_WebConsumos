export interface PedidoDet {
    idProducto: string;
    descripcion: string;
    cantidad: Number;
    cantOpen: Number;
    cantAten: Number;
    codUndMed: Number;
    unidad: string;
    precioUnit: Number;
    precioTotal: Number;
    tipoImpuesto: string;
    codAlmacen: string;
    stockDisponible: Number;
    dimension1: string;
    dimension2: string;
    dimension3: string;
    dimension4: string;
    proyecto: string;
    lineNum: Number;
    descuento: Number;
    precioBruto: number;
    listaUnd: any[];
    idPartida: string;
    partida: string;
    cuentaContable: string;
    nroEntrega: string;
    listaPartida: any[];
    docEntryOc: string;
    docNumOc: string;
    numFacOC: string;
    numLine: string;
}