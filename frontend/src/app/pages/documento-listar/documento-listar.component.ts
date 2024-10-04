import { AfterViewInit, Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import ColumnHeader from 'src/app/models/columnHeader';
import { PedidoCab } from 'src/app/models/pedidocab';
import { ClienteService } from 'src/app/services/cliente.service';
import { DocumentoService } from 'src/app/services/documento.service';
import { MaestroService } from 'src/app/services/maestro.service';

@Component({
  selector: 'app-documento-listar',
  templateUrl: './documento-listar.component.html',
  styleUrls: ['./documento-listar.component.scss']
})
export class DocumentoListarComponent implements OnInit, AfterViewInit {
  public listaHeader: ColumnHeader[] = [];
  public fecDesde: string = new Date().toISOString().slice(0, 10);
  public fecHasta: string = new Date().toISOString().slice(0, 10);
  public codVendedor: number = 0;
  public user: string = "";
  public tipoDoc: any = 0;
  listaDocumento: PedidoCab[] = [];
  valCliente:string = "";
  idSucursal:string = "";
  dataUsuario: any;
  listaPaginas: number[] = [];
  listGrupoPag: number[] = [];
  pagina: number = 0;
  totalPag: number = 0;
  mensajeVacio: string = "";
  listaSucursalesAux: any[] = [];
  listaSucursales: any[] = [];

  constructor(private documentoService: DocumentoService, private clienteService: ClienteService, private _route: ActivatedRoute, private router: Router, private maestroService: MaestroService) { 
    // console.log("listar documentos");
    
    let dataTemp: any = sessionStorage.getItem("dataUsuario");
    this.dataUsuario = JSON.parse(dataTemp);
    if(this.dataUsuario == null || this.dataUsuario == undefined){
      this.router.navigateByUrl('login', { replaceUrl: true });
    }else{
      this.codVendedor = this.dataUsuario.codVendedor;
      this.user = this.dataUsuario.usuario;
      this.idSucursal = this.dataUsuario.sucursales;

    }
  }

  async ngOnInit(): Promise<void> {
    // console.log("---listar documentos----");
    this.tipoDoc = this._route.snapshot.paramMap.get("tipo");

    this.listaHeader = [
      {
        label: "Sucursal",
        key: "compania",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "NroPed",
        key: "nroPedido",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Fecha",
        key: "fecPedido",
        subKey: "",
        customClass: "fecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Moneda",
        key: "moneda",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Importe",
        key: "importeTotal",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Usuario Autorizador",
        key: "userReg",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Nombre Trabajador",
        key: "nombreTrabajador",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Estado",
        key: "estado",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "",
        key: "nroPedido",
        subKey: "",
        customClass: "btnEdit",
        type: "buttonSearch",
        value: "",
        visible: true
      },
    ];
    
    // console.log("fecha hoy  ", this.fecDesde);

    const _txtDesde = document.getElementById("input-fecdesde") as HTMLInputElement;
    const _txtHasta = document.getElementById("input-fechasta") as HTMLInputElement;
    _txtDesde.value = this.fecDesde;
    _txtHasta.value = this.fecHasta;

    const dataSucursal: any = await this.maestroService.obtenerSucursales().toPromise();
    // console.log("sucursal", dataSucursal);
    if(dataSucursal.estado == "True"){
      this.listaSucursalesAux = dataSucursal.listaTablaGeneral;
    }

    // console.log(this.idSucursal);
    
    const sucUsuario: string[] = this.idSucursal.split(",");
    for(let i = 0; i < this.listaSucursalesAux.length; i++){
      if(sucUsuario.includes(this.listaSucursalesAux[i].codigo)){
        this.listaSucursales.push(this.listaSucursalesAux[i]);
      }
    }
  }

  ngAfterViewInit(): void{
    
  }

  async buscarDocumentos(){
    this.listaDocumento = [];
    this.listaPaginas = [];
    this.pagina = 0;
    let fecIni: string;
    let fecFin: string;    

    const _txtDesde = document.getElementById("input-fecdesde") as HTMLInputElement;
    const _txtHasta = document.getElementById("input-fechasta") as HTMLInputElement;    
    const _txtProduct = document.getElementById("input-producto") as HTMLInputElement;    
    const _cmbEstado = document.getElementById("input-estado") as HTMLSelectElement;
    const _cmbCompania = document.getElementById("input-compania") as HTMLSelectElement;
    fecIni = _txtDesde.value.replace('-', '').replace('-', '');
    if (fecIni == "") fecIni = "0";

    fecFin = _txtHasta.value.replace('-', '').replace('-', '');
    if (fecFin == "") fecFin = "0";

    const estado: string = _cmbEstado.value;
    
    this.valCliente = _cmbCompania.value;
    const dataCliente: any = await this.clienteService.obtenerCliente(this.valCliente).toPromise();
    if(dataCliente.estado){
      this.valCliente = dataCliente.key;
    }

    this.documentoService.obtenerDocumentos(fecIni, fecFin, this.user, this.valCliente, estado, _txtProduct.value).subscribe((data: any) => {
      // console.log(data);
      
      if(data.estado == "True"){
        this.mensajeVacio = "";
        this.listaDocumento = data.listaPedido;

        if(this.listaDocumento.length > 0){
          const residuo: number = this.listaDocumento.length % 10;
          const cociente: string = (this.listaDocumento.length / 10).toString().split('.')[0];
          const x: number = residuo == 0 ? 0 : 1;

          let totalPaginas: number = Number(cociente) + x
          this.totalPag = totalPaginas;
          console.log(this.totalPag);
          
          if(totalPaginas > 10)
            totalPaginas = 10

          for(let i = 1; i <= totalPaginas; i++){
            this.listaPaginas.push(i);
          }
    
          this.pagina = 1;
        }else{
          this.mensajeVacio = "No se encontró datos con los filtros ingresados";
        }

      }else{
        this.mensajeVacio = "No se encontró datos con los filtros ingresados";
      }
    })
  }

  searchRow(row: any){
    // console.log(row);
    let tipo: string;
    if(row.estado == "CERRADO" || row.estado == "ABIERTO" || row.estado == "ANULADO")
      tipo = "2";
    else
      tipo ="3";

    // this.router.navigateByUrl(`/doc-mostrar/${ row.idPedido }`, { replaceUrl: true });
    const url = this.router.serializeUrl(this.router.createUrlTree([`/#/doc-mostrar/${ row.idPedido }`]));
    // console.log("url", url.replace("%23","#"))
    window.open(url.replace("%23","#"), '_blank');
  }

  selectPagina(pag: number){
    this.pagina = pag;
    let x = 1;
    let y = 10;
    console.log("pagina", pag);

    if( pag > 10 ){      
      const residuo: number = pag % 10;
      const cociente: string = (pag / 10).toString().split('.')[0];
      const xx: number = residuo == 0 ? 0 : 1;
      if(xx == 1){
        x = (Number(cociente) * 10) + 1;
        y = (Number(cociente) + 1) * 10;
        console.log(x,y);
      }
      else{
        y = pag;
        x = pag - 9;
        console.log(x,y);   
      }
    }

    this.listaPaginas = [];

    if(y > this.totalPag)
      y = this.totalPag;

    for(let i = x; i <= y; i++){
      this.listaPaginas.push(i);
    }
  }

  crearDocumentos(){
    this.router.navigateByUrl(`/doc-crear/0`, { replaceUrl: true });
  }

  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
