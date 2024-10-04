using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Etna.DTOEntidades;
using EtnaEncryptor;

namespace Etna.DBCAccesoDatos
{
    public interface IDBCGrowthMapping
    {
        List<DTORespuesta> fn_Medicion_Registar(DTOGrowthMapping pDto);
        List<DTORespuestaGrowthMapping> fn_Medicion_Buscar(string pNro);
        List<DTORespuestaGrowthMapping> fn_MedicionBuscarRegistro_Buscar(string pCliente, string pTienda);
        //string fn_GrowthMappingMarca_Registrar(DTOGrowthMappingMarca pDto);
        List<DTORespuestaGrowthMapping> fn_GrowthMapping_DiaActualxUsuario_Listar(string pUsuario);
    }

    public class DBCGrowthMapping : IDBCGrowthMapping
    {
        SqlConnection cn = new SqlConnection(Rijndael.Desencriptar(ConfigurationManager.ConnectionStrings["EtnaSap"].ConnectionString));

        public List<DTORespuesta> fn_Medicion_Registar(DTOGrowthMapping pDto)
        {
            DTORespuesta xResultado = new DTORespuesta();
            List<DTORespuesta> xLista = new List<DTORespuesta>();
            SqlCommand cm = new SqlCommand();
            SqlTransaction oTransaction;
            cn.Open();

            oTransaction = cn.BeginTransaction();
            cm.Connection = cn;
            cm.Transaction = oTransaction;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_Registrar]";
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@P_CodCliente", SqlDbType.VarChar).Value = pDto.pr_CodCliente;
                cm.Parameters.Add("@P_CodDepartamento", SqlDbType.VarChar).Value = pDto.pr_CodDepartamento;
                cm.Parameters.Add("@P_CodProvincia", SqlDbType.VarChar).Value = pDto.pr_CodProvincia;
                cm.Parameters.Add("@P_CodDistrito", SqlDbType.VarChar).Value = pDto.pr_CodDistrito;
                cm.Parameters.Add("@P_Tienda", SqlDbType.VarChar).Value = pDto.pr_Tienda;
                cm.Parameters.Add("@P_CodArticulo", SqlDbType.Int).Value = pDto.pr_CodArticulo;
                cm.Parameters.Add("@P_FrentesRack_PRO", SqlDbType.Int).Value = pDto.pr_FrentesRack_PRO;
                cm.Parameters.Add("@P_FrentesRack_AD", SqlDbType.Int).Value = pDto.pr_FrentesRack_AD;
                cm.Parameters.Add("@P_FrentesRack_PL", SqlDbType.Int).Value = pDto.pr_FrentesRack_PL;
                cm.Parameters.Add("@P_FrentesRack_SS", SqlDbType.Int).Value = pDto.pr_FrentesRack_SS;
                cm.Parameters.Add("@P_FrentesRack_CP", SqlDbType.Int).Value = pDto.pr_FrentesRack_CP;
                cm.Parameters.Add("@P_FrentesRackHorizontal", SqlDbType.Int).Value = pDto.pr_FrentesRackHorizontal;
                //cm.Parameters.Add("@P_AccionesPDV", SqlDbType.Bit).Value = pDto.pr_AccionesPDV;
                //cm.Parameters.Add("@P_MarcajeVinetasCarteles", SqlDbType.Bit).Value = pDto.pr_MarcajeVinetasCarteles;
                cm.Parameters.Add("@P_UserReg", SqlDbType.VarChar).Value = pDto.pr_UserReg;
                cm.Parameters.Add("@P_Longitud", SqlDbType.VarChar).Value = pDto.pr_Longitud;
                cm.Parameters.Add("@P_Latitud", SqlDbType.VarChar).Value = pDto.pr_Latitud;
                cm.Parameters.Add("@P_Observacion", SqlDbType.VarChar).Value = pDto.pr_Observacion;
                cm.Parameters.Add("@P_Tipo", SqlDbType.Int).Value = pDto.pr_Tipo;
                cm.Parameters.Add("@P_IdMedicion", SqlDbType.Int).Value = pDto.pr_IdMedicion;
                cm.Parameters.Add("@P_MED_nSKU_PRO", SqlDbType.Int).Value = pDto.pr_MED_nSKU_PRO;
                cm.Parameters.Add("@P_MED_nSKU_AD", SqlDbType.Int).Value = pDto.pr_MED_nSKU_AD;
                cm.Parameters.Add("@P_MED_nSKU_PL", SqlDbType.Int).Value = pDto.pr_MED_nSKU_PL;
                cm.Parameters.Add("@P_MED_nSKU_SS", SqlDbType.Int).Value = pDto.pr_MED_nSKU_SS;
                cm.Parameters.Add("@P_MED_nSKU_CP", SqlDbType.Int).Value = pDto.pr_MED_nSKU_CP;
                cm.Parameters.Add("@P_MED_nStock_PRO", SqlDbType.Int).Value = pDto.pr_MED_nStock_PRO;
                cm.Parameters.Add("@P_MED_nStock_AD", SqlDbType.Int).Value = pDto.pr_MED_nStock_AD;
                cm.Parameters.Add("@P_MED_nStock_PL", SqlDbType.Int).Value = pDto.pr_MED_nStock_PL;
                cm.Parameters.Add("@P_MED_nStock_SS", SqlDbType.Int).Value = pDto.pr_MED_nStock_SS;
                cm.Parameters.Add("@P_MED_nStock_CP", SqlDbType.Int).Value = pDto.pr_MED_nStock_CP;
                cm.Parameters.Add("@P_MED_bPorc_Dcto", SqlDbType.Bit).Value = pDto.pr_MED_bPorc_Dcto;
                cm.Parameters.Add("@P_MED_bPack", SqlDbType.Bit).Value = pDto.pr_MED_bPack;
                cm.Parameters.Add("@P_MED_bMerch", SqlDbType.Bit).Value = pDto.pr_MED_bMerch;
                cm.Parameters.Add("@P_MED_bOtros", SqlDbType.Bit).Value = pDto.pr_MED_bOtros;
                cm.Parameters.Add("@P_MED_cDesc_Otros", SqlDbType.VarChar).Value = pDto.pr_MED_cDesc_Otros;
                cm.Parameters.Add("@P_MED_bCartel", SqlDbType.Bit).Value = pDto.pr_MED_bCartel;
                cm.Parameters.Add("@P_MED_bViñeta", SqlDbType.Bit).Value = pDto.pr_MED_bViñeta;
                cm.Parameters.Add("@P_MED_bSticker", SqlDbType.Bit).Value = pDto.pr_MED_bSticker;
                cm.Parameters.Add("@P_MED_bJalavista", SqlDbType.Bit).Value = pDto.pr_MED_bJalavista;
                cm.Parameters.Add("@P_MED_bBanner", SqlDbType.Bit).Value = pDto.pr_MED_bBanner;
                //cm.Parameters.Add("@P_MED_bPromocion", SqlDbType.Bit).Value = pDto.pr_MED_bPromocion;
                //cm.Parameters.Add("@P_MED_bExhibicion", SqlDbType.Bit).Value = pDto.pr_MED_bExhibicion;
                //cm.Parameters.Add("@P_MED_bDescuento", SqlDbType.Bit).Value = pDto.pr_MED_bDescuento;
                //cm.Parameters.Add("@P_MED_bActivacion", SqlDbType.Bit).Value = pDto.pr_MED_bActivacion;
                cm.Parameters.Add("@P_IdMedicion_out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                int xNewId = Convert.ToInt32(cm.Parameters["@P_IdMedicion_out"].Value);

                foreach (DTOGrowthMappingMarca item in pDto.pr_ToListaMarca)
                {
                    fn_GrowthMappingMarca_Registrar(item, oTransaction, cn, xNewId);
                }

                xResultado.Estado = "true";
                //xResultado.Mensaje = "Se ha registrado la medición N°" + pDto.pr_IdMedicion.ToString();
                xResultado.Mensaje = "Se ha registrado la medición - " + xNewId.ToString();

                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                oTransaction.Rollback();
                xResultado.Estado = "false";
                xResultado.Mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            xLista.Add(xResultado);
            return xLista;
        }

