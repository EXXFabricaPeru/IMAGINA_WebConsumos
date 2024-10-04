using DTOEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBCAccesoDatos
{
    public interface IDBCTransportista
    {
        List<DTORespuestaTransportista> fn_Transportista_Listar(string pValor);
    }
    
    public class DBCTransportista : IDBCTransportista
    {
        //SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["connection"].ConnectionString));
        SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=SBO_NewTrade_PE;Persist Security Info=True;User ID=sa;Password=b1admin");


        public List<DTORespuestaTransportista> fn_Transportista_Listar(string pValor)
        {
            DTOTransportista oTransportista = null;
            List<DTOTransportista> oListaTrans = new List<DTOTransportista>();

            List<DTORespuestaTransportista> oLista = new List<DTORespuestaTransportista>();
            DTORespuestaTransportista dto = new DTORespuestaTransportista();

            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            cn.Open();
            cm.Connection = cn;

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_MAE_Transportista_Listar]";
                cm.Parameters.Add("@P_Valor", SqlDbType.VarChar).Value = pValor;
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    oTransportista = new DTOTransportista();
                    oTransportista.pr_Ruc = dr["Ruc"].ToString();
                    oTransportista.pr_RazonSocial = dr["Nombre"].ToString();
                    oTransportista.pr_Direccion = dr["Direccion"].ToString();
                    oTransportista.pr_CodTransportista = dr["Codigo"].ToString();
                    oListaTrans.Add(oTransportista);
                }
                dto.Estado = "true";
                dto.Mensaje = "Se obtuvo la lista de transportistas";
                dto.ListaTransportista = oListaTrans;

                oLista.Add(dto);
            }
            catch (Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = "Se produjo un error:" + ex.Message;
                dto.ListaTransportista = null;
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
