import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/enviroment'
import { Usuario } from '../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url: string= environment.url;
  constructor(private http: HttpClient) { }

  validarAcceso(user: string, pass: string){
    const data: any = {
      "username": user,
      "password": pass
    }
    return this.http.post(`${ this.url }Usuario/login`, data);
  }

  obtenerUsuario(valor: string){
    return this.http.get(`${ this.url }Usuario/obtener?codigo=${ valor }`);
  }

  obtenerUsuarios(valor: string){
    return this.http.get(`${ this.url }Usuario/listar?valor=${ valor }`);
  }

  usurioRegistrar(user: Usuario){
    return this.http.post(`${ this.url }Usuario`, user);
  }

  usurioActualizar(user: Usuario){
    return this.http.put(`${ this.url }Usuario`, user);
  }

  sendEnvioCorreo(ruc: string, code: string){
    const data: any = {
      "password": code,
      "username": ruc
    }
    return this.http.post(`${ this.url }Usuario/PassLost`, data);
  }

  changePass(user: string, pass: string, code: string){
    const data: any = {
      "password": pass,
      "username": user,
      "token": code
    }
    return this.http.post(`${ this.url }Usuario/PassChange`, data);
  }
}