        public List<DTORespuestaGrowthMapping> fn_Medicion_Buscar(string pNro)
        {
            List<DTORespuestaGrowthMapping> oLista = new List<DTORespuestaGrowthMapping>();
            DTORespuestaGrowthMapping dto = new DTORespuestaGrowthMapping();
            DTOGrowthMapping xDto = new DTOGrowthMapping();
            List<DTOGrowthMapping> xLista = new List<DTOGrowthMapping>();

            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_Buscar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_IdMedicion", SqlDbType.Int).Value = pNro;
                cm.Connection = cn;

                cn.Open();
                dr = cm.ExecuteReader();

                int i = 0;

                while (dr.Read())
                {
                    xDto = new DTOGrowthMapping
                    {
                        //pr_AccionesPDV = Convert.ToBoolean(dr["MED_bAccionesPDV"]),
                        pr_CodArticulo = Convert.ToInt32(dr["MED_nCodArticulo"]),
                        pr_CodCliente = dr["MED_cCodCliente"].ToString(),
                        pr_CodDepartamento = dr["MED_cCodDepartamento"].ToString(),
                        pr_CodDistrito = dr["MED_cCodDistrito"].ToString(),
                        pr_CodProvincia = dr["MED_cCodProvincia"].ToString(),
                        pr_FecReg = Convert.ToDateTime(dr["MED_fFecReg"]),
                        pr_FrentesRack_PRO = Convert.ToInt32(dr["MED_nFrentesRack_PRO"]),
                        pr_FrentesRack_PL = Convert.ToInt32(dr["MED_nFrentesRack_PL"]),
                        pr_FrentesRack_AD = Convert.ToInt32(dr["MED_nFrentesRack_AD"]),
                        pr_FrentesRack_SS = Convert.ToInt32(dr["MED_nFrentesRack_SS"]),
                        pr_FrentesRack_CP = Convert.ToInt32(dr["MED_nFrentesRack_CP"]),
                        pr_FrentesRackHorizontal = Convert.ToInt32(dr["MED_nFrentesRackHorizontal"]),
                        pr_IdMedicion = Convert.ToInt32(dr["MED_nIdMedicion"]),
                        pr_Latitud = dr["MED_cLatitud"].ToString(),
                        pr_Longitud = dr["MED_cLongitud"].ToString(),
                        pr_Observacion = dr["MED_cObservacion"].ToString(),
                        //pr_MarcajeVinetasCarteles = Convert.ToBoolean(dr["MED_bMarcajeVinetasCarteles"]),
                        pr_Tienda = dr["MED_cTienda"].ToString(),
                        pr_MED_nSKU_PRO = Convert.ToInt32(dr["MED_nSKU_PRO"]),
                        pr_MED_nSKU_AD = Convert.ToInt32(dr["MED_nSKU_AD"]),
                        pr_MED_nSKU_PL = Convert.ToInt32(dr["MED_nSKU_PL"]),
                        pr_MED_nSKU_SS = Convert.ToInt32(dr["MED_nSKU_SS"]),
                        pr_MED_nSKU_CP = Convert.ToInt32(dr["MED_nSKU_CP"]),
                        pr_MED_nStock_PRO = Convert.ToInt32(dr["MED_nStock_PRO"]),
                        pr_MED_nStock_AD = Convert.ToInt32(dr["MED_nStock_AD"]),
                        pr_MED_nStock_PL = Convert.ToInt32(dr["MED_nStock_PL"]),
                        pr_MED_nStock_SS = Convert.ToInt32(dr["MED_nStock_SS"]),
                        pr_MED_nStock_CP = Convert.ToInt32(dr["MED_nStock_CP"]),
                        pr_MED_bPorc_Dcto = Convert.ToBoolean(dr["MED_bPorc_Dcto"]),
                        pr_MED_bPack = Convert.ToBoolean(dr["MED_bPack"]),
                        pr_MED_bMerch = Convert.ToBoolean(dr["MED_bMerch"]),
                        pr_MED_bOtros = Convert.ToBoolean(dr["MED_bOtros"]),
                        pr_MED_cDesc_Otros = dr["MED_cDesc_Otros"].ToString(),
                        pr_MED_bCartel = Convert.ToBoolean(dr["MED_bCartel"]),
                        pr_MED_bViñeta = Convert.ToBoolean(dr["MED_bViñeta"]),
                        pr_MED_bSticker = Convert.ToBoolean(dr["MED_bSticker"]),
                        pr_MED_bJalavista = Convert.ToBoolean(dr["MED_bJalavista"]),
                        //pr_MED_bPromocion = Convert.ToBoolean(dr["MED_bPromocion"]),
                        //pr_MED_bExhibicion = Convert.ToBoolean(dr["MED_bExhibicion"]),
                        //pr_MED_bDescuento = Convert.ToBoolean(dr["MED_bDescuento"]),
                        //pr_MED_bActivacion = Convert.ToBoolean(dr["MED_bActivacion"]),

                    };

                    xLista.Add(xDto);

                    i++;
                }

                if (xLista.Count > 0)
                {
                    dto.Estado = "true";
                    dto.Mensaje = "Se encontro la visita";
                    dto.ListaMedicion = xLista;
                    oLista.Add(dto);
                }
                else
                {
                    dto.Estado = "false";
                    dto.Mensaje = "No se encontro la visita";
                    dto.ListaMedicion = null;
                    oLista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = ex.Message;
                dto.ListaMedicion = null;
                oLista.Add(dto);

                dr.Close();
                cn.Close();
            }

            return oLista;
        }

