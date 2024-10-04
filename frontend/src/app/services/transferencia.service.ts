import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/enviroment'
import { TransferenciaCab } from '../models/transferenciaCab';

@Injectable({
  providedIn: 'root'
})
export class TransferenciaService {
  url: string= environment.url;
  constructor(private http: HttpClient) { }

  obtenerTransferencia(nro: string){
    return this.http.get(`${this.url}Transferencia?docEntry=${nro}`);
  }

  obtenerListaTransferencia(fecDesde: string, fecHasta: string, estado: string, user: string, compania: string, product: string){
    return this.http.get(`${this.url}Transferencia/Listar?fecDesde=${fecDesde}&fecHasta=${fecHasta}&estado=${estado}&user=${user}&compania=${compania}&product=${ product }`);
  }

  registrarDocumento(documento: TransferenciaCab){
    return this.http.post(`${ this.url }Transferencia`, documento);
  }

  anularDocumento(documento: TransferenciaCab){
    return this.http.put(`${ this.url }Transferencia`, documento);
  }
}
