import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/enviroment'
import { Cliente } from '../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  url: string= environment.url;
  constructor(private http: HttpClient) { }

  obtenerCliente(idSucursal: string){
    return this.http.get(`${ this.url }Cliente?idSucursal=${ idSucursal }`);
  }


  //------------------->API CONSULTA RUC<-------------------//
  consultaRUC(ruc: string, accessToken: string){
    // const _headers = new Headers({
    //   'Content-Type': 'application/json',
    //   'Authorization': `Bearer ${accessToken}`
    // })
    return this.http.get(`http://192.168.1.235:8081/api/ConsultaGeneral/${ ruc }`, { headers: { 'Authorization': `Bearer ${accessToken}` } })
    // return this.http.get(`https://localhost:44389/api/Contribuyente/${ ruc }`, { headers: { 'Authorization': `Bearer ${accessToken}` } })
  }
  
  loginConsultaRUC(data: any){
    // return this.http.post (`https://localhost:44389/api/Autenticacion`, data);    
    return this.http.post (`http://192.168.1.235:8081/api/Autenticacion`, data);    
  }
}
