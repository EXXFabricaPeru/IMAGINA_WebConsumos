import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import ColumnHeader from 'src/app/models/columnHeader';
import { Producto } from 'src/app/models/producto';
import { TablaGeneral } from 'src/app/models/tablageneral';
import { TransferenciaCab } from 'src/app/models/transferenciaCab';
import { TransferenciaDet } from 'src/app/models/transferenciaDet';
import { MaestroService } from 'src/app/services/maestro.service';
import { TransferenciaService } from 'src/app/services/transferencia.service';

@Component({
  selector: 'app-transferencia-mostrar',
  templateUrl: './transferencia-mostrar.component.html',
  styleUrls: ['./transferencia-mostrar.component.css']
})
export class TransferenciaMostrarComponent implements OnInit {

//#region "Variables"
flagLoad :boolean = false;
listaDocumento: TransferenciaDet[] = [];
listaBoniSug: any[] = [];
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
valorProducto = "";
valorCliente = "";
dimencion1: string = "";
_xDimencion1: string = "";
_dimencion1: string = "";

dimencion2: string = "";
_xDimencion2: string = "";
_dimencion2: string = "";

dimencion3: string = "";
_xDimencion3: string = "";
_dimencion3: string = "";

dimencion4: string = "";
_xDimencion4: string = "";
_dimencion4: string = "";

xdimencion1: boolean = false;
xproyecto: boolean = false;
xtipooperacion: boolean = false;
xmoneda: boolean = false;
xcondicion: boolean = false;
xxtipooperacion: boolean = false;
xxmoneda: boolean = false;
xxcondicion: boolean = false;
ximpuesto: boolean = false;
xximpuesto: boolean = false;

estado: string = "";
idSucursal: string = "";
nroDoc: number = 0;
listaPaginas: number[] = [];
pagina: number = 0;
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

fecNecesaria: string = new Date().toISOString().slice(0, 10);//toLocaleDateString("en-US", { year: 'numeric', month: '2-digit', day: '2-digit' });
fecDocumento: string = new Date().toISOString().slice(0, 10);
fecContabilizacion: string = new Date().toISOString().slice(0, 10);

idGridDoc: string = "gridDoc";
idGridPro: string = "gridPro";

litaMotivo: TablaGeneral[] = [];
anularDialog: boolean = false;

codProy: string = "";
codDim1: string = "";
codDim2: string = "";
//#endregion "Variables"


  constructor(private _route: ActivatedRoute,
    private router: Router,
    private maestroService: MaestroService,
    private transferService: TransferenciaService) { }

  async ngOnInit(): Promise<void> {
    this._titulo = "SOLICITUD TRANSFERENCIA";
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
      // {
      //   label: "UndMed",
      //   key: "",
      //   subKey: "",
      //   customClass: "listaUnd",
      //   type: "select",
      //   value: "",
      //   visible: true
      // },
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
        label: "Cant. Aten.",
        key: "cantidadAten",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      {
        label: "Cant. Pend.",
        key: "cantidadOpen",
        subKey: "",
        customClass: "derecha",
        type: "",
        value: "",
        visible: true
      },
      // {
      //   label: "Proyecto",
      //   key: "proyecto",
      //   subKey: "",
      //   customClass: "Proyecto",
      //   type: "",
      //   value: "",
      //   visible: true
      // },
      // {
      //   label: this.dimencion1,
      //   key: "dimension1",
      //   subKey: "",
      //   customClass: '',
      //   type: "",
      //   value: "",
      //   visible: true
      // },
      // {
      //   label: this.dimencion2,
      //   key: "dimension2",
      //   subKey: "",
      //   customClass: '',
      //   type: "",
      //   value: "",
      //   visible: true
      // },
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
        customClass: '',
        type: "",
        value: "",
        visible: true
      },
      {
        label: this.dimencion4,
        key: "dimension4",
        subKey: "",
        customClass: '',
        type: "",
        value: "",
        visible: true
      },
      {
        label: 'Nro. Transf',
        key: "nroTransferencia",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
      {
        label: 'Estado',
        key: "estado",
        subKey: "",
        customClass: "",
        type: "",
        value: "",
        visible: true
      },
    ];

