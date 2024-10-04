using BOCNegocio;
using DTOEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TomaPedidosApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MaestroController : Controller
    {
        [HttpGet]
        public DTORespuestaTablaGeneral Get(string id, string valor, string valor2, string valor3,string valor4)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCTablaGeneral boc = new BOCTablaGeneral();
            DTORespuestaTablaGeneral dto;
            switch(id)
            {
                case "1":
                    dto = boc.CondicionPago_Listar(valor, conSap);
                    break;
                case "2":
                    dto = boc.fn_AlmacenCliente_Listar(valor, conSap);
                    break;
                case "3":
                    dto = boc.fn_AlmacenVenta_Listar(valor, conSap);
                    break;
                case "4":
                    dto = boc.fn_Moneda_Listar(conSap);
                    break;
                case "5":
                    dto = boc.fn_TipoImpuesto_Listar(conSap);
                    break;
                case "6":
                    dto = boc.fn_Dimencion_Listar(conSap);
                    break;
                case "7":
                    dto = boc.fn_SubDimencion_Listar(valor, valor2, valor3, conSap);
                    break;
                case "8":
                    dto = boc.fn_TipoOPeracion_Listar(conSap);
                    break;
                case "9":
                    dto = boc.fn_Proyecto_Listar(valor, conSap);
                    break;
                case "10":
                    dto = boc.fn_SeriesDocumento_Listar(valor, valor2, conSap);
                    break;
                case "11":
                    dto = boc.fn_UnidadMedida_Listar(valor, valor2, valor3, conSap);
                    break;
                case "12":
                    dto = boc.fn_Departamento_Listar(conSap);
                    break;
                case "13":
                    dto = boc.fn_Provincia_Listar(valor, conSap);
                    break;
                case "14":
                    dto = boc.fn_Distrito_Listar(valor, conSap);
                    break;
                case "15":
                    dto = boc.fn_GrupoCliente_Listar(conSap);
                    break;
                case "16":
                    dto = boc.fn_Sucursales_Listar(conSap);
                    break;
                case "17":
                    dto = boc.fn_ListaPrecio_Listar(conSap);
                    break;
                case "18":
                    dto = boc.fn_Vendedores_Listar(valor, conSap);
                    break;
                case "19":
                    dto = boc.fn_GiroNegocio_Listar(conSap);
                    break;
                case "20":
                    dto = boc.fn_Zona_Listar(conSap);
                    break;
                case "21":
                    dto = boc.fn_MedioEnvio_Listar(conSap);
                    break;
                case "22":
                    dto = boc.fn_PartidaPresupuestal_Listar(valor, valor2, valor3,valor4, conSap);
                    break;
                case "23":
                    dto = boc.fn_MotivoAnulacion_Listar(conSap);
                    break;
                case "24":
                    dto = boc.fn_AlmacenEtapa_Listar(valor, valor2, conSap);
                    break;
                case "25":
                    dto = boc.fn_JefeAlmacen_Listar(conSap);
                    break;
                default:
                    dto = new DTORespuestaTablaGeneral();
                    break;
            }
            return dto;
        }
    }
}
