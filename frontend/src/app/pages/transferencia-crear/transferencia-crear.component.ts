import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import ColumnHeader from 'src/app/models/columnHeader';
import { Producto } from 'src/app/models/producto';
import { TablaGeneral } from 'src/app/models/tablageneral';
import { TransferenciaCab } from 'src/app/models/transferenciaCab';
import { TransferenciaDet } from 'src/app/models/transferenciaDet';
import { DocumentoService } from 'src/app/services/documento.service';
import { MaestroService } from 'src/app/services/maestro.service';
import { ProductoService } from 'src/app/services/producto.service';
import { TransferenciaService } from 'src/app/services/transferencia.service';

@Component({
  selector: 'app-transferencia-crear',
  templateUrl: './transferencia-crear.component.html',
  styleUrls: ['./transferencia-crear.component.css']
})
export class TransferenciaCrearComponent implements OnInit {
//#region "Variables"
  flagLoad :boolean = false;
  listaDocumento: TransferenciaDet[] = [];
  listaBoniSug: any[] = [];
  listaPartidaPresupuestal: any[] = [];
  headerDocumento: ColumnHeader[] = [];
  headerPromoSuge: ColumnHeader[] = [];

  listaProducto: Producto[] = [];
  listaProductoStock: Producto[] = [];

  headerProducto: ColumnHeader[] = [];
  headerProductoStock: ColumnHeader[] = [];
  descProducto: string = "";

  listaAlmacenOrigen: TablaGeneral[] = [];
  listaAlmacenDestino: TablaGeneral[] = [];
  listaSucursales: TablaGeneral[] = [];
  listaSucursalesAux: TablaGeneral[] = [];
  listaJefeAlm: TablaGeneral[] = [];
  listaDim1: TablaGeneral[] = [];
  listaDim2: TablaGeneral[] = [];
  listaDim3: TablaGeneral[] = [];
  listaDim4: TablaGeneral[] = [];
  listaProyecto: TablaGeneral[] = [];
  listaTpoOper: TablaGeneral[] = [];
  listaSeries: TablaGeneral[] = [];

  dataUsuario: any;

  product!: Producto;
  codVendedor: string = "";
  tipoDoc: string = "";
  _titulo: string = "";
  _tituloFecNec: string = "";
  jefeAlm: string = "";
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
  
  xproyecto: boolean = false;
  xtipooperacion: boolean = false;
  xmoneda: boolean = false;
  xcondicion: boolean = false;
  xxtipooperacion: boolean = false;
  xxmoneda: boolean = false;
  xxcondicion: boolean = false;
  ximpuesto: boolean = false;
  xximpuesto: boolean = false;

  idSucursal: string = "";
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

  msgError: string = "";

  codCliente: string = "";
  nomCliente: string = "";
  codAlmacen: string = "";
  codAlmacenDes: string = "";
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
  codProyecto: string;
  codEtapa: string;
  codSubEtapa: string;
  codJefeAlm: string;

  fecNecesaria: string = new Date().toISOString().slice(0, 10);//toLocaleDateString("en-US", { year: 'numeric', month: '2-digit', day: '2-digit' });
  fecDocumento: string = new Date().toISOString().slice(0, 10);
  fecContabilizacion: string = new Date().toISOString().slice(0, 10);