        public List<DTORespuestaGrowthMapping> fn_MedicionBuscarRegistro_Buscar(string pCliente, string pTienda)
        {
            List<DTORespuestaGrowthMapping> oLista = new List<DTORespuestaGrowthMapping>();
            DTORespuestaGrowthMapping dto = new DTORespuestaGrowthMapping();
            DTOGrowthMapping xDto = new DTOGrowthMapping();
            List<DTOGrowthMapping> xLista = new List<DTOGrowthMapping>();

            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            try
            {
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_Validar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_CodCliente", SqlDbType.VarChar).Value = pCliente;
                cm.Parameters.Add("@P_Tienda", SqlDbType.VarChar).Value = pTienda;
                cm.Connection = cn;

                cn.Open();
                dr = cm.ExecuteReader();

                int i = 0;

                while (dr.Read())
                {
                    xDto = new DTOGrowthMapping
                    {
                        //pr_AccionesPDV = Convert.ToBoolean(dr["MED_bAccionesPDV"]),
                        pr_CodArticulo = Convert.ToInt32(dr["MED_nCodArticulo"]),
                        pr_CodCliente = dr["MED_cCodCliente"].ToString(),
                        pr_CodDepartamento = dr["MED_cCodDepartamento"].ToString(),
                        pr_CodDistrito = dr["MED_cCodDistrito"].ToString(),
                        pr_CodProvincia = dr["MED_cCodProvincia"].ToString(),
                        pr_FecReg = Convert.ToDateTime(dr["MED_fFecReg"]),
                        pr_FrentesRack_PRO = Convert.ToInt32(dr["MED_nFrentesRack_PRO"]),
                        pr_FrentesRack_PL = Convert.ToInt32(dr["MED_nFrentesRack_PL"]),
                        pr_FrentesRack_AD = Convert.ToInt32(dr["MED_nFrentesRack_AD"]),
                        pr_FrentesRack_SS = Convert.ToInt32(dr["MED_nFrentesRack_SS"]),
                        pr_FrentesRack_CP = Convert.ToInt32(dr["MED_nFrentesRack_CP"]),
                        pr_FrentesRackHorizontal = Convert.ToInt32(dr["MED_nFrentesRackHorizontal"]),
                        pr_IdMedicion = Convert.ToInt32(dr["MED_nIdMedicion"]),
                        pr_Latitud = dr["MED_cLatitud"].ToString(),
                        pr_Longitud = dr["MED_cLongitud"].ToString(),
                        pr_Observacion = dr["MED_cObservacion"].ToString(),
                        //pr_MarcajeVinetasCarteles = Convert.ToBoolean(dr["MED_bMarcajeVinetasCarteles"]),
                        pr_Tienda = dr["MED_cTienda"].ToString(),

                        pr_MED_nSKU_PRO = Convert.ToInt32(dr["MED_nSKU_PRO"]),
                        pr_MED_nSKU_AD = Convert.ToInt32(dr["MED_nSKU_AD"]),
                        pr_MED_nSKU_PL = Convert.ToInt32(dr["MED_nSKU_PL"]),
                        pr_MED_nSKU_SS = Convert.ToInt32(dr["MED_nSKU_SS"]),
                        pr_MED_nSKU_CP = Convert.ToInt32(dr["MED_nSKU_CP"]),
                        pr_MED_nStock_PRO = Convert.ToInt32(dr["MED_nStock_PRO"]),
                        pr_MED_nStock_AD = Convert.ToInt32(dr["MED_nStock_AD"]),
                        pr_MED_nStock_PL = Convert.ToInt32(dr["MED_nStock_PL"]),
                        pr_MED_nStock_SS = Convert.ToInt32(dr["MED_nStock_SS"]),
                        pr_MED_nStock_CP = Convert.ToInt32(dr["MED_nStock_CP"]),
                        pr_MED_bPorc_Dcto = Convert.ToBoolean(dr["MED_bPorc_Dcto"]),
                        pr_MED_bPack = Convert.ToBoolean(dr["MED_bPack"]),
                        pr_MED_bMerch = Convert.ToBoolean(dr["MED_bMerch"]),
                        pr_MED_bOtros = Convert.ToBoolean(dr["MED_bOtros"]),
                        pr_MED_cDesc_Otros = dr["MED_cDesc_Otros"].ToString(),
                        pr_MED_bCartel = Convert.ToBoolean(dr["MED_bCartel"]),
                        pr_MED_bViñeta = Convert.ToBoolean(dr["MED_bViñeta"]),
                        pr_MED_bSticker = Convert.ToBoolean(dr["MED_bSticker"]),
                        pr_MED_bJalavista = Convert.ToBoolean(dr["MED_bJalavista"]),
                        //pr_MED_bPromocion = Convert.ToBoolean(dr["MED_bPromocion"]),
                        //pr_MED_bExhibicion = Convert.ToBoolean(dr["MED_bExhibicion"]),
                        //pr_MED_bDescuento = Convert.ToBoolean(dr["MED_bDescuento"]),
                        //pr_MED_bActivacion = Convert.ToBoolean(dr["MED_bActivacion"]),
                        pr_MED_bBanner = Convert.ToBoolean(dr["MED_bBanner"]),
                    };

                    xLista.Add(xDto);

                    i++;
                }

                dr.Close();

                foreach (DTOGrowthMapping item in xLista)
                {
                    item.pr_ToListaMarca = new List<DTOGrowthMappingMarca>();
                    item.pr_ToListaMarca = fn_GrowMappingMarca_Listar(item.pr_IdMedicion, cn);
                }

                if (xLista.Count > 0)
                {
                    dto.Estado = "true";
                    dto.Mensaje = "Se encontro la visita";
                    dto.ListaMedicion = xLista;
                    oLista.Add(dto);
                }
                else
                {
                    dto.Estado = "false";
                    dto.Mensaje = "No se encontro la visita";
                    dto.ListaMedicion = null;
                    oLista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Estado = "false";
                dto.Mensaje = ex.Message;
                dto.ListaMedicion = null;
                oLista.Add(dto);

                cn.Close();
            }

            return oLista;
        }

        public string fn_GrowthMappingMarca_Registrar(DTOGrowthMappingMarca pDto, SqlTransaction pTrans, SqlConnection pCn, int idMedicion)
        {
            SqlCommand cm = new SqlCommand();
            string xRespuesta = "";
            cm.Transaction = pTrans;
            try
            {
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_Marca_Registrar]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@P_Tipo", SqlDbType.Int).Value = pDto.pr_Tipo;
                cm.Parameters.Add("@P_GMP_IdMedicion", SqlDbType.Int).Value = idMedicion;
                cm.Parameters.Add("@P_GMP_IdMarca", SqlDbType.Int).Value = pDto.pr_GMP_nIdMarca;
                cm.Parameters.Add("@P_GMP_bPromocion", SqlDbType.Bit).Value = pDto.pr_GMP_bPromocion;
                cm.Parameters.Add("@P_GMP_bExhibicion", SqlDbType.Bit).Value = pDto.pr_GMP_bExhibicion;
                cm.Parameters.Add("@P_GMP_bDescuento", SqlDbType.Bit).Value = pDto.pr_GMP_bDescuento;
                cm.Parameters.Add("@P_GMP_bActivacion", SqlDbType.Bit).Value = pDto.pr_GMP_bActivacion;
                cm.Connection = pCn;

                cm.ExecuteNonQuery();

                xRespuesta = "Se registro con éxito";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cn.Close();
            }
            return xRespuesta;
        }

