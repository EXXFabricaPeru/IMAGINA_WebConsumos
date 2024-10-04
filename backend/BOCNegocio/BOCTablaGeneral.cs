using DBCAccesoDatos;
using DTOEntidades;
using System.Collections.Generic;

namespace BOCNegocio
{
    public interface IBOCTablaGeneral
    {
        DTORespuestaTablaGeneral fn_AlmacenCliente_Listar(string pCodigo, csCompany xCompany);
        DTORespuestaTablaGeneral fn_AlmacenEtapa_Listar(string pCodigo, string etapa, csCompany xCompany);
        DTORespuestaTablaGeneral fn_AlmacenVenta_Listar(string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral CondicionPago_Listar(string pCodigo, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Moneda_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_TipoImpuesto_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Dimencion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_SubDimencion_Listar(string pCodigo, string pSucursal, string pProyecto, csCompany xCompany);
        DTORespuestaTablaGeneral fn_TipoOPeracion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Proyecto_Listar(string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_SeriesDocumento_Listar(string tipo, string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_UnidadMedida_Listar(string codArticulo, string moneda, string listaPrecio, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Departamento_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Provincia_Listar(string codDepartamento, csCompany xCompany);
        DTORespuestaTablaGeneral fn_Distrito_Listar(string codProvincia, csCompany xCompany);
        DTORespuestaTablaGeneral fn_GrupoCliente_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Sucursales_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_ListaPrecio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Vendedores_Listar(string xCodVendedor, csCompany xCompany);
        DTORespuestaTablaGeneral fn_GiroNegocio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_Zona_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_MedioEnvio_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_PartidaPresupuestal_Listar(string proyecto, string etapa, string idEmpleado, string sucursal, csCompany xCompany);
        DTORespuestaTablaGeneral fn_MotivoAnulacion_Listar(csCompany xCompany);
        DTORespuestaTablaGeneral fn_JefeAlmacen_Listar(csCompany xCompany);
    }

    public class BOCTablaGeneral : IBOCTablaGeneral
    {
        public DTORespuestaTablaGeneral fn_AlmacenCliente_Listar(string pCodigo, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_AlmacenCliente_Listar(pCodigo, xCompany);
        }

        public DTORespuestaTablaGeneral fn_AlmacenEtapa_Listar(string pCodigo, string etapa, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_AlmacenEtapa_Listar(pCodigo, etapa, xCompany);
        }

        public DTORespuestaTablaGeneral fn_AlmacenVenta_Listar(string sucursal, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_AlmacenVenta_Listar(sucursal, xCompany);
        }

        public DTORespuestaTablaGeneral CondicionPago_Listar(string pCodigo, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.CondicionPago_Listar(pCodigo, xCompany);
        }

        public DTORespuestaTablaGeneral fn_Moneda_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Moneda_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_TipoImpuesto_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_TipoImpuesto_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Dimencion_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Dimencion_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_SubDimencion_Listar(string pCodigo, string pSucursal, string pProyecto, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_SubDimencion_Listar(pCodigo, pSucursal, pProyecto, xCompany);
        }

        public DTORespuestaTablaGeneral fn_TipoOPeracion_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_TipoOPeracion_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Proyecto_Listar(string sucursal, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Proyecto_Listar(sucursal, xCompany);
        }

        public DTORespuestaTablaGeneral fn_SeriesDocumento_Listar(string tipo, string sucursal, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_SeriesDocumento_Listar(tipo, sucursal, xCompany);
        }

        public DTORespuestaTablaGeneral fn_UnidadMedida_Listar(string codArticulo, string moneda, string listaPrecio, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_UnidadMedida_Listar(codArticulo, moneda, listaPrecio, xCompany);
        }

        public DTORespuestaTablaGeneral fn_Departamento_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Departamento_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Provincia_Listar(string codDepartamento, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Provincia_Listar(codDepartamento, xCompany);
        }

        public DTORespuestaTablaGeneral fn_Distrito_Listar(string codProvincia, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Distrito_Listar(codProvincia, xCompany);
        }

        public DTORespuestaTablaGeneral fn_GrupoCliente_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_GrupoCliente_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Sucursales_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Sucursales_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_ListaPrecio_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_ListaPrecio_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Vendedores_Listar(string xCodVendedor, csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Vendedores_Listar(xCodVendedor, xCompany);
        }

        public DTORespuestaTablaGeneral fn_GiroNegocio_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_GiroNegocio_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_Zona_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_Zona_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_MedioEnvio_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbcTabGen = new DBCTablaGeneral();
            return dbcTabGen.fn_MedioEnvio_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_PartidaPresupuestal_Listar(string proyecto, string etapa, string idEmpleado, string sucursal, csCompany xCompany)
        {
            IDBCTablaGeneral dbc = new DBCTablaGeneral();
            return dbc.fn_PartidaPresupuestal_Listar(proyecto, etapa, idEmpleado,sucursal, xCompany);
        }

        public DTORespuestaTablaGeneral fn_MotivoAnulacion_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbc = new DBCTablaGeneral();
            return dbc.fn_MotivoAnulacion_Listar(xCompany);
        }

        public DTORespuestaTablaGeneral fn_JefeAlmacen_Listar(csCompany xCompany)
        {
            IDBCTablaGeneral dbc = new DBCTablaGeneral();
            return dbc.fn_JefeAlmacen_Listar(xCompany);
        }
    }
}
