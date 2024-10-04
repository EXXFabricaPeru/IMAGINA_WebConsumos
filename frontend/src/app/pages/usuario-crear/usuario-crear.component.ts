import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TablaGeneral } from 'src/app/models/tablageneral';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { MaestroService } from 'src/app/services/maestro.service';

declare var $: any;

@Component({
  selector: 'app-usuario-crear',
  templateUrl: './usuario-crear.component.html',
  styleUrls: ['./usuario-crear.component.css']
})
export class UsuarioCrearComponent implements OnInit {
  _titulo: string = ""; 
  listaPriceList: TablaGeneral[] = [];
  listaMoneda: TablaGeneral[] = [];
  listaVendedores: TablaGeneral[] = [];
  listaSucursales: TablaGeneral[] = [];
  dataUsuario: any;
  codSucursal: string = "";

  idEmpleado: string = "";
  perfil: string = "";

  usuario: string = "";
  password: string = "";
  codMoneda: string = "";
  codVendedorAsig: string = "";
  listaPrecio: string = "";
  codUsuario: string = "";
  oUsuario: Usuario = {
    codVendedor: 0,
    usuario: '',
    nombreEmpleado: '',
    idEmpleado: '',
    contrasenia: '',
    listaPrecio: '',
    moneda: '',
    activo: '',
    sucursal: 0,
    perfil: '',
    sucursales: ''
  };

  confirmDialog: boolean = false;
  exito: boolean = false;
  flagEnable: boolean = true;
  msgError: string = "";
  sucursales: any;

  form: any;
  selectCategoria: any[];
  @Output() formEmit = new EventEmitter<any>;

  constructor(private router: Router, private _route: ActivatedRoute, private maestroService: MaestroService, private usuarioService: LoginService){
    let dataTemp: any = sessionStorage.getItem("dataUsuario");
    this.dataUsuario = JSON.parse(dataTemp);
    // console.log(this.dataUsuario);
    if(this.dataUsuario == null || this.dataUsuario == undefined){
      this.router.navigateByUrl('login', { replaceUrl: true })
    }else{
      this.perfil = this.dataUsuario.perfil;
    }
    
  }

  async ngOnInit(): Promise<void> {  
    sessionStorage.removeItem("sucursales");

    const dataMoneda: any = await this.maestroService.obtenerMoneda().toPromise();
    if(dataMoneda.estado == "True"){
      this.listaMoneda = dataMoneda.listaTablaGeneral;
    }

    //codigo
    const xCodUsuarioaux: any = this._route.snapshot.paramMap.get("codigo");
    // console.log("---->", xCodUsuarioaux)
    if(xCodUsuarioaux != "0"){
      const xCodUsuario: any = this._route.snapshot.paramMap.get("codigo");
      this.codUsuario = xCodUsuario;
      // console.log("Codigo:", this.codUsuario);
      await this.buscarUsuario(this.codUsuario);
    }else{
      const dataVendedor: any = await this.maestroService.obtenerListaVendedor("0").toPromise();
      if(dataVendedor.estado == "True"){
        this.selectCategoria = [0];
        this.listaVendedores = dataVendedor.listaTablaGeneral;
      }
    }

    const dataSucursal: any = await this.maestroService.obtenerSucursales().toPromise();
    if(dataSucursal.estado == "True"){
      this.sucursales = dataSucursal.listaTablaGeneral;
      this.listaSucursales = dataSucursal.listaTablaGeneral;
    }


    this.initSelect();
  }

  async guardarCliente(){
    const _vendedor = document.getElementById("input-vendedor") as HTMLSelectElement
    const _moneda = document.getElementById("input-moneda") as HTMLSelectElement
    const _sucursal: string = sessionStorage.getItem("sucursales")
    // console.log("sucursales",_sucursal);
    
    const _usuario = document.getElementById("input-user") as HTMLInputElement
    const _password = document.getElementById("input-password") as HTMLInputElement
    const _perfil = document.getElementById("input-perfil") as HTMLSelectElement

    this.codSucursal = _sucursal;
    this.codVendedorAsig = _vendedor.value;
    // this.listaPrecio = _listprice.value;
    this.codMoneda = _moneda.value;
    this.usuario = _usuario.value;
    this.password = _password.value;

    if(this.codVendedorAsig == ""){
      this.msgError = "Debe seleccionar un empleado";
      this.confirmDialog = true;
      return;
    }

    if(this.codMoneda == ""){
      this.msgError = "Debe seleccionar una moneda";
      this.confirmDialog = true;
      return;
    }

    if(this.password == ""){
      this.msgError = "Debe llenar el campo password";
      this.confirmDialog = true;
      return;
    }

    if(this.usuario == ""){
      this.msgError = "Debe llenar el campo usuario";
      this.confirmDialog = true;
      return;
    }

    if(_perfil.value == ""){
      this.msgError = "Debe seleccionar un perfil";
      this.confirmDialog = true;
      return;
    }

    console.log("Sucursal", this.codSucursal)
    if((this.codSucursal == null || this.codSucursal == "" || this.codSucursal == ",") && this.listaSucursales.length > 0){
      this.msgError = "Debe seleccionar al menos una sucursal";
      this.confirmDialog = true;
      return;
    }

    this.oUsuario = {
      codVendedor: Number(this.codVendedorAsig),
      usuario: this.usuario,
      nombreEmpleado: '',
      idEmpleado: this.codUsuario,
      contrasenia: this.password,
      listaPrecio: this.listaPrecio,
      moneda: this.codMoneda,
      activo: 'Y',
      sucursal: 0,
      sucursales: this.codSucursal,
      perfil: _perfil.value
    }
    console.log(this.oUsuario);    

    if(this.codUsuario == "" || this.codUsuario == "0"){
      const data: any = await this.usuarioService.usurioRegistrar(this.oUsuario).toPromise();
      console.log("Respuesta registro", data);
      if(data.estado == "True"){
        this.exito = true;
        sessionStorage.removeItem("sucursales");
        
      }
      this.msgError = data.mensaje;
      this.confirmDialog = true;
      console.log(this.confirmDialog, this.msgError);      
    }else{
      const data: any = await this.usuarioService.usurioActualizar(this.oUsuario).toPromise();
      console.log("Respuesta actualizar", data);        
      if(data.estado == "True"){
        this.exito = true;
        sessionStorage.removeItem("sucursales");
        console.log(this.dataUsuario.usuario, this.usuario);
        
        if(this.dataUsuario.usuario == this.usuario){
          this.usuarioService.validarAcceso(this.usuario, this.password).subscribe((data: any) => {
      
            if(data.estado == "True"){
              // console.log("Usuario", data.listaEmpleado[0]);        
              sessionStorage.setItem("dataUsuario", JSON.stringify(data.listaEmpleado[0]));
            }
          });
        }
      }
      this.msgError = data.mensaje;
      this.confirmDialog = true;
      console.log(this.confirmDialog, this.msgError);
    }

  }

