import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import ColumnHeader from 'src/app/models/columnHeader';
import { PedidoCab } from 'src/app/models/pedidocab';
import { PedidoDet } from 'src/app/models/pedidodet';
import { TablaGeneral } from 'src/app/models/tablageneral';
import { DocumentoService } from 'src/app/services/documento.service';
import { MaestroService } from 'src/app/services/maestro.service';
import { environment } from 'src/environment/enviroment'

@Component({
  selector: 'app-documento-mostrar',
  templateUrl: './documento-mostrar.component.html',
  styleUrls: ['./documento-mostrar.component.scss']
})
export class DocumentoMostrarComponent implements OnInit {
  pedido: PedidoCab = {
    motivoAnulacion: "",
    idPedido: undefined,
    nroPedido: '',
    codCliente: '',
    condPago: '',
    nroOC: '',
    subTotal: undefined,
    descuento: undefined,
    importeTotal: undefined,
    moneda: '',
    estado: '',
    comentario: '',
    codVendedor: '',
    codDireccion: '',
    direccion: '',
    userReg: '',
    latitud: '',
    longitud: '',
    nomCliente: '',
    rucCliente: '',
    tipoOperacion: '',
    series: '',
    sucursal: 0,
    medioEnvio: 0,
    estadoPed: '',
    nombreTrabajador: '',
    jefeAlm: '',
    listaDetalle: []
  };
  pedidoDet: PedidoDet[] = [];
  headerDocumento: ColumnHeader[] = [];

  headerProducto: ColumnHeader[] = [];
  headerCliente: ColumnHeader[] = [];

  listaAlmacen: TablaGeneral[] = [];
  listaAlmacenCliente: TablaGeneral[] = [];
  listaCondPago: TablaGeneral[] = [];
  listaMoneda: TablaGeneral[] = [];
  listaImpuesto: TablaGeneral[] = [];
  listaSucursales: TablaGeneral[] = [];

  listaDim1: TablaGeneral[] = [];
  listaDim2: TablaGeneral[] = [];
  listaDim3: TablaGeneral[] = [];
  listaDim4: TablaGeneral[] = [];
  listaDim5: TablaGeneral[] = [];
  listaProyecto: TablaGeneral[] = [];
  listaTpoOper: TablaGeneral[] = [];
  listaSeries: TablaGeneral[] = [];
  litaMotivo: TablaGeneral[] = [];

  dataUsuario: any;

  codVendedor: string = "";
  tipoDoc: string = "";
  _titulo: string = "";
  lblMensajeVacio: string = "";
  valorProducto = "";
  valorCliente = "";
  dimencion1: string = "";
  dimencion2: string = "";
  dimencion3: string = "";
  dimencion4: string = "";
  dimencion5: string = "";
  _dimencion1: string = "";
  _dimencion2: string = "";
  _dimencion3: string = "";
  _dimencion4: string = "";
  _dimencion5: string = "";

  nroDoc: number = 0;
  numeDoc: string = "";
  key: string = "";

  productDialog: boolean = false;
  clienteDialog: boolean = false;
  deleteProductDialog: boolean = false;
  errorDialog: boolean = false;
  confirmDialog: boolean = false;
  copyTo: boolean = false;
  editCoti: boolean = false;
  anularDialog: boolean = false;
  estado: string = "";

  msgError: string = "";

  codCliente: string = "";
  nomCliente: string = "";
  codAlmacen: string = "";
  codCondPag: string = "";
  codDireccion: string = "";
  codMoneda: string = "SOL";
  user: string = "";
  rucCliente: string = "";
  commentario: string = "";
  codImpuesto: string = "IGV";
  subTotal: string = "";
  docTotal: string = "";
  impuesto: string = "";
  tipoOperacion: string = "";
  series: string = "";
  nroDocumento: string | null = "";
  _idDocumento: Number = 0;
  idSucursal: Number = 0;

