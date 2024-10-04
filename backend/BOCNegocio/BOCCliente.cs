using DBCAccesoDatos;
using DTOEntidades;
using System;
using System.Collections.Generic;

namespace BOCNegocio
{
    public interface IBOCCliente
    {
        List<DTORespuestaCliente> fn_ClienteVendedor_Listar(string pValor, string pVendedor, string flag, csCompany xCompany);
        DTORespuestaCliente fn_Cliente_Buscar(string codCliente, csCompany xCompany);
        DTORespuesta fn_Cliente_Registrar(DTOCliente cliente, csCompany xCompany);
        DTORespuesta fn_Cliente_Editar(DTOCliente cliente, csCompany xCompany);
        DTORespuestaEstadoCuenta fn_EstadoCuenta_Listar(string codCliente, csCompany xCompany);
        DTORespuestaCliente fn_Transportista_Buscar(csCompany xCompany);
        DTORespuesta fn_ClienteSucursal(string idSucursal, csCompany xCompany);
    }

    public class BOCCliente : IBOCCliente
    {
        string entraaqui = "entraaaaaaa";
        public List<DTORespuestaCliente> fn_ClienteVendedor_Listar(string pValor, string pVendedor, string flag, csCompany xCompany)
        {
            List<DTORespuestaCliente> oLista = new List<DTORespuestaCliente>();
            IDBCCliente oDBCClienteSAP = new DBCCliente();

            try
            {
                oLista = oDBCClienteSAP.fn_ClienteVendedor_Listar(pValor, pVendedor, flag, xCompany);
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
            return oLista;
        }

        public DTORespuestaCliente fn_Cliente_Buscar(string codCliente, csCompany xCompany)
        {
            IDBCCliente oDBCClienteSAP = new DBCCliente();
            return oDBCClienteSAP.fn_Cliente_Buscar(codCliente, xCompany);
        }

        public DTORespuestaCliente fn_Transportista_Buscar(csCompany xCompany)
        {
            IDBCCliente oDBCClienteSAP = new DBCCliente();
            return oDBCClienteSAP.fn_Transportista_Buscar(xCompany);
        }

        public DTORespuestaEstadoCuenta fn_EstadoCuenta_Listar(string codCliente, csCompany xCompany)
        {
            IDBCCliente dbc = new DBCCliente();
            return dbc.fn_EstadoCuenta_Listar(codCliente, xCompany);
        }

        public DTORespuesta fn_Cliente_Editar(DTOCliente cliente, csCompany xCompany)
        {
            IDBCCliente oDBCClienteSAP = new DBCCliente();
            DTORespuesta dto = new DTORespuesta();

            string xResul = oDBCClienteSAP.fn_Cliente_Editar(cliente, xCompany);
            string[] datos = xResul.Split('-');

            if (datos.Length > 1)
                dto.Estado = true;
            else
                dto.Estado = false;

            dto.Mensaje = xResul;

            return dto;
        }

        public DTORespuesta fn_Cliente_Registrar(DTOCliente cliente, csCompany xCompany)
        {
            IDBCCliente oDBCClienteSAP = new DBCCliente();
            DTORespuesta dto = new DTORespuesta();

            string xResul = oDBCClienteSAP.fn_Cliente_Registrar(cliente, xCompany);
            string[] datos = xResul.Split('-');

            if (datos[0] == "1")
                dto.Estado = true;
            else
                dto.Estado = false;

            dto.Mensaje = datos[1];

            return dto;
        }

        public DTORespuesta fn_ClienteSucursal(string idSucursal, csCompany xCompany)
        {
            IDBCCliente oDBCClienteSAP = new DBCCliente();
            return oDBCClienteSAP.fn_ClienteSucursal(idSucursal, xCompany);
        }
    }
}
