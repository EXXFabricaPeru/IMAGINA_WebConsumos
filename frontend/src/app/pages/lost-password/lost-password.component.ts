import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-lost-password',
  templateUrl: './lost-password.component.html',
  styleUrls: ['./lost-password.component.css']
})
export class LostPasswordComponent implements OnInit {
  flagExito: Boolean = false;
  flagError: Boolean = false;
  flagLoad: Boolean = false;

  msg: string = "";

  constructor(private router: Router, private _route: ActivatedRoute, private loginService: LoginService) { }

  ngOnInit(): void {
  }

  async grabar(){
    this.flagLoad = true;
    this.msg = "";
    try {
      this.flagError = false;
      this.flagExito = false;

      const user = this._route.snapshot.paramMap.get("user") || "";
      const pass: string = (document.getElementById("passNew") as HTMLInputElement).value
      const passAux: string = (document.getElementById("passCon") as HTMLInputElement).value
      const token: string = (document.getElementById("token") as HTMLInputElement).value

      if(pass == "")
        this.msg = "Debe colocar una contraseña"

      if(pass != passAux)
        this.msg = "La confirmación de contraseña debe ser igual a la nueva contraseña";

      if(token == "")
        this.msg = "Debe colocar un código de validación";

      if(this.msg == ""){
        console.log("enviando info.....");
         const data: any = await this.loginService.changePass(user,pass,token).toPromise();
        console.log(data);
        
        if(data.estado){
          this.flagExito = true;
        }else{
          this.flagError = true;
        }

        this.msg = data.mensaje;

      }else{
        this.flagError = true;
      }
    } catch (error) {
      this.flagError = true;
      this.msg = error.toString();
    }

    this.flagLoad = false;
  }

  salir(){
    this.router.navigateByUrl('login', { replaceUrl: true })
  }
}
