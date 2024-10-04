import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  url: string = "./assets/img/GK.jpg";
  errorValidacion: Boolean = false;
  flagLoad: Boolean = false;
  flagExito: Boolean = false;
  confirmDialog: Boolean = false;
  msgError: string = "";
  userLost: string = "";
  user: string = "";
  pass: string = "";
  test: Date = new Date();

  constructor(private loginService: LoginService, private router: Router) {}

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  validar(){
    // console.log("---- Validar ----");
    // console.log("user", this.user);
    // console.log("pass", this.pass);
    this.flagExito = false;
    this.errorValidacion = false;

    if(this.user == "" || this.user == null){
      this.errorValidacion = true;
      this.msgError = "Tiene que escribir un usuario";
      return;
    }
    
    if(this.pass == "" || this.pass == null){
      this.errorValidacion = true;
      this.msgError = "Tiene que escribir una contraseña";
      return;
    }


    this.loginService.validarAcceso(this.user, this.pass).subscribe((data: any) => {
      
      if(data.estado == "True"){
        // console.log("Usuario", data.listaEmpleado[0]);        
        sessionStorage.setItem("dataUsuario", JSON.stringify(data.listaEmpleado[0]));
        // console.log("envio a la pantalla de inicio");
        this.router.navigateByUrl('/dashboard', { replaceUrl: true });
        // this.router.navigate(['inicio'])
      }else{
        this.errorValidacion = true;
        this.msgError = data.mensaje;
      }
    })
  }

  keyPress(eventVal: any){
    // console.log(eventVal)
    if (eventVal.charCode == 13){
      this.validar();
    }else{
      this.errorValidacion = false;
    }
  }

  async enviarOlvido(){
    this.flagExito = false;
    this.errorValidacion = false;

    const txtId = document.getElementById("txtId") as HTMLInputElement;
    if(txtId.value == ""){
      this.confirmDialog=false;
      this.errorValidacion = true;
      this.msgError = "Debe colocar su Usuario"
      return;
    }

    this.confirmDialog=false;
    this.flagLoad = true;
    const random = Math.random().toString(36).substring(2,12)
    const data: any = await this.loginService.sendEnvioCorreo(txtId.value, random.substring(0, 5).toUpperCase()).toPromise();

    this.msgError = data.mensaje;
    if(data.estado){
      this.flagExito = true;
      this.errorValidacion = false;
    }
    else{
      this.errorValidacion = true;
      this.flagExito = false;
    }
    this.flagLoad = false;
  }
}
