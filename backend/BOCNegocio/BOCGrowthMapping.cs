using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using Etna.DBCAccesoDatos;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Etna.BOCNegocio
{
    public interface IBOCGrowthMapping
    {
        List<DTORespuesta> fn_Medicion_Registar(DTOGrowthMapping pDto);
        List<DTORespuestaGrowthMapping> fn_Medicion_Buscar(string pNro);
        List<DTORespuestaGrowthMapping> fn_MedicionBuscarRegistro_Buscar(string pCliente, string pTienda);
        //List<DTORespuesta> fn_GrowthMappingMarca_Registrar(DTOGrowthMappingMarca pDto);
        List<DTORespuestaGrowthMapping> fn_GrowthMapping_DiaActualxUsuario_Listar(string pUsuario);
    }

    public class BOCGrowthMapping : IBOCGrowthMapping
    {
        public List<DTORespuesta> fn_Medicion_Registar(DTOGrowthMapping pDto)
        {
            IDBCGrowthMapping dbc = new DBCGrowthMapping();
            List<DTORespuesta> oLista = new List<DTORespuesta>();
            try
            {
                //BOCErrorControl.RegistraError(pDto.pr_Observacion);

                oLista = dbc.fn_Medicion_Registar(pDto);
            }
            catch (Exception ex)
            {
                BOCErrorControl.RegistraError(ex.Message);
            }
            return oLista;
        }

        public List<DTORespuestaGrowthMapping> fn_Medicion_Buscar(string pNro)
        {
            IDBCGrowthMapping dbc = new DBCGrowthMapping();
            return dbc.fn_Medicion_Buscar(pNro);
        }

        public List<DTORespuestaGrowthMapping> fn_MedicionBuscarRegistro_Buscar(string pCliente, string pTienda)
        {
            IDBCGrowthMapping dbc = new DBCGrowthMapping();
            return dbc.fn_MedicionBuscarRegistro_Buscar(pCliente, pTienda);
        }

        /*public List<DTORespuesta> fn_GrowthMappingMarca_Registrar(DTOGrowthMappingMarca pDto)
        {
            IDBCGrowthMapping dbc = new DBCGrowthMapping();
            DTORespuesta oDto = new DTORespuesta();
            List<DTORespuesta> oLista = new List<DTORespuesta>();

            string xRespuesta = "";

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DTOGrowthMappingMarca));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, pDto);

                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                data += "-->" + _xResult;
                BOCErrorControl.RegistrarJson(data);


                xRespuesta = dbc.fn_GrowthMappingMarca_Registrar(pDto);
                oDto.Mensaje = xRespuesta;
                oDto.Estado = "True";
            }
            catch (Exception ex)
            {
                oDto.Estado = "False";
                oDto.Mensaje = ex.Message;
            }

            oLista.Add(oDto);

            return oLista;
        }*/

        public List<DTORespuestaGrowthMapping> fn_GrowthMapping_DiaActualxUsuario_Listar(string pUsuario)
        {
            IDBCGrowthMapping dbc = new DBCGrowthMapping();
            return dbc.fn_GrowthMapping_DiaActualxUsuario_Listar(pUsuario);
        }
    }
}
