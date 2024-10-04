using BOCNegocio;
using DTOEntidades;
using Microsoft.AspNetCore.Mvc;

namespace TomaPedidosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        //private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        //public UsuarioController(IJwtAuthenticationManager jwtAuthenticationManager)
        //{
        //    this.jwtAuthenticationManager = jwtAuthenticationManager;
        //}

        [HttpPost("login")]
        public DTORespuestaEmpleado PostLogin(DTOCredencial credencial)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();

            DTORespuestaEmpleado oUsuario = boc.fn_BuscarUsuario(credencial.Username, credencial.Password, conSap);
            
            //if(oUsuario.Estado == "True")
            //{
            //    oUsuario.Token = jwtAuthenticationManager.GetToken(credencial.Username);
            //}

            return oUsuario;
        }
        
        [HttpGet("listar")]
        public DTORespuestaEmpleado GetListar(string valor)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();
            return boc.fn_ListarUsuario(valor, conSap);
        }
        
        [HttpGet("obtener")]
        public DTORespuestaEmpleado GetUsuario(string codigo)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();
            return boc.fn_Usuario_Buscar(codigo, conSap);
        }

        [HttpPost]
        public DTORespuestaEmpleado Post(DTOEmpleado empleado)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();
            return boc.fn_GuardarEmpleado(empleado, conSap);
        }

        [HttpPut]
        public DTORespuestaEmpleado Put(DTOEmpleado empleado)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();
            return boc.fn_ActualizarEmpleado(empleado, conSap);
        }

        [HttpPut("inactivar")]
        public DTORespuestaEmpleado PutInactivar(DTOEmpleado empleado)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();
            return boc.fn_InactivarEmpleado(empleado, conSap);
        }

        [HttpPost("PassLost")]
        public DTORespuesta RecoveryPass(DTOCredencial credencial)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();

            string correo = boc.GetEmail(credencial.Username, conSap);

            DTORespuesta dto = new DTORespuesta();

            if (correo.Contains('@'))
            {
                string xMensaje = boc.ActualizarToken(credencial.Password, credencial.Username, conSap);
                if(xMensaje== "")
                {
                    xMensaje = new Cartero().RecuperacionPass(correo, credencial.Username, credencial.Password);
                    if (xMensaje == "") 
                    {
                        dto.Mensaje = $"Se envió con éxito al correo {correo}";
                        dto.Estado = true;
                    }
                }                
            }
            else
            {
                dto.Mensaje = "El usuario no existe o no cuenta con correo";
                dto.Estado = false;
            }
            return dto;
        }


        [HttpPost("PassChange")]
        public DTORespuesta ChangePass(DTOCredencial credencial)
        {
            csCompany conSap = new Constantes().GetConexion();
            IBOCSeguridad boc = new BOCSeguridad();

            DTORespuesta dto = new DTORespuesta();

            string xMensaje = boc.ActualizarPass(credencial.Token, credencial.Username, credencial.Password, conSap);
            if (xMensaje == "")
            {
                dto.Mensaje = $"Se reestableció con éxito su contraseña";
                dto.Estado = true;
            }
            else
            {
                dto.Mensaje = xMensaje;
                dto.Estado = true;
            }

            return dto;
        }
    }
}
