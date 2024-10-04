import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import ColumnHeader from 'src/app/models/columnHeader';
import { TransferenciaCab } from 'src/app/models/transferenciaCab';
import { MaestroService } from 'src/app/services/maestro.service';
import { TransferenciaService } from 'src/app/services/transferencia.service';

@Component({
  selector: 'app-transferencia-listar',
  templateUrl: './transferencia-listar.component.html',
  styleUrls: ['./transferencia-listar.component.css']
})
export class TransferenciaListarComponent implements OnInit {
  public listaHeader: ColumnHeader[] = [];
  public fecDesde: string = new Date().toISOString().slice(0, 10);
  public fecHasta: string = new Date().toISOString().slice(0, 10);
  public codVendedor: number = 0;
  public user: string = "";
  public idSucursal: string = "";
  public tipoDoc: any = 0;
  listaDocumento: TransferenciaCab[] = [];
  valCliente:string = "";
  dataUsuario: any;
  listaPaginas: number[] = [];
  listaSucursalesAux: any[] = [];
  listaSucursales: any[] = [];
  pagina: number = 0;
  totalPag: number = 0;
  mensajeVacio: string = "";

  constructor(private router: Router,
              private transferService: TransferenciaService, private maestroService: MaestroService) { 

    let dataTemp: any = sessionStorage.getItem("dataUsuario");
    this.dataUsuario = JSON.parse(dataTemp);
    if(this.dataUsuario == null || this.dataUsuario == undefined){
      this.router.navigateByUrl('login', { replaceUrl: true })
    }else{
      
      this.codVendedor = this.dataUsuario.codVendedor
      this.user = this.dataUsuario.usuario;
      this.idSucursal = this.dataUsuario.sucursales;            
    }    
  
  }

  async ngOnInit(): Promise<void> {
    this.listaHeader = [
      {
        label: "Sucursal",
        key: "sucursal",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Nro SAP",
        key: "docNum",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      // {
      //   label: "Cliente",
      //   key: "nomCliente",
      //   subKey: "",
      //   customClass: "",
      //   type: "",
      //   value: "",
      //   visible: true
      // },
      {
        label: "Fecha",
        key: "docDate",
        subKey: "",
        customClass: "fecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Almacén Origen",
        key: "codAlmacenOri",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Almacén Destino",
        key: "codAlmacenDest",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Usuario Aut.",
        key: "userReg",
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
      // {
      //   label: "",
      //   key: "nroPedido",
      //   subKey: "",
      //   customClass: "btnDel",
      //   type: "buttonDelete",
      //   value: "",
      //   visible: true
      // },
    ];

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

  crearDocumento(){
    this.router.navigateByUrl(`/transferencia-crear/0`, { replaceUrl: true });
  }

  async buscarDocumento(){
    const fecDesde: string = (document.getElementById("input-fecdesde") as HTMLInputElement).value;
    const fecHasta: string = (document.getElementById("input-fechasta") as HTMLInputElement).value;
    const estado: string = (document.getElementById("input-estado") as HTMLSelectElement).value;
    const product: string = (document.getElementById("input-producto") as HTMLSelectElement).value;
    const idcompania: string = (document.getElementById("input-compania") as HTMLSelectElement).value;
    
    console.log("estado", estado);
    this.listaDocumento = [];
    this.listaPaginas = [];
    const fd = fecDesde.replace("-", "").replace("-", "")
    const fh = fecHasta.replace("-", "").replace("-", "")
    const data: any = await this.transferService.obtenerListaTransferencia(fd, fh, estado, this.user, idcompania, product).toPromise();
    console.log("data", data);
    if(data.estado){      
      if(data.listaTransferencia.length == 0){
        this.mensajeVacio = "No se encontró datos con los filtros seleccionados"
      }else{
        this.mensajeVacio = "";
        this.listaDocumento = data.listaTransferencia;
        // console.log("Lista Data", data.listaTransferencia);
        // console.log("Lista", this.listaDocumento);
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
      }
    }
  }

  searchRow(row: any){
    console.log(row);
    
    // this.router.navigateByUrl(`/transferencia-mostrar/${ row.docEntry }`, { replaceUrl: true });
    const url = this.router.serializeUrl(this.router.createUrlTree([`/#/transferencia-mostrar/${ row.docEntry }`]));
    window.open(url.replace("%23","#"), '_blank');
  }

  async cancelRow(row: any){
    console.log(row);
    const data: any = await this.transferService.anularDocumento(row).toPromise();
    // const data: any  = await this.transferService.;
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
        this.listaPaginas = [];
        for(let i = x; i <= y; i++){
          this.listaPaginas.push(i);
        }
      }   
    }
  }

  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
