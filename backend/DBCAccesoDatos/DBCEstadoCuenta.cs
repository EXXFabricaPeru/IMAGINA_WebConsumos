using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using System.Data;
using System.Data.SqlClient;
using EtnaEncryptor;
using System.Configuration;

namespace Etna.DBCAccesoDatos
{
    public interface IDBCEstadoCuenta
    {
        List<DTORespuestaEstadoCuenta> fn_EstadoCuenta_Buscar(string CodVendedor, string CodCliente, string Tipo);
    }

    public class DBCEstadoCuenta : IDBCEstadoCuenta
    {
        SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["EtnaSap"].ConnectionString));

        public List<DTORespuestaEstadoCuenta> fn_EstadoCuenta_Buscar(string CodVendedor, string CodCliente, string Tipo)
        {
            DTOEstadoCuenta dtoEstadoCuenta = null;
            List<DTOEstadoCuenta> oListaEstCta = new List<DTOEstadoCuenta>();
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            DTORespuestaEstadoCuenta dto = new DTORespuestaEstadoCuenta();
            List<DTORespuestaEstadoCuenta> oLista = new List<DTORespuestaEstadoCuenta>();

            try
            {
                cm.CommandText = "[dbo].[PA_CRE_EstadoCuentaxCliente]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_Vendedor", SqlDbType.VarChar).Value = CodVendedor;
                cm.Parameters.Add("@P_Cliente", SqlDbType.VarChar).Value = CodCliente;
                cm.Parameters.Add("@P_Tipo", SqlDbType.VarChar).Value = Tipo;
                cm.Connection = cn;

                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    dtoEstadoCuenta = new DTOEstadoCuenta
                    {
                        CodCliente = dr["Codigo"].ToString(),
                        NomCliente = dr["Cliente"].ToString(),
                        NroDocumento = dr["NroDoc"].ToString(),
                        FecEmision = Convert.ToDateTime(dr["FechaEmision"]).ToShortDateString(),
                        FecVencimiento = Convert.ToDateTime(dr["FechaVencimiento"]).ToShortDateString(),
                        Original = Convert.ToDecimal(dr["MontoOrigen"]).ToString("N2"),
                        Saldo = Convert.ToDecimal(dr["Saldo"]).ToString("N2"),
                        Vendedor = dr["Vendedor"].ToString(),
                        LineaCredito = Convert.ToDecimal(dr["LineaCredito"]).ToString("N2"),
                        LineaDisponible = Convert.ToDecimal(dr["LineaDisponible"]).ToString("N2")
                    };
                    oListaEstCta.Add(dtoEstadoCuenta);
                }

                if (oListaEstCta.Count > 0)
                {
                    dto.Estado = "True";
                    dto.Mensaje = "Se recibio el estado de cuenta del cliente";
                    dto.ListaEstadoCuenta = oListaEstCta;

                    oLista.Add(dto);
                }
                else
                {
                    dto.Estado = "False";
                    dto.Mensaje = "No se ah encontrado ningun estado de cuenta con los filtros ingresados";
                    dto.ListaEstadoCuenta = null;

                    oLista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Estado = "False";
                dto.Mensaje = "Se ha producido un error: " + ex.Message;
                dto.ListaEstadoCuenta = null;

                oLista.Add(dto);
            }
            finally
            {
                dr.Close();
                cn.Close();
            }

            return oLista;
        }
    }
}