  idGridDoc: string = "gridDoc";
  idGridPro: string = "gridPro";

//#endregion "Variables"
  
constructor(private _route: ActivatedRoute,
    private router: Router,
    private maestroService: MaestroService,
    private artService: ProductoService,
    private documentService: DocumentoService,
    private transferService: TransferenciaService) { 
      let dataTemp: any = sessionStorage.getItem("dataUsuario");
      this.dataUsuario = JSON.parse(dataTemp);
      if(this.dataUsuario == null || this.dataUsuario == undefined){
        this.router.navigateByUrl('login', { replaceUrl: true })
      }else{        
        this.codVendedor = this.dataUsuario.codVendedor || "4"
        this.user = this.dataUsuario.usuario;
        this.codMoneda = this.dataUsuario.moneda;
        this.idSucursal = this.dataUsuario.sucursales;
        this.listaPrecioBruto = this.dataUsuario.listaPrecioBruto;
        this.diasVen = 7//this.dataUsuario.diasVencim == "" ? 0 : parseInt(this.dataUsuario.diasVencim);

        const listConfig: any[] = this.dataUsuario.configuracion.filter(t => t.formulario == "ST")
      // console.log("Lista Config", listConfig)

        if(listConfig != undefined)
        {
          for(let i = 0; i<listConfig.length; i++){
            switch (listConfig[i].code){
              case "00011":{
                this.xtipooperacion = listConfig[i].visible;
                this.xxtipooperacion = listConfig[i].editable
                this.tipoOperacion = listConfig[i].valor;
                break;
              }
            }
          } 
        }
      }
    }

