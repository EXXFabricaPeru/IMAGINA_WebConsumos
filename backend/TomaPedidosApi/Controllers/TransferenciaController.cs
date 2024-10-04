using BOCNegocio;
using DBCAccesoDatos;
using DTOEntidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace TomaPedidosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciaController : Controller
    {
        [HttpGet]
        public DTORespuestaTransferencia fn_SolicitudTransferencia_Buscar(string docEntry)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCTransferencia boc = new BOCTransferencia();
            DTORespuestaTransferencia rpta = boc.fn_SolicitudTransferencia_Buscar(docEntry, conSap);
            return rpta;
        }
        [HttpGet("Listar")]
        public DTORespuestaTransferencia fn_SolicitudTransferencia_Listar(string fecDesde, string fecHasta, string estado, string user, string compania, string product)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCTransferencia boc = new BOCTransferencia();
            DTORespuestaTransferencia rpta = boc.fn_SolicitudTransferencia_Listar(fecDesde, fecHasta, estado, user, compania,product, conSap);
            return rpta;
        }
        [HttpPost]
        public DTORespuesta fn_SolicitudTransferencia_Registrar(DTOTransferenciaCab transferencia)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCTransferencia boc = new BOCTransferencia();
            DTORespuesta dto = boc.fn_SolicitudTransferencia_Registrar(transferencia, conSap);
            return dto;
        }

        [HttpPut]
        public DTORespuesta fn_SolicitudTransferencia_Anular(DTOTransferenciaCab transferencia)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCTransferencia boc = new BOCTransferencia();
            DTORespuesta dto = boc.fn_SolicitudTransferencia_Anular(transferencia, conSap);
            return dto;
        }
    }
}
