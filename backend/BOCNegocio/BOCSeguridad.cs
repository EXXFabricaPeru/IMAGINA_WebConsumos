using DTOEntidades;
using DBCAccesoDatos;
using System;
using System.Collections.Generic;

namespace BOCNegocio
{
    public interface IBOCSeguridad
    {
        DTORespuestaEmpleado fn_BuscarUsuario(string pUser, string pPassword, csCompany xCompany);
        DTORespuestaEmpleado fn_Usuario_Buscar(string pCodigo, csCompany xCompany);
        DTORespuestaEmpleado fn_ListarUsuario(string pValor, csCompany xCompany);
        DTORespuestaEmpleado fn_GuardarEmpleado(DTOEmpleado empleado, csCompany xCompany);
        DTORespuestaEmpleado fn_ActualizarEmpleado(DTOEmpleado empleado, csCompany xCompany);
        DTORespuestaEmpleado fn_InactivarEmpleado(DTOEmpleado empleado, csCompany xCompany);
        string ActualizarToken(string token, string usuario, csCompany xCompany);
        string ActualizarPass(string token, string usuario, string password, csCompany xCompany);
        string GetEmail(string usuario, csCompany xCompany);
    }

    public class BOCSeguridad : IBOCSeguridad
    {
        public DTORespuestaEmpleado fn_BuscarUsuario(string pUsuario, string pPass, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_BuscarUsuario(pUsuario, pPass, xCompany);
        }
        
        public DTORespuestaEmpleado fn_Usuario_Buscar(string pCodigo, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_Usuario_Buscar(pCodigo, xCompany);
        }
        
        public DTORespuestaEmpleado fn_ListarUsuario(string pValor, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_ListarUsuario(pValor, xCompany);
        }
        
        public DTORespuestaEmpleado fn_GuardarEmpleado(DTOEmpleado empleado, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_GuardarEmpleado(empleado, xCompany);
        }
        
        public DTORespuestaEmpleado fn_ActualizarEmpleado(DTOEmpleado empleado, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_ActualizarEmpleado(empleado, xCompany);
        }
        
        public DTORespuestaEmpleado fn_InactivarEmpleado(DTOEmpleado empleado, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.fn_InactivarEmpleado(empleado, xCompany);
        }

        public string ActualizarToken(string token, string usuario, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.ActualizarToken(token, usuario, xCompany);
        }

        public string ActualizarPass(string token, string usuario, string password, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.ActualizarPass(token, usuario, password, xCompany);
        }

        public string GetEmail(string usuario, csCompany xCompany)
        {
            IDBCSeguridad dbcSeg = new DBCSeguridad();
            return dbcSeg.GetEmail(usuario, xCompany);
        }
    }
}