  async ngOnInit(): Promise<void> {
    this._titulo = "SOLICITUD DE TRASLADO";

    const dataDimension: any = await this.maestroService.obtenerDimencion().toPromise();
    // console.log("Nombres Dimension", dataDimension)
    if (dataDimension.estado == "True") {
      for (let i = 0; i < dataDimension.listaTablaGeneral.length; i++) {
        // console.log("dimension", dataDimension.listaTablaGeneral[i].descripcion);

        switch (dataDimension.listaTablaGeneral[i].descripcion) {
          case "Dimensión 1":
            this.dimencion1 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion1 = dataDimension.listaTablaGeneral[i].descripcion;
            break;
          case "Dimensión 2":
            this.dimencion2 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion2 = dataDimension.listaTablaGeneral[i].descripcion;
            break;
          case "Dimensión 3":
            this.dimencion3 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion3 = dataDimension.listaTablaGeneral[i].descripcion;
            break;
          case "Dimensión 4":
            this.dimencion4 = dataDimension.listaTablaGeneral[i].valor01;
            this._dimencion4 = dataDimension.listaTablaGeneral[i].descripcion;
            break;
          // case "Dimensión 5":
          //   this.dimencion5 = dataDimension.listaTablaGeneral[i].valor01;
          //   this._dimencion5 = dataDimension.listaTablaGeneral[i].descripcion;
          //   break;
        }
      }
    }

    this.headerDocumento = [
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
        customClass: "",
        type: "text",
        value: "",
        visible: true
      },
      // {
      //   label: "Proyecto",
      //   key: "",
      //   subKey: "",
      //   customClass: "Proyecto",
      //   type: "select",
      //   value: "",
      //   visible: this.xproyecto
      // },      
      // {
      //   label: this.dimencion1,
      //   key: "",
      //   subKey: "",
      //   customClass: this._dimencion1,
      //   type: "select",
      //   value: "",
      //   visible: this.xdimencion1
      // },
      // {
      //   label: this.dimencion2,
      //   key: "",
      //   subKey: "",
      //   customClass: this._dimencion2,
      //   type: "select",
      //   value: "",
      //   visible: this.xdimencion2
      // },
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
        label: "*",
        key: "codArticulo",
        subKey: "",
        customClass: "btnDelete",
        type: "buttonDelete",
        value: "",
        visible: true
      }
    ];
    
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
      // {
      //   label: "Precio",
      //   key: "precioUnit",
      //   subKey: "",
      //   customClass: "",
      //   type: "",
      //   value: "",
      //   visible: true
      // },
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
    
    //Tipo Operacion
    const dataTipoOper: any = await this.maestroService.obtenerTioOperacion().toPromise();
    if(dataTipoOper.estado == "True"){
      this.listaTpoOper = dataTipoOper.listaTablaGeneral;
    }

    const dataSucursal: any = await this.maestroService.obtenerSucursales().toPromise();
    // console.log("sucursal", dataSucursal);
    if(dataSucursal.estado == "True"){
      this.listaSucursalesAux = dataSucursal.listaTablaGeneral;      
    }
    
    this.maestroService.obtenerJefeAlmacen().subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaJefeAlm = data.listaTablaGeneral;
      }
    });

    const sucUsuario: string[] = this.idSucursal.split(",");
    for(let i = 0; i < this.listaSucursalesAux.length; i++){
      if(sucUsuario.includes(this.listaSucursalesAux[i].codigo)){
        this.listaSucursales.push(this.listaSucursalesAux[i]);
      }
    }

    const _sucursal = document.getElementById("input-compania") as HTMLSelectElement;
    _sucursal.value = sucUsuario[0];

    this.obtenerListaxSucursal();

    const _txtFecDoc = document.getElementById("input-fecha-docu") as HTMLInputElement;
    _txtFecDoc.value = this.fecDocumento;
    const _txtFecNec = document.getElementById("input-fecha-necesaria") as HTMLInputElement;
    _txtFecNec.value = this.fecNecesaria;
  }

  async guardar(){
    try {
      // const tipoOpe: string = (document.getElementById("input-tip-ope") as HTMLSelectElement).value;
      // const serie: string = (document.getElementById("input-serie") as HTMLSelectElement).value;
      // const _JefeAlm: string = (document.getElementById("input-jefe") as HTMLInputElement).value;
      // const almOri: string = (document.getElementById("input-alm-ori") as HTMLSelectElement).value;
      // const almDes: string = (document.getElementById("input-alm-des") as HTMLSelectElement).value;
      // const cmbProyecto = document.getElementById("cmbProyecto") as HTMLSelectElement
      // const cmbEtapa = document.getElementById("cmbEtapa") as HTMLSelectElement
      // const cmbSubEtapa = document.getElementById("cmbSubEtapa") as HTMLSelectElement
      const comentario: string = (document.getElementById("input-comment") as HTMLInputElement).value;
      const trabajador: string = (document.getElementById("input-trabajador") as HTMLInputElement).value;
      const _FecDoc: string = (document.getElementById("input-fecha-docu") as HTMLInputElement).value;
      const _FecDue: string = (document.getElementById("input-fecha-necesaria") as HTMLInputElement).value;
      const fecDoc: Date = new Date(_FecDoc);
      const fecDue: Date = new Date(_FecDue);

      const serie = this.listaSeries.filter(t=>t.descripcion == this.series)[0].codigo;

      if(serie == ""){
        this.msgError = "\n - Debe seleccionar una serie";
        this.errorDialog = true;
        return;
      }

      if(this.codAlmacenDes == "" && this.codAlmacenDes == undefined){
        this.msgError = "\n - El almacén destino es obligatorio";
        this.errorDialog = true;
        return; 
      }

      if(this.codAlmacen == this.codAlmacenDes){
        this.msgError = "\n - El almacén origen y destino no pueden ser iguales";
        this.errorDialog = true;
        return;
      }

      if(this.listaDocumento.length == 0){
        this.msgError = "\n - Debe agregar un producto";
        this.errorDialog = true;
        return;
      }

      if(this.codJefeAlm == "" || this.codJefeAlm == undefined){
        this.msgError = "\n - Debe colocar un jefe de almacén";
        this.errorDialog = true;
        return;
      }

      this.jefeAlm = this.listaJefeAlm.filter(t=>t.descripcion == this.codJefeAlm)[0].codigo;

      for(let i = 0; i< this.listaDocumento.length; i++){
        const _table: any = document.querySelector("#gridDoc");
        const _pp = _table.rows[i + 1].cells[4].children[0].value;
        const _d3 = _table.rows[i + 1].cells[5].children[0].value;
        const _d4 = _table.rows[i + 1].cells[6].children[0].value;
        const _Cantidad = _table.rows[i + 1].cells[3].children[0].value;
        const listaPP: TablaGeneral[] = this.listaDocumento[i].listaPartida

        if(_Cantidad == "" || _Cantidad == "0"){
          this.msgError = "\n - La cantidad debe ser mayor a 0";
          this.errorDialog = true;
          return;
        }

        console.log("1"); this.listaDocumento[i].cantidad = Number(_Cantidad);
        console.log("2"); this.listaDocumento[i].codAlmacenOri = this.listaAlmacenOrigen.filter(t=>t.descripcion == this.codAlmacen)[0].codigo;
        console.log("3"); this.listaDocumento[i].codAlmacenDest = this.listaAlmacenOrigen.filter(t=>t.descripcion == this.codAlmacenDes)[0].codigo

        if(_pp == ""){
          this.msgError = "\n - La partida presupuestal es obligatorio";
          this.errorDialog = true;
          return;
        }

        for(let x = 0; x < listaPP.length; x++){
          if(listaPP[x].codigo == _pp){
            this.listaDocumento[i].idPartida = listaPP[x].codigo;
            this.listaDocumento[i].partida =listaPP[x].valor01; 
          }
        }

        this.listaDocumento[i].proyecto = this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0].codigo//cmbProyecto.value;
        this.listaDocumento[i].dimension1 = this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0].codigo//cmbEtapa.value;
        this.listaDocumento[i].dimension2 = this.listaDim2.filter(t=>t.descripcion == this.codSubEtapa)[0].codigo;
        this.listaDocumento[i].dimension3 = _d3;
        this.listaDocumento[i].dimension4 = _d4;
      }

      let transferencia: TransferenciaCab = {
        docEntry: 0,
        docNum: 0,
        codCliente: '',
        docDate: fecDoc,
        taxDate: fecDoc,
        docDueDate: fecDue,
        estado: '',
        comentario: comentario,
        codEmpleado: this.codVendedor.toString(),
        userReg: this.user,
        nomCliente: '',
        tipoOperacion: this.tipoOperacion,
        series: serie,
        sucursal: "0",
        compania: '',
        codAlmacenOri: this.listaAlmacenOrigen.filter(t=>t.descripcion == this.codAlmacen)[0].codigo,
        codAlmacenDest: this.listaAlmacenDestino.filter(t=>t.descripcion == this.codAlmacenDes)[0].codigo,
        listaDetalle: this.listaDocumento,
        idSucursal: 0,
        nombreTrabajador: trabajador,
        jefeAlm: this.jefeAlm
      }
      console.log("data enviada", transferencia);
      
      this.flagLoad = true;
      const data: any = await this.transferService.registrarDocumento(transferencia).toPromise();
      console.log("respuesta", data);
      
      this.flagLoad = false;
      if(data.estado){
        this.msgError = data.mensaje;
        if(data.estado){        
          this.confirmDialog = true;
          this.nroDoc = data.key;
        }else{
          this.errorDialog = true;
        }
      }else{
        this.msgError = data.mensaje;
        this.errorDialog = true;
      }      
    } catch (error) {
      // console.log(error);  
      this.msgError = error.toString();
      this.errorDialog = true;    
      this.flagLoad = false;
    }
  }

  openProducto(){
    this.lblMensajeVacio = "";
    // const _cmbAlmacen = document.getElementById("input-alm-ori") as HTMLSelectElement;
    // this.codAlmacen = _cmbAlmacen.value;
    console.log(this.codAlmacen, this.codProyecto, this.codEtapa);
    
    if(this.codAlmacen == "" || this.codAlmacen == undefined){
      this.msgError = "Seleccione un almacén de origen"
      this.errorDialog = true;
      return;
    }
    
    if (this.codProyecto == "" || this.codProyecto == undefined){
      this.msgError = "Seleccione un proyecto"
      this.errorDialog = true;
      return;
    }
    
    if (this.codEtapa == "" || this.codEtapa == undefined){
      this.msgError = "Seleccione una etapa"
      this.errorDialog = true;
      return;
    }

    console.log("Abre popup de productos"); 
      this.productDialog = true;
  }

  async buscarProducto(){
    this.lblMensajeVacio = "";
    this.listaPaginas = [];
    this.pagina = 0;
    this.listaProducto = [];
    // const _flag = document.getElementById("input-cb-todos") as HTMLInputElement

    // const _cmbAlmacen = document.getElementById("input-alm-ori") as HTMLSelectElement;
    // this.codAlmacen = _cmbAlmacen.value;

    const codAlm = this.listaAlmacenOrigen.filter(t=>t.descripcion == this.codAlmacen)[0].codigo

    const xFlag: string = "1"; //_flag.checked ? "1" : "0";
    const data: any = await this.artService.obtenerProducto(this.valorProducto, codAlm, "1", this.codMoneda, xFlag).toPromise();
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

  async selectProduct( _row: any){
    const _cmbAlmacenOri = document.getElementById("input-alm-ori") as HTMLSelectElement;
    const _cmbAlmacen = document.getElementById("input-alm-des") as HTMLSelectElement;
    this.codAlmacen = _cmbAlmacenOri.value;
    this.codAlmacenDes = _cmbAlmacen.value;
    const tblProd: any = document.querySelector("#tblProd");
    for(let i = 1; i < tblProd.rows.length; i++){
      const _sel = tblProd.rows[i].cells[5].children[0] as HTMLInputElement;
      console.log(_sel);
      
      const _cod = tblProd.rows[i].cells[0].innerText;
      if(_sel.checked){
        const listaAux: any = this.listaDocumento.filter(t => t.idProducto == _cod);
        if(listaAux.length > 0){
          this.productDialog = false;
          continue;
        }   
        const row: Producto = this.listaProducto.filter(t => t.codArticulo == _cod)[0];
        
        let detalle: TransferenciaDet = {
          idProducto: row.codArticulo,
          descripcion: row.descripcion,
          cantidad: 1,
          unidad: row.unidadMedida,
          codUndMed: 0,
          codAlmacenOri: this.codAlmacen,
          codAlmacenDest: this.codAlmacenDes,
          dimension1: this._xDimencion1,
          proyecto: "",
          lineNum: -1,
          estado: '',
          idPartida: '',
          partida: '',
          listaPartida: this.listaPartidaPresupuestal,
          dimension2: '',
          dimension3: '',
          dimension4: '',
          nroTransferencia:'',
          cantidadOpen: 0,
          cantidadAten: 0
        };
        // console.log(detalle);
        this.listaDocumento.push(detalle);
      }
    }

    this.listaProducto = [];
    this.productDialog = false;
    /*
    const listaAux: any = this.listaDocumento.filter(t => t.idProducto == row.codArticulo);
    
    if(listaAux.length > 0){
      this.productDialog = false;
      return;
    }   
    
    const _cmbAlmacenOri = document.getElementById("input-alm-ori") as HTMLSelectElement;
    this.codAlmacen = _cmbAlmacenOri.value;
    const _cmbAlmacen = document.getElementById("input-alm-des") as HTMLSelectElement;
    this.codAlmacenDes = _cmbAlmacen.value;

    let detalle: TransferenciaDet = {
      idProducto: row.codArticulo,
      descripcion: row.descripcion,
      cantidad: 1,
      unidad: row.unidadMedida,
      codUndMed: 0,
      codAlmacenOri: this.codAlmacen,
      codAlmacenDest: this.codAlmacenDes,
      dimension1: this._xDimencion1,
      proyecto: "",
      lineNum: -1,
      estado: '',
      idPartida: '',
      partida: '',
      listaPartida: this.listaPartidaPresupuestal,
      dimension2: '',
      dimension3: '',
      dimension4: '',
      nroTransferencia:'',
      cantidadOpen: 0,
      cantidadAten: 0
    };
   
    this.listaProducto = [];
    this.listaDocumento.push(detalle);
    this.productDialog = false;
    */
  }

  setValueGrilla(detalle: TransferenciaDet[]){    
    // const _table: any = document.querySelector("#gridDoc");
    // for(let i = 1; i < _table.rows.length; i++){
    //   _table.rows[i].cells[5].children[0].value = detalle[i-1].cantidad;
    // }
  }

  deleteProduct( row: any){
    const index: number = this.listaDocumento.indexOf(row);
    if (index !== -1) {
        this.listaDocumento.splice(index, 1);
    }
  }

  async obtenerListaxSucursal(){
    let _tipoDoc: string = "4";
    console.log("Sucursal", this.idSucursal)
    this.listaProyecto = [];
    this.listaAlmacenOrigen = []
    this.listaAlmacenDestino = []
    this.listaSeries = []
    this.codProyecto = "";
    this.codEtapa = "";
    this.codSubEtapa = "";
    this.codAlmacen = "";
    this.codAlmacenDes = "";
    
    const sucUsuario: string[] = this.idSucursal.split(",");
    if(sucUsuario.length > 1){
      this.idSucursal = sucUsuario[0]
    }

    const sucSel = this.listaSucursales.filter(t=>t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0]
    this.idSucursal = sucSel.descripcion    
    const dataAlmVen: any = await this.maestroService.obtenerAlmacenVenta(sucSel.codigo).toPromise();
    if(dataAlmVen.estado == "True"){
      console.log("alm", dataAlmVen)
      this.listaAlmacenOrigen = dataAlmVen.listaTablaGeneral;
      this.listaAlmacenDestino = dataAlmVen.listaTablaGeneral;
      this.codAlmacen = this.listaAlmacenOrigen[0].descripcion;
    }
    
    const dataSerie: any = await this.maestroService.obtenerSeries(_tipoDoc, sucSel.codigo).toPromise();
    console.log("serie", dataSerie);    
    if(dataSerie.estado == "True"){
      this.listaSeries = dataSerie.listaTablaGeneral;
      if(this.listaSeries.length > 0){
        this.series = this.listaSeries[0].descripcion;
      }
    }

    //Obtener Proyecto    
    this.maestroService.obtenerProyectos(sucSel.codigo).subscribe((data: any) => {
      console.log("Lista Proyectos", data);
      if(data.estado == "True"){
        this.listaProyecto = data.listaTablaGeneral;
      }
    });

    for(let i=0; i < this.listaDocumento.length; i++){
      this.listaPartidaPresupuestal = [];
      this.listaDocumento[i].listaPartida = [];
      this.listaDocumento[i].dimension3 = "";
      this.listaDocumento[i].dimension4 = "";
      this.listaDocumento[i].idPartida = "";
    }

  }

  async mostrarStock(item: any){
    const codProducto = item.idProducto;
    let xUndMed: string = "";
    let x: number = 0;
    const idSucursal = this.listaSucursales.filter(t=>t.descripcion = this.idSucursal)[0].codigo
    const data: any = await this.artService.obtenerProductoStock(codProducto, idSucursal).toPromise();
    console.log("stock", data);
    let lista: Producto[] = [];
      if(data.estado == "True"){
        lista = data.listaArticulo;
        for(let i = 0; i < lista.length; i++){
          let _cantAct: Number = lista[i].cantActual;
          let _cantSol: Number = lista[i].cantSolicitada;
          let _cantDis: Number = lista[i].stockDisponible;
          lista[i].cantActual = parseInt(_cantAct.toString())
          lista[i].cantSolicitada = parseInt(_cantSol.toString())
          lista[i].stockDisponible = parseInt(_cantDis.toString())
        }

        this.lblMensajeVacio = "";
        this.descProducto = lista[0].descripcion;
        this.listaProductoStock = lista;//.filter(t=> t.stockDisponible != 0)
        this.productStockDialog = true;
      }else{
        this.lblMensajeVacio = "No hay stock en ningun almacén";
        this.productStockDialog = true;
      }
  }

  async selectProyectoRow(row: any){
    let xProyecto: string = "";
    let xEtapa: string = "";
    let x: number = 0;
    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const codigo = this.listaDocumento[i -1].idProducto;
      if(row.idProducto == codigo){
        xProyecto = _table.rows[i].cells[4].children[0].value;
        xEtapa = _table.rows[i].cells[5].children[0].value;
        console.log("CodProyecto", xProyecto);
        this.listaDocumento[i - 1].proyecto = xProyecto;
        this.listaDocumento[i - 1].dimension3 = "";
        this.listaDocumento[i - 1].dimension4 = "";
        x = i-1;
      }
    }

    const dataPartida: any = await this.maestroService.obtenerPartidaPresupuestal(xProyecto, xEtapa, this.codVendedor,this.idSucursal).toPromise();
    console.log("data Partida", dataPartida);
    
    if(dataPartida.estado == "True"){
      this.listaDocumento[x].listaPartida = dataPartida.listaTablaGeneral;
    }
  }

  async selectProyecto(flag: any){
    let xProyecto: string = "";
    let xEtapa: string = "";
    let x: number = 0;
    
    // const cmbProyecto = document.getElementById("cmbProyecto") as HTMLSelectElement
    const idSucursal = this.listaSucursales.filter(t=>t.codigo == this.idSucursal || t.descripcion == this.idSucursal)[0].codigo;
    xProyecto = this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0].codigo;//cmbProyecto.value;
    if(flag == "0"){
      this.listaDim1 = [];
      this.listaDim2 = [];
      this.listaDim3 = [];
      this.listaDim4 = [];

      this.obtenerDimension();
    }

    // const cmbEtapa = document.getElementById("cmbEtapa") as HTMLSelectElement
    if(this.codEtapa != "" && this.codEtapa != undefined)
      xEtapa = this.listaDim1.filter(t=>t.descripcion == this.codEtapa)[0].codigo //cmbEtapa.value
    // console.log("data Partida envío", xProyecto, xEtapa, this.codVendedor,this.idSucursal);
    const dataPartida: any = await this.maestroService.obtenerPartidaPresupuestal(xProyecto, xEtapa, this.codVendedor,idSucursal).toPromise();
    // console.log("data Partida", dataPartida);
    
    if(dataPartida.estado == "True"){
      // this.listaDocumento[x].listaPartida = dataPartida.listaTablaGeneral;
      this.listaPartidaPresupuestal = dataPartida.listaTablaGeneral;

      for(let i=0; i<this.listaDocumento.length; i++){
        this.listaDocumento[i].listaPartida = this.listaPartidaPresupuestal;
        this.listaDocumento[i].dimension3 = "";
        this.listaDocumento[i].dimension4 = "";
        this.listaDocumento[i].idPartida = "";
      }

    }
  }

  async selectPartida(row: any){
    let xPartida: string = "";
    let x: number = 0;
    const _table: any = document.querySelector("#gridDoc");
    for(let i = 1; i < _table.rows.length; i++){
      const codigo = this.listaDocumento[i -1].idProducto;
      if(row.idProducto == codigo){
        xPartida = _table.rows[i].cells[4].children[0].value;
        // console.log(xProyecto);
        const listaPartida = this.listaDocumento[i - 1].listaPartida;

        for(let j=0; j<listaPartida.length; j++){
          if(listaPartida[j].codigo == xPartida){
            // this.listaDocumento[i -1].cuentaContable = listaPartida[j].valor02
            this.listaDocumento[i -1].dimension3 = listaPartida[j].valor03
            this.listaDocumento[i -1].dimension4 = listaPartida[j].valor04
          }
        }
        x = i-1;
      }
    }
  }

  closeDialogConfirm(){
    this.router.navigateByUrl(`/transferencia-mostrar/${ this.nroDoc }`, { replaceUrl: true });
  }

  async obtenerDimension(){
    const cmbProyecto = this.listaProyecto.filter(t=>t.descripcion == this.codProyecto)[0]//document.getElementById("cmbProyecto") as HTMLSelectElement;
    const idSucursal = this.listaSucursales.filter(t=>t.descripcion == this.idSucursal)[0].codigo
    
    //Obtener Subdimencion1
    this.maestroService.obtenerSubDimencion("1", idSucursal, cmbProyecto.codigo).subscribe((data: any) => {
      // console.log("dimencion", data);
      if(data.estado == "True"){
        this.listaDim1 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion2
    this.maestroService.obtenerSubDimencion("2", idSucursal, cmbProyecto.codigo).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim2 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion3
    this.maestroService.obtenerSubDimencion("3", idSucursal, cmbProyecto.codigo).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim3 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion4
    this.maestroService.obtenerSubDimencion("4", idSucursal, cmbProyecto.codigo).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim4 = data.listaTablaGeneral;
      }
    });
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

  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