  closeDialogConfirm(){
    this.confirmDialog = false;
    if(this.exito){
      this.router.navigateByUrl(`/usuario-listar`, { replaceUrl: true });
    }
  }

  async buscarUsuario(id: string){
    const _vendedor = document.getElementById("input-vendedor") as HTMLSelectElement;
    // const _listprice = document.getElementById("input-lista-precio") as HTMLSelectElement;
    const _moneda = document.getElementById("input-moneda") as HTMLSelectElement;
    // const _sucursal = document.getElementById("input-compania") as HTMLSelectElement;
    const _perfil = document.getElementById("input-perfil") as HTMLSelectElement;
    const _usuario = document.getElementById("input-user") as HTMLInputElement;
    const _password = document.getElementById("input-password") as HTMLInputElement;

    const data: any = await this.usuarioService.obtenerUsuario(id).toPromise();
    // console.log("Usuario--->", data)
    this.oUsuario = data.listaEmpleado[0];
    this.codVendedorAsig = this.oUsuario.codVendedor.toString();
    // console.log("Vendedor--->", this.codVendedorAsig)
    const dataVendedor: any = await this.maestroService.obtenerListaVendedor(this.codVendedorAsig).toPromise();
    if(dataVendedor.estado == "True"){
      this.listaVendedores = dataVendedor.listaTablaGeneral;
    }
    // console.log(this.codVendedorAsig, _vendedor.options);
    _vendedor.value = this.codVendedorAsig;
    this.usuario = this.oUsuario.usuario;
    _usuario.value = this.usuario;
    this.listaPrecio = this.oUsuario.listaPrecio;
    // _listprice.value = this.listaPrecio;
    this.codMoneda = this.oUsuario.moneda;
    _moneda.value = this.codMoneda;
    this.codSucursal = this.oUsuario.sucursales;
    sessionStorage.setItem("sucursales", this.codSucursal)
    console.log("sucursal", this.codSucursal);
    this.codUsuario = this.oUsuario.idEmpleado;      
    // _sucursal.value = this.codSucursal;
    this.selectCategoria = this.codSucursal.split(",");
    // console.log("Categoria Usuario",this.codSucursal, this.selectCategoria);
    
    this.password = this.oUsuario.contrasenia;
    _password.value = this.password;

    _perfil.value = this.oUsuario.perfil;

    if(this.perfil == "1"){
      this.flagEnable = true;
      _vendedor.disabled = true;
      _moneda.disabled = true;
      // _sucursal.disabled = true;
      _perfil.disabled = true;
      _usuario.disabled = true;
      // _password.disabled = true;
    }else{
      this.flagEnable = false;
    }
  }

  initSelect(){
    const self = this;
    $(document).ready(function() {
      $('.select-sucursal').select2();
      $('.select-sucursal').on('select2:select', function(event: any) {
        const values = $('.select-sucursal').select2('val');
        // console.log(values);
        this.selectCategoria = values;
        this.codSucursal=",";
        for(let i=0; i<this.selectCategoria.length; i++){
          this.codSucursal += this.selectCategoria[i] + ",";
        }
        this.codSucursal = this.codSucursal.toString().substring(1, this.codSucursal.toString().length-1);
        console.log("string", this.codSucursal);
        // console.log("array", this.selectCategoria);
        sessionStorage.setItem("sucursales", this.codSucursal)
      });
      $('.select-sucursal').on('select2:unselect', function(event: any) {
        const values = $('.select-sucursal').select2('val');
        // console.log(values);
        this.selectCategoria = values;
        this.codSucursal=",";
        for(let i=0; i<this.selectCategoria.length; i++){
          this.codSucursal += this.selectCategoria[i] + ",";
        }
        this.codSucursal = this.codSucursal.toString().substring(1, this.codSucursal.toString().length-1);
        console.log("string", this.codSucursal);
        // console.log("array", this.selectCategoria);
        sessionStorage.setItem("sucursales", this.codSucursal)
      });
    });
  }

  

  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