  fecNecesaria: string = new Date().toISOString().slice(0, 10);//toLocaleDateString("en-US", { year: 'numeric', month: '2-digit', day: '2-digit' });
  fecDocumento: string = new Date().toISOString().slice(0, 10);
  fecContabilizacion: string = new Date().toISOString().slice(0, 10);

  idGridDoc: string = "gridDoc";
  idGridCli: string = "gridCli";
  idGridPro: string = "gridPro";

  precioLista: string = "1";

  _codProyecto: string = "";
  _codDimensi1: string = "";
  _codDimensi2: string = "";

  constructor (private _route: ActivatedRoute, 
               private router: Router, 
               private maestroService: MaestroService, 
               private documentoService: DocumentoService
               ){
    let dataTemp: any = sessionStorage.getItem("dataUsuario");
    this.dataUsuario = JSON.parse(dataTemp);
    if(this.dataUsuario == null || this.dataUsuario == undefined){
      this.router.navigateByUrl('login', { replaceUrl: true })
    }else{
      this.codVendedor = this.dataUsuario.codVendedor
      this.user = this.dataUsuario.usuario;
      this.idSucursal = this.dataUsuario.sucursal;
    }
  }

  async ngOnInit(): Promise<void> {
    let xTipoDoc: string | null;
    let xNroDoc: string | null;

    xTipoDoc = this._route.snapshot.paramMap.get("tipo") || "";
    xNroDoc = this._route.snapshot.paramMap.get("nro") || "";

    // console.log("NroDoc", xNroDoc);
    // console.log("TipoDoc", xTipoDoc);
    

    this.tipoDoc = xTipoDoc;
    this.numeDoc = xNroDoc;

    this._titulo = "SOLICITUD DE CONSUMO";

    const dataDimension: any = await this.maestroService.obtenerDimencion().toPromise();
    
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
        // case "Dimension 5":
        //   this.dimencion5 = dataDimension.listaTablaGeneral[i].valor01;
        //   this._dimencion5 = dataDimension.listaTablaGeneral[i].descripcion;
        //   break;
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
        label: "Precio",
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
        key: "cantidad",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },        
      {
        label: "Cant.Aten",
        key: "cantAten",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Cant.Pend",
        key: "cantOpen",
        subKey: "",
        customClass: "derecha",
        type: "",
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
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Nro SAP OC / FT",
        key: "docNumOc",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "N° Linea",
        key: "numLine",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Id FT",
        key: "numFacOC",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Partida Presupuestal",
        key: "partida",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: this.dimencion3,
        key: "dimension3",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: this.dimencion4,
        key: "dimension4",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "NroEntrega",
        key: "nroEntrega",
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
      }
    ];
    
    const dataCondPago: any = await this.maestroService.obtenerCondicionPago().toPromise();
    if(dataCondPago.estado == "True"){
      this.listaCondPago = dataCondPago.listaTablaGeneral;
    }

    const dataMoneda: any = await this.maestroService.obtenerMoneda().toPromise();
    if(dataMoneda.estado == "True"){
      this.listaMoneda = dataMoneda.listaTablaGeneral;      
    }
    
    const dataImpuesto: any = await this.maestroService.obtenerImpuesto().toPromise();
    if(dataImpuesto.estado == "True"){
      this.listaImpuesto = dataImpuesto.listaTablaGeneral;
    }

    const dataTipoOperacion: any = await this.maestroService.obtenerTioOperacion().toPromise();
    if(dataTipoOperacion.estado == "True"){
      this.listaTpoOper = dataTipoOperacion.listaTablaGeneral;
    }

    const dataMotivo: any = await this.maestroService.obtenerMotivoAnulacion().toPromise();
    if(dataMotivo.estado == "True"){
      this.litaMotivo = dataMotivo.listaTablaGeneral;
    }
    // this.obtenerListaxSucursal()

    this.obtenerDocumento();    
  }

  closeDialog(){
    this.errorDialog = false;
  }

  copiarPedido(){
    this.router.navigateByUrl(`/doc-crear/${ this.numeDoc }/3`, { replaceUrl: true });
  }  

  editarPedido(){
    this.router.navigateByUrl(`/doc-crear/${ this.numeDoc }/${ this.tipoDoc }`, { replaceUrl: true });
  }
  
  closeDialogConfirm(){
    this.confirmDialog = false;
    // this.router.navigateByUrl(`/mostrar/${ this.key }/3`, { replaceUrl: true });
    // window.location.reload();
  }

  async obtenerListaxSucursal(){
    console.log("sucursal", this.idSucursal);
    
    const dataAlmVen: any = await this.maestroService.obtenerAlmacenVenta(this.idSucursal.toString()).toPromise();
    // console.log("almacen venta", dataAlmVen);    
    if(dataAlmVen.estado == "True"){
      this.listaAlmacen = dataAlmVen.listaTablaGeneral;
      this.codAlmacen = this.listaAlmacen[0].codigo;
      const _cmbAlmacen = document.getElementById("input-alm-ven") as HTMLSelectElement;
      _cmbAlmacen.value = this.codAlmacen;
    }
    
    let tipo: string = "2";
    const dataSerie: any = await this.maestroService.obtenerSeries(tipo, this.idSucursal.toString()).toPromise();
    // console.log("series", dataSerie);
    if(dataSerie.estado == "True"){
      this.listaSeries = dataSerie.listaTablaGeneral;
      this.series = this.listaSeries[0].codigo;    
      const _cmbSerie = document.getElementById("input-serie") as HTMLSelectElement;
      _cmbSerie.value = this.series;
    }

    //Proyecto
    const dataProyecto: any = await this.maestroService.obtenerProyectos(this.idSucursal.toString()).toPromise();
    if(dataProyecto.estado == "True"){
      this.listaProyecto = dataProyecto.listaTablaGeneral;
    }
  }

  async descargarReporte(){
    let tipo: string;
    if(this.tipoDoc == "1")
      tipo = "cot"
     if(this.tipoDoc == "2")
      tipo = "ped"
    if(this.tipoDoc == "3")
      tipo = "bor"
    const xUrl: string = environment.urlrpt;
    const url: string = `${xUrl}/index.aspx?Parametros=P_DOCENTRY|${this._idDocumento},${tipo}`;

    window.open(url, "_blank");

    // const data: any = await this.documentoService.obtenerReporte(this._idDocumento.toString(), this.tipoDoc).toPromise();
    // console.log("Reporte", data)
    // if(data.estado){
    //   var byteCharacters = atob(data.rpt);
    //   var byteNumbers = new Array(byteCharacters.length);

    //   for (var i = 0; i < byteCharacters.length; i++) {
    //       byteNumbers[i] = byteCharacters.charCodeAt(i);
    //   }

    //   var byteArray = new Uint8Array(byteNumbers); 
 
    //   let filename = `${ this.nroDocumento }.pdf`;  
    //   let binaryData = [];
    //   binaryData.push(byteArray);
    //   let downloadLink = document.createElement('a');
    //   downloadLink.href = window.URL.createObjectURL(
    //   new Blob(binaryData, { type: 'blob' }));
    //   downloadLink.setAttribute('download', filename);
    //   document.body.appendChild(downloadLink);
    //   downloadLink.click();
    // }
  }

  mostrarAnulacion(){
    this.lblMensajeVacio = "";
    this.anularDialog = true;
  }

  async cancelPedido(){
    const _motAnulacion = document.getElementById("input-mot-anula") as HTMLSelectElement

    if(_motAnulacion.value == ""){
      this.lblMensajeVacio = "Tiene que seleccionar un motivo";
      return;
    }
    else{
      this.lblMensajeVacio = "";
    }

    let pedido: PedidoCab = {
      idPedido: Number(this.numeDoc),
      nroPedido: '',
      codCliente: '',
      condPago: '',
      nroOC: '',
      subTotal: 0,
      descuento: 0,
      importeTotal: 0,
      moneda: '',
      estado: '',
      comentario: _motAnulacion.value,
      codVendedor: '',
      codDireccion: '',
      direccion: '',
      userReg: '',
      latitud: '',
      longitud: '',
      nomCliente: '',
      rucCliente: '',
      tipoOperacion: '',
      series: '',
      sucursal: 0,
      medioEnvio: 0,
      estadoPed: '',
      listaDetalle: [],
      nombreTrabajador: '',
      motivoAnulacion: '',
      jefeAlm: ''
    }
    const data: any = await this.documentoService.cancelarDocumento(pedido).toPromise();

    this.errorDialog = true;
    this.msgError = data.mensaje
    
    this._titulo = "SOLICITUD DE CONSUMO";

    this.anularDialog = false;

    this.obtenerDocumento();
  }

  async obtenerDocumento(){
    const dataDocumento: any = await this.documentoService.obtenerDocumento(this.numeDoc, this.tipoDoc).toPromise();

    console.log("documento", dataDocumento);    

    if(dataDocumento.estado == "True"){
      this.pedido = dataDocumento.listaPedido[0];
      this.idSucursal = this.pedido.sucursal;
      this.nroDocumento = this.pedido.nroPedido;
      this._idDocumento = this.pedido.idPedido;
      this.pedidoDet = this.pedido.listaDetalle;

      await this.obtenerListaxSucursal();

      // const _txtCodCli = document.getElementById("input-cod-cliente") as HTMLInputElement;
      // _txtCodCli.value = this.pedido.codCliente;
      // const _txtNomCli = document.getElementById("input-razon-social") as HTMLInputElement;
      // _txtNomCli.value = this.pedido.nomCliente;
      const _txtFecDoc = document.getElementById("input-fecha-docu") as HTMLInputElement;
      const _txtFecNec = document.getElementById("input-fecha-necesaria") as HTMLInputElement;
      const _txtTrabajador = document.getElementById("input-trabajador") as HTMLInputElement;
      const _txtCompania = document.getElementById("input-compania") as HTMLInputElement;
      const _txtComent = document.getElementById("input-comment") as HTMLInputElement;
      const _cmbSeries = document.getElementById("input-serie") as HTMLSelectElement
      const _cmbMoneda = document.getElementById("input-cod-moneda") as HTMLSelectElement
      const _cmbAlmVen = document.getElementById("input-alm-ven") as HTMLSelectElement
      const _cmbImpues = document.getElementById("input-cod-imp") as HTMLSelectElement
      const _cmbTpoOpe = document.getElementById("input-tip-ope") as HTMLSelectElement
      const _cmbProyecto = document.getElementById("cmbProyecto") as HTMLSelectElement;
      const _cmbEtapa = document.getElementById("cmbEtapa") as HTMLSelectElement;
      const _cmbSubEtapa = document.getElementById("cmbSubEtapa") as HTMLSelectElement;
      const _txtSubTotal = document.getElementById("input-subtotal") as HTMLInputElement;
      const _txtImpuesto = document.getElementById("input-impuesto") as HTMLInputElement;
      const _txtDocTotal = document.getElementById("input-total") as HTMLInputElement;
      const _txtJefeAlm = document.getElementById("input-jefe") as HTMLSelectElement;
      
      _txtFecDoc.value = this.pedido.fecContabilizacion.toString().substring(0, 10) || "";
      _txtFecNec.value = this.pedido.fecSolicitado.toString().substring(0, 10) || "";
      _txtTrabajador.value = this.pedido.nombreTrabajador;
      _txtCompania.value = dataDocumento.listaPedido[0].compania;
      _txtComent.value = this.pedido.comentario || "";
      _cmbSeries.value = this.pedido.series;
      _cmbMoneda.value = this.pedido.moneda;      
      _txtJefeAlm.value = this.pedido.jefeAlm;      
      _cmbAlmVen.value = this.pedidoDet[0].codAlmacen;
      _cmbImpues.value = this.pedidoDet[0].tipoImpuesto;
      _cmbTpoOpe.value = this.pedido.tipoOperacion;
      _cmbProyecto.value = this.pedidoDet[0].proyecto;
      this._codProyecto = this.pedidoDet[0].proyecto;
      _cmbEtapa.value = this.pedidoDet[0].dimension1;
      this._codDimensi1 = this.pedidoDet[0].dimension1;
      _cmbSubEtapa.value = this.pedidoDet[0].dimension2;
      this._codDimensi2 = this.pedidoDet[0].dimension2;
      
      this.obtenerDimencion();

      for(let i=0; i < this.pedidoDet.length; i++){
        const dim3 = this.pedidoDet[i].dimension3;
        const dim4 = this.pedidoDet[i].dimension4;
        const listaAux3 = this.listaDim3.filter(t => t.codigo == dim3);

        if(listaAux3.length > 0)
        this.pedidoDet[i].dimension3 = `${ listaAux3[0].descripcion }`

        const listaAux4 = this.listaDim4.filter(t => t.codigo == dim4);
        if(listaAux4.length > 0)
        this.pedidoDet[i].dimension4 = `${ listaAux4[0].descripcion }`
      }

      this._titulo = `${ this._titulo } N° ${ this.pedido.nroPedido }`

      let _subTotal: Number = 0; 
      let _impuesto: Number = 0; 
      let _docTotal: Number = 0; 

      for(let i = 0; i < this.pedidoDet.length; i++){
        _subTotal = Number(_subTotal) + Number(this.pedidoDet[i].precioTotal);
      }

      if(this.pedidoDet[0].tipoImpuesto == "IGV"){
        _impuesto = (Number(_subTotal) * (1-(Number(this.pedido.descuento)/100))) * 0.18
      }

      _docTotal = Number(_subTotal) + Number(_impuesto);
      
      this.subTotal = _subTotal.toLocaleString("es-PE", { style: "currency", currency: this.pedido.moneda });
      this.impuesto = _impuesto.toLocaleString("es-PE", { style: "currency", currency: this.pedido.moneda });
      this.docTotal = _docTotal.toLocaleString("es-PE", { style: "currency", currency: this.pedido.moneda });
  
      _txtSubTotal.value = this.subTotal;
      _txtImpuesto.value = this.impuesto;
      _txtDocTotal.value = this.docTotal;

      this.estado = this.pedido.estadoPed;
    }
  }

  async obtenerDimencion(){
    //Obtener Subdimencion1
    const datadim1: any = await this.maestroService.obtenerSubDimencion("1", this.idSucursal.toString(), this._codProyecto).toPromise();
    if(datadim1.estado == "True"){
      this.listaDim1 = datadim1.listaTablaGeneral;
    }

    //Obtener Subdimencion2
    const datadim2: any = await this.maestroService.obtenerSubDimencion("2", this.idSucursal.toString(), this._codProyecto).toPromise();
    if(datadim2.estado == "True"){
      this.listaDim2 = datadim2.listaTablaGeneral;
    }

    //Obtener Subdimencion3
    const datadim3: any = this.maestroService.obtenerSubDimencion("3", this.idSucursal.toString(), this._codProyecto).toPromise();
    if(datadim3.estado == "True"){
      this.listaDim3 = datadim3.listaTablaGeneral;
    }

    //Obtener Subdimencion4
    const datadim4: any = this.maestroService.obtenerSubDimencion("4", this.idSucursal.toString(), this._codProyecto).toPromise();
    if(datadim4.estado == "True"){
      this.listaDim4 = datadim4.listaTablaGeneral;
    }
  }

  cerrar(){
    try {
      window.close();      
      this.router.navigateByUrl('doc-listar', { replaceUrl: true })     
    } catch (error) {
      console.log("error", error);
    }
  }
  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
