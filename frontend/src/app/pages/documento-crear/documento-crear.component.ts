import { Component, HostListener, OnInit, ViewEncapsulation } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';

import { Cliente } from 'src/app/models/cliente';
import ColumnHeader from 'src/app/models/columnHeader';
import { PedidoCab } from 'src/app/models/pedidocab';
import { PedidoDet } from 'src/app/models/pedidodet';
import { Producto } from 'src/app/models/producto';
import { TablaGeneral } from 'src/app/models/tablageneral';

import { ClienteService } from 'src/app/services/cliente.service';
import { DocumentoService } from 'src/app/services/documento.service';
// import { GeolocationService } from 'src/app/services/geolocation.service';
import { MaestroService } from 'src/app/services/maestro.service';
import { ProductoService } from 'src/app/services/producto.service';

@Component({
  selector: 'app-documento-crear',
  templateUrl: './documento-crear.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./documento-crear.component.scss']
})
export class DocumentoCrearComponent implements OnInit {
//#region "Variables"
  flagLoad :boolean = false;
  pedidoCopy!: PedidoCab;
  pedido!: PedidoCab;
  listaDocumento: PedidoDet[] = [];
  listaBoniSug: any[] = [];
  listaOC: any[] = [];
  
  headerDocumento: ColumnHeader[] = [];
  headerProducto: ColumnHeader[] = [];
  headerProductoStock: ColumnHeader[] = [];
  headerCliente: ColumnHeader[] = [];
  headerOC: ColumnHeader[] = [];

  listaProducto: Producto[] = [];
  listaProductoStock: Producto[] = [];

  descProducto: string = "";

  listaCliente: Cliente[] = [];
  clienteSelect: any;

  listaAlmacen: TablaGeneral[] = [];
  listaAlmacenCliente: TablaGeneral[] = [];
  listaCondPago: TablaGeneral[] = [];
  listaMoneda: TablaGeneral[] = [];
  listaImpuesto: TablaGeneral[] = [];
  listaSucursales: TablaGeneral[] = [];
  listaSucursalesAux: TablaGeneral[] = [];
  listaDim1: TablaGeneral[] = [];
  listaDim2: TablaGeneral[] = [];
  listaDim3: TablaGeneral[] = [];
  listaDim4: TablaGeneral[] = [];
  listaProyecto: TablaGeneral[] = [];
  listaTpoOper: TablaGeneral[] = [];
  listaSeries: TablaGeneral[] = [];
  listaMedioEnvio: TablaGeneral[] = [];
  listaJefeAlmacen: TablaGeneral[] = [];

  dataUsuario: any;

  product!: Producto;
  codVendedor: string = "";
  _titulo: string = "";
  _tituloFecNec: string = "";
  valorProducto = "";
  valorCliente = "";
  
  dimencion1: string = "";
  _xDimencion1: string = "";
  _dimencion1: string = "";
  xdimencion1: boolean = false;

  dimencion2: string = "";
  _xDimencion2: string = "";
  _dimencion2: string = "";
  xdimencion2: boolean = false;

  dimencion3: string = "";
  _xDimencion3: string = "";
  _dimencion3: string = "";
  xdimencion3: boolean = false;

  dimencion4: string = "";
  _xDimencion4: string = "";
  _dimencion4: string = "";
  xdimencion4: boolean = false;

  dimencion5: string = "";
  _xDimencion5: string = "";
  _dimencion5: string = "";
  xdimencion5: boolean = false;


  xproyecto: boolean = false;
  xtipooperacion: boolean = false;
  xmoneda: boolean = false;
  xcondicion: boolean = false;
  xxtipooperacion: boolean = false;
  xxmoneda: boolean = false;
  xxcondicion: boolean = false;
  ximpuesto: boolean = false;
  xximpuesto: boolean = false;

  prodSel: string = "";
  idSucursal: string = "";
  jefeAlm: string = "";
  nroDoc: number = 0;
  listaPaginas: number[] = [];
  pagina: number = 0;
  totalPag: number = 0;
  diasVen: number = 0;
  
  medioEnvio: number = 0;
  productDialog: boolean = false;
  productStockDialog: boolean = false;
  clienteDialog: boolean = false;
  deleteProductDialog: boolean = false;
  errorDialog: boolean = false;
  confirmDialog: boolean = false;
  confirmDialogCliente: boolean = false;
  flagSelectCliente: boolean = false;
  listaPrecioBruto:boolean = false;
  promoSugDialog:boolean = false;
  flagMostrarOC: boolean = false;
  flagMostrarFT: boolean = false;
  
  msgError: string = "";

  codCliente: string = "";
  nomCliente: string = "";
  codAlmacen: string = "";
  codCondPag: string = "";
  codDireccion: string = "";
  codMoneda: string = "";
  user: string = "";
  rucCliente: string = "";
  commentario: string = "";
  codImpuesto: string = "";
  subTotal: string = "0.00";
  docTotal: string = "0.00";
  impuesto: string = "0.00";
  tipoOperacion: string = "";
  series: string = "";
  nroDocumento: string | null = "";
  _idDocumento: Number = 0;
  descuento: string = "0.00";
  _descuentoGlobal: number;
  lblMensajeVacio: string;
  numeroOperacion: string;
  jefeAlmacen: string;
  codProyecto: string;
  codEtapa: string;
  codSubEtapa: string;

  fecNecesaria: string = new Date().toISOString().slice(0, 10);//toLocaleDateString("en-US", { year: 'numeric', month: '2-digit', day: '2-digit' });
  fecDocumento: string = new Date().toISOString().slice(0, 10);
  fecContabilizacion: string = new Date().toISOString().slice(0, 10);

  idGridDoc: string = "gridDoc";
  idGridCli: string = "gridCli";
  idGridPro: string = "gridPro";

  nombreTrabajador: string = "";
  precioLista: string = "1";
  unidadMedidaAux: string[] = [];
  listaPartida: string[] = [];
  listaPartidaPresupuestal: any[] = [];

//#endregion "Variables"
 
