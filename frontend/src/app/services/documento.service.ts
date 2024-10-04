import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/enviroment'
import { PedidoCab } from '../models/pedidocab';

@Injectable({
  providedIn: 'root'
})
export class DocumentoService {
  url: string= environment.url;
  constructor(private http: HttpClient) { }

  //Lista las OC
  obtenerOC(proyecto: string, etapa: string, sucursal: string, articulo: string){
    return this.http.get(`${ this.url }Pedido/ListaOC?empresa=${ sucursal }&proyecto=${ proyecto }&etapa=${ etapa }&articulo=${ articulo }`);
  }
  //Lista las facturas
  obtenerFT(proyecto: string, etapa: string, sucursal: string, articulo: string){
    return this.http.get(`${ this.url }Pedido/ListaFT?empresa=${ sucursal }&proyecto=${ proyecto }&etapa=${ etapa }&articulo=${ articulo }`);
  }
  //Lista los pedidos
  obtenerDocumentos(fecIni: string, fecFin: string, codVendedor: string, codCliente: string, estado: string, product: string){
    return this.http.get(`${ this.url }Pedido/Lista?CodVendedor=${ codVendedor }&FecIni=${ fecIni }&FecFin=${ fecFin }&cliente=${ codCliente }&Estado=${ estado }&product=${ product }`);
  }

  //Registra pedido
  registrarDocumento(documento: PedidoCab){
    return this.http.post(`${ this.url }Pedido`, documento);
  }

  //Registra pedido
  cancelarDocumento(documento: PedidoCab){
    return this.http.put(`${ this.url }Pedido`, documento);
  }

  //Busca el pedido
  obtenerDocumento(nro: string, tipo: string){
    return this.http.get(`${ this.url }Pedido?id=${ nro }&tipo=${ tipo }`);
  }

  obtenerReporte(nro: string, tipo: string){
    return this.http.get(`${ this.url }Pedido/Reporte?id=${ nro }&tipo=${ tipo }`);
  }

}
