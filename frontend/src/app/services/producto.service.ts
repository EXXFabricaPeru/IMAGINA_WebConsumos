import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/enviroment'

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  url: string= environment.url;
  constructor(private http: HttpClient) { }

  obtenerProducto(valor: string, almacen: string, listprecio: string, moneda: string, flagStock: string){
    return this.http.get(`${ this.url }Articulo?valor=${ valor }&almacen=${ almacen }&listprice=${ listprecio }&moneda=${ moneda }&flagStock=${ flagStock }`);
  }

  obtenerProductoStock(codArticulo: string, sucursal: string){
    return this.http.get(`${ this.url }Articulo/stock?codigoArt=${ codArticulo }&sucursal=${sucursal}`);
  }

  obtenerProductPrecio(listPrice: string, moneda: string, undMed: string, codArti: string){
    return this.http.get(`${ this.url }Articulo/precio?listPrice=${ listPrice }&moneda=${ moneda }&undMed=${ undMed }&codigoArt=${ codArti }`);
  }

  obtenerStockUnd(almacen: string, undMed: string, codArti: string){
    return this.http.get(`${ this.url }Articulo/stockund?almacen=${ almacen }&undMed=${ undMed }&codigoArt=${ codArti }`);
  }
}