  constructor(private _route: ActivatedRoute,
              private router: Router,
              private maestroService: MaestroService,
              private artService: ProductoService,
              private clienteService: ClienteService,
              private documentService: DocumentoService,
              private sanitizer: DomSanitizer
              ){
    let dataTemp: any = sessionStorage.getItem("dataUsuario");
    this.dataUsuario = JSON.parse(dataTemp);
    if(this.dataUsuario == null || this.dataUsuario == undefined){
      this.router.navigateByUrl('login', { replaceUrl: true })
    }else{
      
      this.codVendedor = this.dataUsuario.codVendedor
      this.user = this.dataUsuario.usuario;
      this.precioLista = this.dataUsuario.listaPrecio;
      this.codMoneda = this.dataUsuario.moneda;
      this.idSucursal = this.dataUsuario.sucursales;
      this.listaPrecioBruto = this.dataUsuario.listaPrecioBruto;
      this.diasVen = 0//this.dataUsuario.diasVencim == "" ? 0 : parseInt(this.dataUsuario.diasVencim);

      const listConfig: any[] = this.dataUsuario.configuracion.filter(t => t.formulario == "SC")
      // console.log("Lista Config", listConfig)

      if(listConfig != undefined)
      for(let i = 0; i<listConfig.length; i++){
        switch (listConfig[i].code){
          case "00001":{
            this.xtipooperacion = listConfig[i].visible;
            this.xxtipooperacion = listConfig[i].editable
            this.tipoOperacion = listConfig[i].valor;
            break;
          }
          case "00002":{
            this.xdimencion1 = listConfig[i].visible
            this._xDimencion1 = listConfig[i].valor;
            break;
          }
          case "00003":{
            this.xdimencion2 = listConfig[i].visible
            this._xDimencion2 = listConfig[i].valor;
            break;
          }
          case "00004":{
            this.xdimencion3 = listConfig[i].visible
            this._xDimencion3 = listConfig[i].valor;
            break;
          }
          case "00005":{
            this.xdimencion4 = listConfig[i].visible
            this._xDimencion4 = listConfig[i].valor;
            break;
          }
          case "00006":{
            this.xdimencion5 = listConfig[i].visible
            this._xDimencion5 = listConfig[i].valor;
            break;
          }
          case "00007":{
            this.xmoneda = listConfig[i].visible;
            this.xxmoneda = listConfig[i].editable;
            break;
          }
          case "00008":{
            this.xcondicion = listConfig[i].visible
            this.xxcondicion = listConfig[i].editable
            this.codCondPag = listConfig[i].valor
            break;
          }
          case "00009":{
            this.xproyecto = listConfig[i].visible
            break;
          }
          case "00010":{
            this.ximpuesto = listConfig[i].visible
            this.xximpuesto = listConfig[i].editable
            this.codImpuesto = listConfig[i].valor
            break;
          }
        }
      }    
    }
    
    // console.log(this.fecNecesaria);
    let xTipoDoc: string | null;
    this._tituloFecNec = "Fecha de Entrega";
    this._titulo = "SOLICITUD DE CONSUMO";
            
    this.headerProducto = [
      {
        label: "Código",
        key: "codArticulo",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Descripción",
        key: "descripcion",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Unidad",
        key: "unidadMedida",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Costo",
        key: "precioUnit",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Stock",
        key: "cantActual",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Disponible",
        key: "stockDisponible",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Marcar",
        key: "codArticulo",
        subKey: "",
        customClass: "btnEdit",
        type: "buttonSelect",
        value: "",
        visible: true
      },
    ];

    this.headerProductoStock = [
      {
        label: "Almacén",
        key: "codAlmacen",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Stock",
        key: "cantActual",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Comprometido",
        key: "cantComprom",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Solicitado",
        key: "cantSolicitada",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Disponible",
        key: "stockDisponible",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Stock Min",
        key: "stockMin",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
    ];

    this.headerOC = [
      {
        label: "Id OC",
        key: "idPedido",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Num OC",
        key: "nroPedido",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "N° Linea",
        key: "lineId",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Fecha OC",
        key: "fecPedido",
        subKey: "",
        customClass: "fecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Cod.Partida",
        key: "codPartida",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Partida",
        key: "partida",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Cantidad",
        key: "saldo",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Sel",
        key: "idPedido",
        subKey: "",
        customClass: "btnEdit",
        type: "buttonSelect",
        value: "",
        visible: true
      },
    ];

    this.headerCliente = [
      {
        label: "Código",
        key: "codigoCliente",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Razón Social",
        key: "nombre",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Linea Cred.",
        key: "lineaCredito",
        subKey: "",
        customClass: "right",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Saldo",
        key: "saldoDisponible",
        subKey: "",
        customClass: "right",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "",
        key: "codigoCliente",
        subKey: "",
        customClass: "btnEdit",
        type: "buttonSelect",
        value: "",
        visible: true
      },
    ];

    // this.maestroService.obtenerCondicionPago().subscribe((data: any) => {
    //   if(data.estado == "True"){
    //     this.listaCondPago = data.listaTablaGeneral;
  
    //     console.log(this.codCondPag);      
    //     const cpSel = this.listaCondPago.filter(t=>t.codigo == this.codCondPag)[0];
    //     console.log(cpSel);      
    //     this.codCondPag = cpSel.descripcion; 
    //   }
    // });
    
    this.maestroService.obtenerMoneda().subscribe((data: any) => {
      if(data.estado == "True"){
        this.listaMoneda = data.listaTablaGeneral;      
      }
    });
        
    this.maestroService.obtenerImpuesto().subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaImpuesto = data.listaTablaGeneral;
      }
    });

    this.maestroService.obtenerJefeAlmacen().subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaJefeAlmacen = data.listaTablaGeneral;
      }
    });

    this.maestroService.obtenerTioOperacion().subscribe((dataTipoOper: any) => {
      if(dataTipoOper.estado == "True"){
        this.listaTpoOper = dataTipoOper.listaTablaGeneral;

        const tpoSel = this.listaTpoOper.filter(t => t.codigo == this.tipoOperacion)[0];
        this.tipoOperacion = tpoSel.descripcion;
      }
    });
    
    this.maestroService.obtenerSucursales().subscribe((dataSucursal: any) => {
      // console.log("sucursal", dataSucursal);
      if(dataSucursal.estado == "True"){
        this.listaSucursalesAux = dataSucursal.listaTablaGeneral;
        // console.log(this.idSucursal);    
        const sucUsuario: string[] = this.idSucursal.split(",");
        for(let i = 0; i < this.listaSucursalesAux.length; i++){
          if(sucUsuario.includes(this.listaSucursalesAux[i].codigo)){
            this.listaSucursales.push(this.listaSucursalesAux[i]);
          }
        }

        // const _sucursal = document.getElementById("input-compania") as HTMLSelectElement;
        // _sucursal.value = sucUsuario[0];

        this.obtenerListaxSucursal();
      }
    });
    
    let nroDoc: string | null;
    if(this._route.snapshot.paramMap.get("nro"))
      nroDoc = this._route.snapshot.paramMap.get("nro");
    else
      nroDoc = "";

    let xFecha: Date = new Date()
    
    // const _txtFecDoc = document.getElementById("input-fecha-docu") as HTMLInputElement;
    // _txtFecDoc.value = this.fecDocumento;
    // const _txtFecNec = document.getElementById("input-fecha-necesaria") as HTMLInputElement;
    xFecha.setDate(xFecha.getDate() + this.diasVen)
    this.fecNecesaria = xFecha.toISOString().slice(0, 10);
    // _txtFecNec.value = this.fecNecesaria;
  }

  async ngOnInit(): Promise<void> {
    const dataDimension:any = await this.maestroService.obtenerDimencion().toPromise()
    
    if (dataDimension.estado == "True") {
      for (let i = 0; i < dataDimension.listaTablaGeneral.length; i++) {
        // console.log("dimension", dataDimension.listaTablaGeneral[i].descripcion);  
        switch (dataDimension.listaTablaGeneral[i].descripcion) {
          case "Dimensión 1":
            this.dimencion1 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion1 = dataDimension.listaTablaGeneral[i].descripcion;
            // console.log(this.dimencion1, this._dimencion1);
            break;
          case "Dimensión 2":
            this.dimencion2 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion2 = dataDimension.listaTablaGeneral[i].descripcion;
            // console.log(this.dimencion2, this._dimencion2);
            break;
          case "Dimensión 3":
            this.dimencion3 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion3 = dataDimension.listaTablaGeneral[i].descripcion;
            // console.log(this.dimencion3, this._dimencion3);
            break;
          case "Dimensión 4":
            this.dimencion4 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion4 = dataDimension.listaTablaGeneral[i].descripcion;
            // console.log(this.dimencion4, this._dimencion4);
            break;
          case "Dimensión 5":
            this.dimencion5 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion5 = dataDimension.listaTablaGeneral[i].descripcion;
            // console.log(this.dimencion5, this._dimencion5);
            break;
        }
      }
    }

    let headerDocumentoAux: ColumnHeader[] = [
      {
        label: "Código",
        key: "idProducto",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Descripción",
        key: "descripcion",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Costo",
        key: "precioUnit",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "UndMed",
        key: "unidad",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Cantidad",
        key: "",
        subKey: "",
        customClass: "cantidad",
        type: "text",
        value: "",
        visible: true
      },
      {
        label: "Total",
        key: "precioTotal",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Id OC",
        key: "docEntryOc",
        subKey: "",
        customClass: "OC",
        type: "text",
        value: "",
        visible: true
      },
      {
        label: "Nro OC / FT",
        key: "docNumOc",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Id Fac",
        key: "numFacOC",
        subKey: "",
        customClass: "FT",
        type: "text",
        value: "",
        visible: true
      },
      {
        label: "Partida Presupuestal",
        key: "",
        subKey: "",
        customClass: "listaPartida",
        type: "select",
        value: "",
        visible: true
      },
      {
        label: this.dimencion3,
        key: "",
        subKey: "",
        customClass: this._dimencion3,
        type: "select",
        value: "",
        visible: this.xdimencion3
      },
      {
        label: this.dimencion4,
        key: "",
        subKey: "",
        customClass: this._dimencion4,
        type: "select",
        value: "",
        visible: this.xdimencion4
      },
      {
        label: this.dimencion5,
        key: "",
        subKey: "",
        customClass: this._dimencion5,
        type: "select",
        value: "",
        visible: this.xdimencion5
      },
      {
        label: "*",
        key: "codArticulo",
        subKey: "",
        customClass: "btnDelete",
        type: "buttonDelete",
        value: "",
        visible: true
      }
    ];

    // console.log(this.xdimencion3, this.dimencion3)
    // console.log(this.xdimencion3, this.dimencion4)
    // console.log("col con false",headerDocumentoAux)
    let colDel: string[] = []
    for(let i=0; i<headerDocumentoAux.length; i++){
      // console.log("col con false",headerDocumentoAux[i])
      if(headerDocumentoAux[i].visible == false){
        colDel.push(headerDocumentoAux[i].label)
      }
    }
    // console.log("coldel", colDel)
    for(let j=0; j<colDel.length; j++){
      for(let x=0; x<headerDocumentoAux.length; x++){
        if(colDel[j] == headerDocumentoAux[x].label){
          headerDocumentoAux.splice(x, 1);
        }
      }
    }

    // console.log("col sin false",headerDocumentoAux)
    this.headerDocumento = headerDocumentoAux

  }

  openCliente(){
    this.lblMensajeVacio = "";
    this.clienteDialog = true;
  }

  openProducto(){
    this.lblMensajeVacio = "";
    const _cmbAlmacen = document.getElementById("input-alm-ven") as HTMLSelectElement;
    this.codAlmacen = _cmbAlmacen.value;
    if(this.codAlmacen == ""){
      this.msgError = "Seleccione un almacén de venta"
      this.errorDialog = true;
    }else{
      this.productDialog = true;
      // this.modalService.open(content, { size: 'xl' });
    }

  }

  async buscarProducto(){
    this.lblMensajeVacio = "";
    this.listaPaginas = [];
    this.pagina = 0;
    this.listaProducto = [];
    // const _almacen: any = document.querySelector("#cmbAlmacen");
    // const almacen: string = _almacen.value;    
    const _flag = document.getElementById("input-cb-todos") as HTMLInputElement
    const _valor = document.getElementById("input-valProd") as HTMLInputElement
    const _alm = this.listaAlmacen.filter(t=>t.descripcion == this.codAlmacen)[0]//document.getElementById("input-alm-ven") as HTMLSelectElement
    const xFlag: string = "1";//_flag.checked ? "1" : "0";

    const xValor: string = _valor.value;

    console.log(xValor, _valor.value);
    const data: any = await this.artService.obtenerProducto(xValor, _alm.codigo , "1", "SOL", xFlag).toPromise();
    console.log("productos", data);
    
    if(data.estado == "True"){
      this.lblMensajeVacio = "";
      this.listaPaginas = [];
      this.pagina = 1;
      this.listaProducto = data.listaArticulo;
      const residuo: number = this.listaProducto.length % 10;
      const cociente: string = (this.listaProducto.length / 10).toString().split('.')[0];
      const x: number = residuo == 0 ? 0 : 1;

      let totalPaginas: number = Number(cociente) + x
      this.totalPag = totalPaginas;
      console.log(this.totalPag);
      
      if(totalPaginas > 5)
        totalPaginas = 5

      for(let i = 1; i <= totalPaginas; i++){
        this.listaPaginas.push(i);
      }
    }else{
      this.lblMensajeVacio = data.mensaje;
    }
  }

  deleteProduct( row: any){
    const index: number = this.listaDocumento.indexOf(row);
    if (index !== -1) {
        this.listaDocumento.splice(index, 1);
    }
    this.actualizarCantDel();
  }

  actualizarCant(){    
    const _table: any = document.querySelector("#gridDoc");
    let _subTotal: Number = 0;
    let _impuesto: Number = 0;
    let _docTotal: Number = 0;
    let _docTotalSD: Number = 0;

    console.log("lineas", _table.rows.length);
    const _txtSubTotal = document.getElementById("input-subtotal") as HTMLInputElement;
    const _txtImouesto = document.getElementById("input-impuesto") as HTMLInputElement;
    const _txtTotal = document.getElementById("input-total") as HTMLInputElement;
    this.codMoneda = "SOL";

    if(_table.rows.length - 1 == this.listaDocumento.length){
      for(let i = 1; i < _table.rows.length; i++){
        
        const xCant: Number = Number(_table.rows[i].cells[4].children[0].value);
        const xPrec: Number = this.listaDocumento[i-1].precioUnit;
        
        const xDesc: Number = 1 - (Number(this.listaDocumento[i-1].descuento) / 100)
        let xPrecioTot: Number = Number((Number(xCant) * Number(xPrec) * Number(xDesc)).toFixed(2));
        let xPreSinDes: Number = Number((Number(xCant) * Number(xPrec)).toFixed(2));
        _docTotalSD = Number(_docTotalSD) + Number(xPreSinDes);

        this.listaDocumento[i-1].precioTotal = xPrecioTot;
        this.listaDocumento[i-1].cantidad = xCant;
        this.listaDocumento[i-1].precioUnit = xPrec;

        _subTotal = Number(_subTotal) + Number(xPrecioTot);
      }
            
      const descuento:number = 0; //Number((Number(_subTotal) * Number((_txtPorDescuento.value == "" ? "0" : _txtPorDescuento.value)) / 100).toFixed(2));
      // _txtDescuento.value = descuento.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      
      if(this.codImpuesto == "IGV"){
        _impuesto = Number(((Number(_subTotal) - descuento )* 0.18).toFixed(2));
      }

      _docTotal = Number(_subTotal) + Number(_impuesto) - descuento

      this.subTotal = _subTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.impuesto = _impuesto.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.docTotal = _docTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });     
      
    }else{
      let stot: number = 0;
      for(let i = 0; i < this.listaDocumento.length; i++){
        stot += Number(this.listaDocumento[i].precioTotal);
      }

      this.subTotal = stot.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.impuesto = (stot * 0.18).toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.docTotal = (stot * 1.18).toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
    }

    _txtSubTotal.value = this.subTotal;    
    _txtImouesto.value = this.impuesto;    
    _txtTotal.value = this.docTotal;
    console.log("total", this.docTotal);
  }

  actualizarCantDel(){
    
    let _subTotal: Number = 0;
    let _impuesto: Number = 0;
    let _docTotal: Number = 0;

    for(let i = 0; i < this.listaDocumento.length; i++){
      const xCant: Number = this.listaDocumento[i].cantidad;
      const xPrec: Number = this.listaDocumento[i].precioUnit;
      let xPrecioTot: Number = Number((Number(xCant) * Number(xPrec)).toFixed(2));

      _subTotal = Number(_subTotal) + Number(xPrecioTot);
    }

    if(this.codImpuesto == "IGV"){
      _impuesto = Number((Number(_subTotal) * 0.18).toFixed(2));
    }

    _docTotal = Number(_subTotal) + Number(_impuesto)

    this.subTotal = _subTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
    this.impuesto = _impuesto.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
    this.docTotal = _docTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });

    const _txtSubTotal = document.getElementById("input-subtotal") as HTMLInputElement;
    _txtSubTotal.value = this.subTotal;

    const _txtImouesto = document.getElementById("input-impuesto") as HTMLInputElement;
    _txtImouesto.value = this.impuesto;

    const _txtTotal = document.getElementById("input-total") as HTMLInputElement;
    _txtTotal.value = this.docTotal;
  }

  actualizarDesc( row: any){
    // console.log(row);
    const _table: any = document.querySelector("#gridDoc");
    let _subTotal: Number = 0;
    let _impuesto: Number = 0;
    let _docTotal: Number = 0;
    let _descuento: Number = 0;

    for(let i = 1; i < _table.rows.length; i++){
      const xCant: Number = Number(_table.rows[i].cells[4].children[0].value);
      const xPrec: Number = this.listaDocumento[i-1].precioUnit;

      const xPorDescuento: Number = Number(_table.rows[i].cells[5].children[0].value);
      let xPrecioTot: Number = Number(xCant) * Number(xPrec);

      let xImporteConDesc: Number = Number(xPrecioTot) * ( 1 - (Number(xPorDescuento) / 100))

      this.listaDocumento[i-1].precioTotal = xImporteConDesc;

      _subTotal = Number(_subTotal) + Number(xImporteConDesc);
    }

    if(this.codImpuesto == "IGV"){
      _impuesto = Number(_subTotal) * 0.18;
    }

    _docTotal = Number(_subTotal) + Number(_impuesto)

    this.subTotal = _subTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
    this.impuesto = _impuesto.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
    this.docTotal = _docTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });

    const _txtSubTotal = document.getElementById("input-subtotal") as HTMLInputElement;
    _txtSubTotal.value = this.subTotal;

    const _txtImouesto = document.getElementById("input-impuesto") as HTMLInputElement;
    _txtImouesto.value = this.impuesto;

    const _txtTotal = document.getElementById("input-total") as HTMLInputElement;
    _txtTotal.value = this.docTotal;
  }

  async selectProduct( _row: any){
    const tblProd: any = document.querySelector("#tblProd");
    for(let i = 1; i < tblProd.rows.length; i++){
      const _sel = tblProd.rows[i].cells[6].children[0] as HTMLInputElement;
      const _cod = tblProd.rows[i].cells[0].innerText;
      if(_sel.checked){
        console.log("codigo Seleccionado", _cod)
        const listaAux: any = this.listaDocumento.filter(t => t.idProducto == _cod);
        const row: Producto = this.listaProducto.filter(t => t.codArticulo == _cod)[0];
        
        if(listaAux.length > 0){
          this.productDialog = false;
          continue;
        }
        
        let _PrecioUnit: number = 0;

        if (this.listaPrecioBruto){
          _PrecioUnit = Number((Number(row.precioUnit) / 1.18).toFixed(2));
        }else{
          _PrecioUnit = Number(row.precioUnit);
        }

        let detalle: PedidoDet = {
          idProducto: row.codArticulo,
          descripcion: row.descripcion,
          cantidad: 1,
          unidad: row.unidadMedida,
          codUndMed: 0,
          precioUnit: _PrecioUnit,
          precioBruto: Number(row.precioUnit),
          precioTotal: _PrecioUnit,
          tipoImpuesto: this.codImpuesto,
          codAlmacen: this.codAlmacen,
          stockDisponible: row.stockDisponible,
          dimension1: this._xDimencion1,
          proyecto: "",
          lineNum: -1,
          descuento: 0,
          listaUnd: [],
          listaPartida: this.listaPartidaPresupuestal,
          idPartida: '',
          partida: '',
          dimension2: '',
          dimension3: '',
          dimension4: '',
          cuentaContable: '',
          cantOpen: 0,
          cantAten: 0,
          nroEntrega: '',          
          docEntryOc: '',
          docNumOc: '',
          numFacOC: '',
          numLine: ''
        };
        this.listaDocumento.push(detalle);        
      }
    }
    this.listaProducto = [];
    this.actualizarCant();
    console.log("detalle", this.listaDocumento);
    
    this.setValueGrilla(this.listaDocumento)
    this.productDialog = false;

  }

  confirmDelete(){

  }

  closeModalProd(){
    this.listaProducto = [];
    this.productDialog = false;
  }

  closeDialog(){
    this.errorDialog = false;
  }

  async guardar(){
    let mensaje = await this.validarDatos();

    if(mensaje != ""){

      this.msgError = mensaje;
      this.errorDialog = true;
      return;
    }

    const _fecNecesaria: Date = new Date(this.fecNecesaria);
    const _fecDocumento: Date = new Date(this.fecDocumento);
    const _fecContabilizacion: Date = new Date(this.fecContabilizacion);

    const dir: any = this.listaAlmacenCliente.find(t => t.codigo === this.codDireccion);
    // console.log("almacen cliente", dir);
    // console.log("geolocation", this.locationService.userLocation);
    let latitude: string = "";
    let longitude: string = "";

    // if(!!this.locationService.userLocation){
    //   longitude = this.locationService.userLocation[0].toString()
    //   latitude = this.locationService.userLocation[1].toString();
    // }

    this.flagLoad = true;
    
    // let pedido: PedidoCab = {
    //   idPedido: this._idDocumento,
    //   nroPedido: '',
    //   codCliente: this.codCliente,
    //   condPago: this.codCondPag,
    //   fecSolicitado: _fecNecesaria,
    //   fecPedido: _fecDocumento,
    //   fecContabilizacion: _fecContabilizacion,
    //   nroOC: '',
    //   subTotal: 0,
    //   descuento: this._descuentoGlobal,
    //   importeTotal: 0,
    //   moneda: this.codMoneda,
    //   estado: '',
    //   comentario: this.commentario,
    //   codVendedor: this.codVendedor.toString(),
    //   codDireccion: '',//this.codDireccion,
    //   direccion: '', //dir.descripcion,
    //   userReg: this.user,
    //   latitud: latitude,
    //   longitud: longitude,
    //   nomCliente: this.nomCliente,
    //   rucCliente: this.rucCliente,
    //   tipoOperacion: this.tipoOperacion,
    //   series: this.series,
    //   listaDetalle: this.listaDocumento,
    //   sucursal: Number(this.idSucursal),
    //   medioEnvio: this.medioEnvio,
    //   estadoPed: "",
    //   nombreTrabajador: this.nombreTrabajador,
    //   motivoAnulacion: "",
    //   jefeAlm: this.jefeAlm
    // };

    console.log("data envio", this.pedido);

    if(this.pedido.idPedido == 0){
      const data: any = await this.documentService.registrarDocumento(this.pedido).toPromise();
      // console.log("rpta reg", data);
      this.flagLoad = false;
      
      this.msgError = data.mensaje;
      if(data.estado){        
        this.confirmDialog = true;
        this.nroDoc = data.key;
      }else{
        this.errorDialog = true;
      }
    }    
  }

  async validarDatos(): Promise<string> {
    let xRpta: string = "";

    const _fecNecesaria: Date = new Date(this.fecNecesaria);
    const _fecDocumento: Date = new Date(this.fecDocumento);
    const _fecContabilizacion: Date = new Date();
    const sucSel = this.listaSucursales.filter(t => t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0];

    //Obtener Cliente
    const dataCliente: any = await this.clienteService.obtenerCliente(sucSel.codigo).toPromise();
    if(dataCliente.estado){
      this.codCliente = dataCliente.key;
    }

    if(this.codCliente == ""){
      xRpta += "\n - La sucursal debe tener asignado un cliente";
    }

    if(this.codAlmacen == ""){
      xRpta += "\n - Debe seleccionar una almacén de venta";
    }

    if(this.nombreTrabajador == ""){
      xRpta += "\n - Debe colocar un trabajador";
    }
    console.log("jefe", this.jefeAlmacen);
    
    if(this.jefeAlmacen == "" || this.jefeAlmacen == undefined){
      xRpta += "\n - Debe colocar un jefe de almacén";
    }

    if(this.codProyecto == "" || this.codProyecto == undefined){
      xRpta += "\n - Debe seleccionar un Proyecto";
    }

    if(this.codEtapa == "" || this.codEtapa == undefined){
      xRpta += "\n - Debe seleccionar una Etapa";
    }

    if(this.codSubEtapa == "" || this.codSubEtapa == undefined){
      xRpta += "\n - Debe seleccionar una Sub-Etapa";
    }

    if(this.listaDocumento == null || this.listaDocumento == undefined || this.listaDocumento.length == 0){
      xRpta += "\n - Debe agregar un producto";
    }

    if(xRpta != "")
      return xRpta = "Hay campos que deben ser llenados: " + xRpta.split("\n").join("<br />");

    this.pedido = {
      idPedido: this._idDocumento,
      nroPedido: '',
      codCliente: this.codCliente,
      condPago: this.codCondPag,
      fecSolicitado: _fecNecesaria,
      fecPedido: _fecDocumento,
      fecContabilizacion: _fecContabilizacion,
      nroOC: '',
      subTotal: 0,
      descuento: this._descuentoGlobal,
      importeTotal: 0,
      moneda: this.codMoneda,
      estado: '',
      comentario: this.commentario,
      codVendedor: this.codVendedor.toString(),
      codDireccion: '',//this.codDireccion,
      direccion: '', //dir.descripcion,
      userReg: this.user,
      latitud: '',//latitude,
      longitud: '',//longitude,
      nomCliente: this.nomCliente,
      rucCliente: this.rucCliente,
      tipoOperacion: (this.listaTpoOper.filter(t=>t.descripcion == this.tipoOperacion)[0]).codigo,
      series: (this.listaSeries.filter(t=>t.descripcion == this.series)[0]).codigo,
      listaDetalle: this.listaDocumento,
      sucursal: Number((this.listaSucursales.filter(t=>t.descripcion == this.idSucursal)[0]).codigo),
      medioEnvio: this.medioEnvio,
      estadoPed: "",
      nombreTrabajador: this.nombreTrabajador,
      motivoAnulacion: "",
      jefeAlm: (this.listaJefeAlmacen.filter(t=>t.descripcion == this.jefeAlmacen)[0]).codigo
    }

    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const xCant: string = _table.rows[i].cells[4].children[0].value;
      if(xCant == "" || xCant == "0"){
        xRpta += `\n - Debe colocar una cantidad en la fila ${ i }`
      }else{
        
        const xDim3: string = _table.rows[i].cells[10].children[0].value;
        const xDim4: string = _table.rows[i].cells[11].children[0].value;

        this.listaDocumento[i - 1].proyecto = (this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]).codigo;
        this.listaDocumento[i - 1].dimension1 = (this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0]).codigo;
        this.listaDocumento[i - 1].dimension2 = (this.listaDim2.filter(t=>t.descripcion == this.codSubEtapa)[0]).codigo;
        this.listaDocumento[i - 1].dimension3 = xDim3;
        this.listaDocumento[i - 1].dimension4 = xDim4;
        this.listaDocumento[i - 1].codAlmacen = (this.listaAlmacen.filter(t=>t.descripcion == this.codAlmacen)[0]).codigo;
      }
    }

    for(let i = 0; i< this.listaDocumento.length; i++){
      //const _table: any = document.querySelector("#gridDoc");
      const _pp = _table.rows[i + 1].cells[9].children[0].value;

      if(_pp == ""){
        xRpta += `\n - En la línea ${i + 1} la partida presupuestal es obligatoria. \n`;        
      }

      const listaPP: TablaGeneral[] = this.listaDocumento[i].listaPartida
      for(let x = 0; x < listaPP.length; x++){
        if(listaPP[x].codigo == _pp){
          this.listaDocumento[i].idPartida = listaPP[x].codigo;
          this.listaDocumento[i].partida =listaPP[x].valor01; 
        }
      }               
    }

    if(xRpta != ""){
      xRpta = "Hay campos que deben ser llenados: " + xRpta.split("\n").join("<br />");
    }

    return xRpta;
  }

  closeDialogConfirm(){
    // console.log("OK");
    this.router.navigateByUrl(`/doc-mostrar/${ this.nroDoc }`, { replaceUrl: true });
  }

  async actualizarPrecio(row: any){
    let xUndMed: string = "";
    let x: number = 0;
    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const codigo = this.listaDocumento[i -1].idProducto;
      if(row.idProducto == codigo){
        xUndMed = _table.rows[i].cells[4].children[0].value;
        console.log(xUndMed);
        this.listaDocumento[i - 1].unidad = xUndMed;
        x = i-1;
      }
    }

    const _cmbAlmVen = document.getElementById("input-alm-ven") as HTMLSelectElement
    const dataStock: any = await this.artService.obtenerStockUnd(_cmbAlmVen.value, xUndMed, row.idProducto).toPromise();
    let stockUnd: Number;
    if (dataStock.estado == "True"){
      const prodaux: Producto = dataStock.listaArticulo[0];
      stockUnd = prodaux.cantActual;
    }


    // console.log(this.precioLista + "-" + this.codMoneda + "-" + xUndMed,row.idProducto);
    const data: any = await this.artService.obtenerProductPrecio(this.precioLista, this.codMoneda, xUndMed, row.idProducto).toPromise();
    // console.log(data);
    
    if (data.estado == "True"){
      const prodaux: Producto = data.listaArticulo[0];
      let _PrecioUnit: number;
      if (this.listaPrecioBruto){
        _PrecioUnit = Number((Number(prodaux.precioUnit) / 1.18).toFixed(2));
      }else{
        _PrecioUnit = Number(prodaux.precioUnit);
      }

      this.listaDocumento[x].precioUnit = _PrecioUnit;
      this.listaDocumento[x].stockDisponible = parseInt(stockUnd.toString());
      this.actualizarCant();
    }else{
      this.errorDialog = true;
      this.msgError = "No hay precio para la unidad de medida seleccionado";
      _table.rows[x+1].cells[4].children[0].value = this.unidadMedidaAux[x]
    }    
  }

  async selectProyectoRow(row: any){
    console.log(row);
    let xProyecto: string = "";
    let xEtapa: string = "";
    let x: number = 0;
    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const codigo = this.listaDocumento[i -1].idProducto;
      if(row.idProducto == codigo){
        xProyecto = _table.rows[i].cells[6].children[0].value;
        xEtapa = _table.rows[i].cells[7].children[0].value;
        
        this.listaDocumento[i - 1].proyecto = xProyecto;
        this.listaDocumento[i - 1].dimension1 = xEtapa;
        this.listaDocumento[i - 1].dimension3 = "";
        this.listaDocumento[i - 1].dimension4 = "";

        x = i-1;
      }
    }
    // console.log("data Partida envío", xProyecto, xEtapa, this.codVendedor,this.idSucursal);
    const dataPartida: any = await this.maestroService.obtenerPartidaPresupuestal(xProyecto, xEtapa, this.codVendedor,this.idSucursal).toPromise();
    // console.log("data Partida", dataPartida);
    
    if(dataPartida.estado == "True"){
      this.listaDocumento[x].listaPartida = dataPartida.listaTablaGeneral;
    }
  }

  async selectProyecto(flag: any){
    let xProyecto: string = "";
    let xEtapa: string = "";
    let x: number = 0;
    // const cmbEtapa = document.getElementById("cmbEtapa") as HTMLSelectElement    
    // const cmbProyecto = document.getElementById("cmbProyecto") as HTMLSelectElement
    xProyecto = (this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]).codigo; //cmbProyecto.value;
    console.log(this.codEtapa);    
    if (this.codEtapa != "" && this.codEtapa != undefined)
      xEtapa = (this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0]).codigo || ""; //cmbEtapa.value

    this.listaAlmacen = [];
    const sucSel = this.listaSucursales.filter(t => t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0];
    if(flag == "0"){
      this.listaDim1 = [];
      this.listaDim2 = [];
      this.listaDim3 = [];
      this.listaDim4 = [];
      await this.obtenerDimencion(sucSel.codigo);
    }else{
      const dataAlm: any = await this.maestroService.obtenerAlmacenEtapa(sucSel.codigo, xEtapa).toPromise();
      if(dataAlm.estado == "True"){
        this.listaAlmacen = dataAlm.listaTablaGeneral;
        this.codAlmacen = this.listaAlmacen[0].descripcion;
        // const _cmbAlmacen = document.getElementById("input-alm-ven") as HTMLSelectElement;
        // _cmbAlmacen.value = this.codAlmacen;
      }
    }
    
    // console.log("data Partida envío", xProyecto, xEtapa, this.codVendedor,this.idSucursal);
    const dataPartida: any = await this.maestroService.obtenerPartidaPresupuestal(xProyecto, xEtapa, this.codVendedor, sucSel.codigo).toPromise();
    // console.log("data Partida", dataPartida);
    
    if(dataPartida.estado == "True"){
      // this.listaDocumento[x].listaPartida = dataPartida.listaTablaGeneral;
      this.listaPartidaPresupuestal = dataPartida.listaTablaGeneral;
      for(let i=0; i<this.listaDocumento.length; i++){
        this.listaDocumento[i].listaPartida = this.listaPartidaPresupuestal;
        this.listaDocumento[i].dimension3 = "";
        this.listaDocumento[i].dimension4 = "";
        this.listaDocumento[i].idPartida = "";
        this.listaDocumento[i].docEntryOc = "";
        this.listaDocumento[i].docNumOc = "";
        this.listaDocumento[i].numLine = "";
        this.listaDocumento[i].numFacOC = "";
      }
    }    
  }

  async selectPartida(row: any){
    console.log("select", row);
    
    let xPartida: string = "";
    let x: number = 0;
    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const codigo = this.listaDocumento[i -1].idProducto;
      if(row.idProducto == codigo){
        xPartida = this.listaDocumento[i-1].idPartida;//_table.rows[i].cells[9].children[0].value;
        console.log(xPartida);
        const listaPartida = this.listaDocumento[i - 1].listaPartida;
        // console.log(xPartida, listaPartida);
        
        for(let j=0; j<listaPartida.length; j++){
          if(listaPartida[j].codigo == xPartida){
            this.listaDocumento[i -1].cuentaContable = listaPartida[j].valor02
            this.listaDocumento[i -1].dimension3 = listaPartida[j].valor03
            this.listaDocumento[i -1].dimension4 = listaPartida[j].valor04
          }
        }
        x = i-1;
      }
    } 
  }

  mostrarStock(item: any){
    this.lblMensajeVacio = "";
    const codProducto = item.idProducto;
    this.listaProductoStock = [];
    const sucSel = this.listaSucursales.filter(t=>t.descripcion == this.idSucursal)[0]
    this.artService.obtenerProductoStock(codProducto, sucSel.codigo).subscribe((data: any) => {
      console.log("stock", data);
      let lista: Producto[] = [];
      if(data.estado == "True"){
        lista = data.listaArticulo;
        for(let i = 0; i < lista.length; i++){
          let _cantAct: Number = lista[i].cantActual;
          let _cantSol: Number = lista[i].cantSolicitada;
          let _cantDis: Number = lista[i].stockDisponible;
          let _cantMin: Number = lista[i].stockMin;
          let _cantCom: Number = lista[i].cantComprom;
          lista[i].cantActual = parseInt(_cantAct.toString())
          lista[i].cantSolicitada = parseInt(_cantSol.toString())
          lista[i].stockMin = parseInt(_cantMin.toString())
          lista[i].cantComprom = parseInt(_cantCom.toString())
        }

        this.descProducto = lista[0].descripcion;
        this.listaProductoStock = lista;//.filter(t=> t.stockDisponible != 0)
        this.productStockDialog = true;
      }else{
        this.lblMensajeVacio = "No hay stock en ningun almacén";
        this.productStockDialog = true;
      }
    });
  }

  async obtenerListaxSucursal(){
    let _tipoDoc: string = "2";
    this.series = "";
    this.codProyecto = "";
    this.codEtapa = "";
    this.codSubEtapa = "";
    this.codAlmacen = "";

    this.listaAlmacen = [];
    const sucUsuario: string[] = this.idSucursal.split(",");
    // console.log(sucUsuario, this.idSucursal);
    if(sucUsuario.length > 1){
      this.idSucursal = sucUsuario[0]
    }
    // console.log(this.idSucursal);
    const sucSel = this.listaSucursales.filter(t => t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0];
    // console.log(sucSel.codigo, sucSel);
    const dataSerie: any = await this.maestroService.obtenerSeries(_tipoDoc, sucSel.codigo).toPromise();
    // console.log("Serie", dataSerie);
    if(dataSerie.estado == "True"){
      this.listaSeries = dataSerie.listaTablaGeneral;
      if(this.listaSeries.length > 0){
        this.series = this.listaSeries[0].descripcion;   
      }
    }
    
    //Obtener proyecto
    if(this.xproyecto){
      this.maestroService.obtenerProyectos(sucSel.codigo).subscribe((data: any) => {
        // console.log(data);
        this.listaDim1 = [];
        this.listaDim2 = [];
        this.listaDim3 = [];
        this.listaDim4 = [];

        if(data.estado == "True"){
          this.listaProyecto = data.listaTablaGeneral;
        }
      });

      this.idSucursal = sucSel.descripcion;
    }

    for(let i = 0; i < this.listaDocumento.length; i++){
      // this.listaDocumento[i].dimension1 = "";
      // this.listaDocumento[i].dimension2 = "";
      this.listaDocumento[i].dimension3 = "";
      this.listaDocumento[i].dimension4 = "";
      this.listaDocumento[i].listaPartida = [];
      this.listaDocumento[i].docEntryOc = "";
      this.listaDocumento[i].docNumOc = "";
      this.listaDocumento[i].numLine = "";
      this.listaDocumento[i].numFacOC = "";
      this.listaPartidaPresupuestal = [];
    }

    // console.log("cto", this.tipoOperacion);
  }

  selectPagina(pag: number){
    this.pagina = pag;

    this.pagina = pag;
    let x = 1;
    let y = 5;
    console.log("pagina", pag);

    if( pag > 5 ){      
      const residuo: number = pag % 5;
      const cociente: string = (pag / 5).toString().split('.')[0];
      const xx: number = residuo == 0 ? 0 : 1;
      if(xx == 1){
        x = (Number(cociente) * 5) + 1;
        y = (Number(cociente) + 1) * 5;
        console.log(x,y);
      }
      else{
        y = pag;
        x = pag - 4;
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

  columnasGrillaDetalle(config: any){
    if(this.dataUsuario.config){

    }
  }

  setValueGrilla(detalle: PedidoDet[]){
    
    // const _table: any = document.querySelector("#gridDoc");
    // for(let i = 1; i < _table.rows.length; i++){
    //   _table.rows[i].cells[5].children[0].value = detalle[i-1].cantidad;
    // }
  }

  descuentoGlobal(){
    const _txtPorDescuento = document.getElementById("input-por-descuento") as HTMLInputElement;
    const _txtDescuento = document.getElementById("input-descuento") as HTMLInputElement;
    const _txtSubTotal = document.getElementById("input-subtotal") as HTMLInputElement;
    const _txtImpuesto = document.getElementById("input-impuesto") as HTMLInputElement;
    const _txtTotal = document.getElementById("input-total") as HTMLInputElement;

    let _impuesto: number = 0
    let _docTotal: number = 0

    if(_txtSubTotal.value != "" && _txtSubTotal.value != "0.00"){
      const descuento: number = Number(_txtSubTotal.value.replace("USD ", "")) * (Number(_txtPorDescuento.value) / 100)
      
      if(this.codImpuesto == "IGV"){
        _impuesto = Number(((Number(_txtSubTotal.value.replace("USD ", "")) - descuento )* 0.18).toFixed(2));
      }
      
      _docTotal = Number(_txtSubTotal.value.replace("USD ", "")) + _impuesto - descuento;
      
      _txtDescuento.value = descuento.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.impuesto = _impuesto.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
      this.docTotal = _docTotal.toLocaleString("es-PE", { style: "currency", currency: this.codMoneda });
        
      _txtImpuesto.value = this.impuesto;    
      _txtTotal.value = this.docTotal;

    }
  }

  obtenerDimencion(idSucursal: string){
    // const cmbProyecto = document.getElementById("cmbProyecto") as HTMLSelectElement
    const xProyecto: string = (this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]).codigo //cmbProyecto.value;
    //Obtener Subdimencion1
    if (this.xdimencion1){
      this.maestroService.obtenerSubDimencion("1", idSucursal, xProyecto).subscribe((data: any) => {
        // console.log(data);
        if(data.estado == "True"){
          this.listaDim1 = data.listaTablaGeneral;
        }
      });
    }

    //Obtener Subdimencion2
    if (this.xdimencion1){
      this.maestroService.obtenerSubDimencion("2", idSucursal, xProyecto).subscribe((data: any) => {
        // console.log(data);
        if(data.estado == "True"){
          this.listaDim2 = data.listaTablaGeneral;
        }
      });
    }

    //Obtener Subdimencion3
    if (this.xdimencion1){
      this.maestroService.obtenerSubDimencion("3", idSucursal, xProyecto).subscribe((data: any) => {
        // console.log(data);
        if(data.estado == "True"){
          this.listaDim3 = data.listaTablaGeneral;
        }
      });
    }

    //Obtener Subdimencion4
    if (this.xdimencion1){
      this.maestroService.obtenerSubDimencion("4", idSucursal, xProyecto).subscribe((data: any) => {
        // console.log(data);
        if(data.estado == "True"){
          this.listaDim4 = data.listaTablaGeneral;
        }
      });
    }

  }

  mostrarOC(row: any){
    this.flagMostrarOC = true;
    this.listaOC = [];
    const proyecto = (this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]).codigo;
    const etapa = (this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0]).codigo;
    const sucursal = (this.listaSucursales.filter(t => t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0]).codigo;
    this.prodSel = row.idProducto;
    this.documentService.obtenerOC(proyecto, etapa, sucursal, row.idProducto).subscribe((data: any) => {
      console.log("OC", data);
      
      if(data.estado == "True"){
        this.listaOC =  data.listaPedido
      }
    });
  }
  
  selectOC(row: any){
    for(let i=0; i<this.listaDocumento.length; i++){
      if(this.listaDocumento[i].idProducto == this.prodSel){
        this.listaDocumento[i].docEntryOc = row.idPedido.toString();
        this.listaDocumento[i].docNumOc = row.nroPedido.toString();
        this.listaDocumento[i].idPartida = row.codPartida.toString();
        this.listaDocumento[i].partida = row.partida.toString();
        this.listaDocumento[i].numLine = row.lineId.toString();
        this.listaDocumento[i].numFacOC = "";
        this.selectPartida(this.listaDocumento[i]);
        break;
      }
    }
    this.listaOC = [];
    this.flagMostrarOC = false;
  }

  mostrarFT(row: any){
    this.flagMostrarFT = true;
    this.listaOC = [];
    const proyecto = (this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]).codigo;
    const etapa = (this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0]).codigo;
    const sucursal = (this.listaSucursales.filter(t => t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0]).codigo;
    this.prodSel = row.idProducto;
    this.documentService.obtenerFT(proyecto, etapa, sucursal, row.idProducto).subscribe((data: any) => {
      console.log("FT", data);
      
      if(data.estado == "True"){
        this.listaOC =  data.listaPedido
      }
    });
  }

  selectFT(row: any){
    for(let i=0; i<this.listaDocumento.length; i++){
      if(this.listaDocumento[i].idProducto == this.prodSel){
        this.listaDocumento[i].docEntryOc = "";
        this.listaDocumento[i].docNumOc = row.nroPedido.toString();
        this.listaDocumento[i].idPartida = row.codPartida.toString();
        this.listaDocumento[i].partida = row.partida.toString();
        this.listaDocumento[i].numLine = row.lineId.toString();
        this.listaDocumento[i].numFacOC = row.idPedido.toString();
        this.selectPartida(this.listaDocumento[i]);
        break;
      }
    }
    this.listaOC = [];
    this.flagMostrarFT = false;
  }
}