    const dataSucursal: any = await this.maestroService.obtenerSucursales().toPromise();
    // console.log("sucursal", dataSucursal);
    if(dataSucursal.estado == "True"){
      this.listaSucursales = dataSucursal.listaTablaGeneral;
    }

    const dataMotivo: any = await this.maestroService.obtenerMotivoAnulacion().toPromise();
    if(dataMotivo.estado == "True"){
      this.litaMotivo = dataMotivo.listaTablaGeneral;
    }

    this.buscarTransferencia();
  }

  async buscarTransferencia(){
    const _compania = document.getElementById("input-compania") as HTMLSelectElement
    const _tipooper = document.getElementById("input-tip-ope") as HTMLSelectElement
    const _Serie = document.getElementById("input-serie") as HTMLSelectElement
    const _fecDocu = document.getElementById("input-fecha-docu") as HTMLInputElement
    const _JefeAlm = document.getElementById("input-jefe") as HTMLSelectElement
    const _fecNece = document.getElementById("input-fecha-necesaria") as HTMLInputElement
    const _almOri = document.getElementById("input-alm-ori") as HTMLSelectElement
    const _almDes = document.getElementById("input-alm-des") as HTMLSelectElement
    const _cmbProy = document.getElementById("cmbProyecto") as HTMLSelectElement
    const _cmbEtapa = document.getElementById("cmbEtapa") as HTMLSelectElement
    const _cmbSubEt = document.getElementById("cmbSubEtapa") as HTMLSelectElement
    const _trabaj = document.getElementById("input-trabajador") as HTMLInputElement
    const _coment = document.getElementById("input-comment") as HTMLInputElement

    try {
      const xNroDoc: string = this._route.snapshot.paramMap.get("nro") || "0";
      this.nroDoc = Number(xNroDoc);
      const data: any = await this.transferService.obtenerTransferencia(xNroDoc).toPromise();
      console.log("rpta transferencia", data)
      if (data.estado){
        const transferencia = data.listaTransferencia[0];
        console.log("transferencia--", transferencia)
        _compania.value = transferencia.idSucursal.toString();
        _Serie.value = transferencia.series;
        _coment.value = transferencia.comentario;
        _JefeAlm.value = transferencia.jefeAlm;
        _fecDocu.value = transferencia.docDate.toString().substring(0, 10);
        _fecNece.value = transferencia.docDueDate.toString().substring(0, 10);
        this.listaDocumento = transferencia.listaDetalle; 
        this.idSucursal = transferencia.idSucursal.toString();
         //Tipo Operacion
        const dataTipoOper: any = await this.maestroService.obtenerTioOperacion().toPromise();
        if(dataTipoOper.estado == "True"){
          this.listaTpoOper = dataTipoOper.listaTablaGeneral;
          _tipooper.value = transferencia.tipoOperacion;
          this.tipoOperacion = transferencia.tipoOperacion;
        }

        const dataAlmVen: any = await this.maestroService.obtenerAlmacenVenta(transferencia.idSucursal.toString()).toPromise();
        if(dataAlmVen.estado == "True"){
          this.listaAlmacenDestino = dataAlmVen.listaTablaGeneral;
          this.listaAlmacenOrigen = dataAlmVen.listaTablaGeneral;          
        }
        
        const dataSerie: any = await this.maestroService.obtenerSeries("4", transferencia.idSucursal.toString()).toPromise();
        if(dataSerie.estado == "True"){
          this.listaSeries = dataSerie.listaTablaGeneral;
          _Serie.value = transferencia.series;
        }

        //Proyecto
        const dataProyecto: any = await this.maestroService.obtenerProyectos(transferencia.idSucursal.toString()).toPromise();
        if(dataProyecto.estado == "True"){
          this.listaProyecto = dataProyecto.listaTablaGeneral;
        }

        this.estado = transferencia.estado;

        await this.obtenerDimension();

        _cmbProy.value = this.listaDocumento[0].proyecto;
        this.codProy = this.listaDocumento[0].proyecto;
        _cmbEtapa.value = this.listaDocumento[0].dimension1;
        this.codDim1 = this.listaDocumento[0].dimension1;
        _cmbSubEt.value = this.listaDocumento[0].dimension2;
        this.codDim2 = this.listaDocumento[0].dimension2;

        _trabaj.value = transferencia.nombreTrabajador;
        
        for(let i = 0; i < this.listaDocumento.length; i++){
          
          const dim3 = this.listaDocumento[i].dimension3;
          const dim4 = this.listaDocumento[i].dimension4;
          // console.log("proy", this.listaProyecto);        
          
          const listaAux3 = this.listaDim3.filter(t => t.codigo == dim3);
          if(listaAux3.length > 0)
          this.listaDocumento[i].dimension3 = `${ listaAux3[0].descripcion }`

          const listaAux4 = this.listaDim4.filter(t => t.codigo == dim4);
          if(listaAux4.length > 0)
          this.listaDocumento[i].dimension4 = `${ listaAux4[0].descripcion }`

          _almDes.value = this.listaDocumento[i].codAlmacenDest;
          _almOri.value = this.listaDocumento[i].codAlmacenOri;
        }

        this._titulo += " N° " + transferencia.docNum; 
      }
    } catch (error) {
      console.log(error);      
    }
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

    this.flagLoad = true;

    let transferencia: TransferenciaCab = {
      docEntry: this.nroDoc,
      docNum: 0,
      codCliente: '',
      docDate: new Date(),
      taxDate: new Date(),
      docDueDate: new Date(),
      estado: '',
      comentario: _motAnulacion.value,
      codEmpleado: '',
      userReg: '',
      nomCliente: '',
      tipoOperacion: '',
      series: '',
      idSucursal: 0,
      sucursal: '',
      compania: '',
      codAlmacenOri: '',
      codAlmacenDest: '',
      nombreTrabajador: '',
      jefeAlm: '',
      listaDetalle: []
    }

    console.log("dato", transferencia);
    const data: any = await this.transferService.anularDocumento(transferencia).toPromise();

    this.flagLoad = false;
    
    console.log("rpta", data);
    this.errorDialog = true;
    this.msgError = data.mensaje;

    this.anularDialog = false;

    this._titulo = "SOLICITUD DE TRANSFERENCIA";

    this.buscarTransferencia();
  }

  async obtenerDimension(){
    //Obtener Subdimencion1
    this.maestroService.obtenerSubDimencion("1", this.idSucursal, this.listaDocumento[0].proyecto).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim1 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion2
    this.maestroService.obtenerSubDimencion("2", this.idSucursal, this.listaDocumento[0].proyecto).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim2 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion3
    this.maestroService.obtenerSubDimencion("3", this.idSucursal, this.listaDocumento[0].proyecto).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim3 = data.listaTablaGeneral;
      }
    });

    //Obtener Subdimencion4
    this.maestroService.obtenerSubDimencion("4", this.idSucursal, this.listaDocumento[0].proyecto).subscribe((data: any) => {
      // console.log(data);
      if(data.estado == "True"){
        this.listaDim4 = data.listaTablaGeneral;
      }
    });
  }

  cerrar(){
    try {
      window.close();      
      this.router.navigateByUrl('transferencia-listar', { replaceUrl: true })     
    } catch (error) {
      console.log("error", error);
    }
  }

  // @HostListener('window:beforeunload', ["$event"]) 
  // DoSometing() {
  //   sessionStorage.clear();
  // }
}