        public List<DTORespuestaGrowthMapping> fn_GrowthMapping_DiaActualxUsuario_Listar(string pUsuario)
        {
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            List<DTORespuestaGrowthMapping> oLista = new List<DTORespuestaGrowthMapping>();
            DTORespuestaGrowthMapping dto = null;
            List<DTOGrowthMapping> xListaGrowMaping = new List<DTOGrowthMapping>();
            DTOGrowthMapping xGrowMapping = null;
            List<DTOGrowthMappingMarca> xListaGrowMappingMarca = null;

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_DiaActualxUsuario_Listar]";
                cm.Parameters.Add("@P_Usuario", SqlDbType.VarChar).Value = pUsuario;
                cm.Connection = cn;

                cn.Open();

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    xGrowMapping = new DTOGrowthMapping();
                    xGrowMapping.pr_IdMedicion = Convert.ToInt32(dr["ID MEDICION"]);
                    xGrowMapping.pr_CodCliente = dr["CODIGO CLIENTE"].ToString();
                    xGrowMapping.pr_NomCliente = dr["RAZON SOCIAL"].ToString();
                    xGrowMapping.pr_Tienda = dr["NOMBRE TIENDA"].ToString();
                    xGrowMapping.pr_Promotor = dr["Promotor"].ToString();
                    xGrowMapping.pr_Supervisor = dr["Supervisor"].ToString();
                    xGrowMapping.pr_Categoria = dr["CATEGORIA"].ToString();
                    xGrowMapping.pr_NroSkuCodificados = Convert.ToInt32(dr["NRO SKU CODIFICADOS"]);
                    xGrowMapping.pr_MED_nSKU_PRO = Convert.ToInt32(dr["NRO SKU PRO"]);
                    xGrowMapping.pr_MED_nSKU_AD = Convert.ToInt32(dr["NRO SKU AD"]);
                    xGrowMapping.pr_MED_nSKU_PL = Convert.ToInt32(dr["NRO SKU PL"]);
                    xGrowMapping.pr_MED_nSKU_SS = Convert.ToInt32(dr["NRO SKU SS"]);
                    xGrowMapping.pr_MED_nSKU_CP = Convert.ToInt32(dr["NRO SKU CP"]);
                    xGrowMapping.pr_Meta_SKU_Codi = Convert.ToInt32(dr["META SKU CODIFICADOS"]);
                    xGrowMapping.pr_FrentesRackHorizontal = Convert.ToInt32(dr["RACK HORIZONTAL"]);
                    xGrowMapping.pr_FrentesRack_PRO = Convert.ToInt32(dr["PRO"]);
                    xGrowMapping.pr_FrentesRack_AD = Convert.ToInt32(dr["AD"]);
                    xGrowMapping.pr_FrentesRack_PL = Convert.ToInt32(dr["PL"]);
                    xGrowMapping.pr_FrentesRack_SS = Convert.ToInt32(dr["SS"]);
                    xGrowMapping.pr_FrentesRack_CP = Convert.ToInt32(dr["CP"]);
                    xGrowMapping.pr_Meta_Horizontal = Convert.ToInt32(dr["META HORIZONTAL"]);
                    xGrowMapping.pr_FrentRack_Pro = Convert.ToInt32(dr["META PRO"]);
                    xGrowMapping.pr_FrentRack_AD = Convert.ToInt32(dr["META AD"]);
                    xGrowMapping.pr_FrentRack_PL = Convert.ToInt32(dr["META PL"]);
                    xGrowMapping.pr_FrentRack_SS = Convert.ToInt32(dr["META SS"]);
                    xGrowMapping.pr_FrentRack_CP = Convert.ToInt32(dr["META CP"]);
                    xGrowMapping.pr_MED_bPorc_Dcto = Convert.ToBoolean(dr["PORC DCTO"]);
                    xGrowMapping.pr_MED_bPack = Convert.ToBoolean(dr["PACK"]);
                    xGrowMapping.pr_MED_bMerch = Convert.ToBoolean(dr["MERCH"]);
                    xGrowMapping.pr_MED_bOtros = Convert.ToBoolean(dr["OTROS"]);
                    xGrowMapping.pr_MED_cDesc_Otros = dr["DESC. OTROS"].ToString();
                    xGrowMapping.pr_MED_bCartel = Convert.ToBoolean(dr["CARTEL"]);
                    xGrowMapping.pr_MED_bViñeta = Convert.ToBoolean(dr["VINETA"]);
                    xGrowMapping.pr_MED_bSticker = Convert.ToBoolean(dr["STICKER"]);
                    xGrowMapping.pr_MED_bJalavista = Convert.ToBoolean(dr["JALAVISTA"]);
                    xGrowMapping.pr_MED_bBanner = Convert.ToBoolean(dr["BANNER"]);
                    //xGrowMapping.pr_MED_bPromocion = Convert.ToBoolean(dr["PROMOCION"]);
                    //xGrowMapping.pr_MED_bExhibicion = Convert.ToBoolean(dr["EXHIBICION"]);
                    //xGrowMapping.pr_MED_bDescuento = Convert.ToBoolean(dr["DESCUENTO"]);
                    //xGrowMapping.pr_MED_bActivacion = Convert.ToBoolean(dr["ACTIVACION"]);
                    xGrowMapping.pr_Fecha = dr["FECHA"].ToString();
                    xGrowMapping.pr_UserReg = dr["USUARIO"].ToString();
                    xGrowMapping.pr_MED_nStock_PRO = Convert.ToInt32(dr["STOCK PRO"]);
                    xGrowMapping.pr_MED_nStock_AD = Convert.ToInt32(dr["STOCK AD"]);
                    xGrowMapping.pr_MED_nStock_PL = Convert.ToInt32(dr["STOCK PL"]);
                    xGrowMapping.pr_MED_nStock_SS = Convert.ToInt32(dr["STOCK SS"]);
                    xGrowMapping.pr_MED_nStock_CP = Convert.ToInt32(dr["STOCK CP"]);

                    xListaGrowMaping.Add(xGrowMapping);
                }
                dr.Close();

                foreach (DTOGrowthMapping item in xListaGrowMaping)
                {
                    xListaGrowMappingMarca = new List<DTOGrowthMappingMarca>();
                    xListaGrowMappingMarca = fn_GrowMappingMarca_Listar(item.pr_IdMedicion, cn);
                    item.pr_ToListaMarca = xListaGrowMappingMarca;
                }

                if (xListaGrowMaping.Count > 0)
                {
                    dto = new DTORespuestaGrowthMapping();
                    dto.Estado = "True";
                    dto.Mensaje = "Se encontro la lista de growmapping";
                    dto.ListaMedicion = xListaGrowMaping;
                }
                else
                {
                    dto = new DTORespuestaGrowthMapping();
                    dto.Estado = "False";
                    dto.Mensaje = "No se encontro mediciones";
                    dto.ListaMedicion = null;
                }
            }
            catch (Exception ex)
            {
                dto = new DTORespuestaGrowthMapping();
                dto.Estado = "False";
                dto.Mensaje = ex.Message;
                dto.ListaMedicion = null;
            }
            finally
            {
                cn.Close();
            }

            oLista.Add(dto);
            return oLista;
        }

        List<DTOGrowthMappingMarca> fn_GrowMappingMarca_Listar(int pIdMedicion, SqlConnection pConn)
        {
            SqlCommand cm = new SqlCommand();
            SqlDataReader dr = null;

            List<DTOGrowthMappingMarca> oLista = new List<DTOGrowthMappingMarca>();
            DTOGrowthMappingMarca xGrowthMappingMarca = null;

            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "[dbo].[PA_VEN_GrowthMapping_Marca_Listar]";
                cm.Parameters.Add("@P_IdMedicion", SqlDbType.Int).Value = pIdMedicion;
                cm.Connection = pConn;

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    xGrowthMappingMarca = new DTOGrowthMappingMarca();
                    xGrowthMappingMarca.pr_GMP_nIdMarca = Convert.ToInt32(dr["GMP_nIdMarca"]);
                    xGrowthMappingMarca.pr_GMP_nIdMedicion = Convert.ToInt32(dr["GMP_nIdMedicion"]);
                    xGrowthMappingMarca.pr_GMP_bPromocion = Convert.ToBoolean(dr["GMP_bPromocion"]);
                    xGrowthMappingMarca.pr_GMP_bExhibicion = Convert.ToBoolean(dr["GMP_bExhibicion"]);
                    xGrowthMappingMarca.pr_GMP_bDescuento = Convert.ToBoolean(dr["GMP_bDescuento"]);
                    xGrowthMappingMarca.pr_GMP_bActivacion = Convert.ToBoolean(dr["GMP_bActivacion"]);

                    oLista.Add(xGrowthMappingMarca);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }

            return oLista;
        }
    }
}
