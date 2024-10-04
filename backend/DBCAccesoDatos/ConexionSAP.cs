using DTOEntidades;
using SAPbobsCOM;
using System;
using System.Configuration;

namespace DBCAccesoDatos
{
    public class ConexionSAP
    {
        public static int iRet;
        public static int iErrCod;
        public static string sErrMsg;
        public static string sQuery;
        public static Company oCompany;


        public static Recordset oRec = null;

        public Company LoginSAP(csCompany conSap)
        {
            try
            {
                if (oCompany == null || !oCompany.Connected)
                {
                    oCompany = new Company();
                    oCompany.Server = conSap.ServerBD;
                    oCompany.DbUserName = conSap.UserBD;
                    oCompany.DbPassword = conSap.PwBD;
                    if (conSap.ServerLic != "") oCompany.LicenseServer = conSap.ServerLic;
                    oCompany.CompanyDB = conSap.NameBD;
                    oCompany.UserName = conSap.UserSAP;
                    oCompany.Password = conSap.PwSAP;
                    switch (conSap.ServerType)
                    {
                        case "MSSQL2012":
                            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                            break;
                        case "MSSQL2014":
                            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                            break;
                        case "MSSQL2016":
                            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
                            break;
                        case "MSSQL2017":
                            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
                            break;
                        case "MSSQL2019":
                            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019;
                            break;
                        case "HANA":
                            oCompany.DbServerType = BoDataServerTypes.dst_HANADB;
                            break;
                    }

                    oCompany.language = BoSuppLangs.ln_Spanish_La;
                    iRet = oCompany.Connect();

                    if (iRet == 0)
                    {
                        return oCompany;
                    }
                    else
                    {
                        oCompany.GetLastError(out iErrCod, out sErrMsg);
                        throw new Exception(String.Concat(iErrCod, ": ", sErrMsg));
                    }
                }
                else
                {
                    return oCompany;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConnexionHana(csCompany conSap)
        {
            string xConexion = "";

            if (IntPtr.Size == 8)
            {
                xConexion = "Driver={HDBODBC};UID=" + conSap.UserBD + ";PWD=" + conSap.PwBD + ";SERVERNODE=" + conSap.ServerBD.Replace("NDB@", "").Replace("30013", "30015") + ";CS=" + conSap.NameBD + "";
            }
            else
            {
                xConexion = "Driver={HDBODBC32};UID=" + conSap.UserBD + ";PWD=" + conSap.PwBD + ";SERVERNODE=" + conSap.ServerBD.Replace("NDB@", "").Replace("30013", "30015") + ";CS=" + conSap.NameBD + "";
            }

            //xConexion = $"DSN=HANASERVER;Server={conSap.ServerBD.Replace("NDB@", "").Replace("30013", "30015")};UserName={conSap.UserBD};Password={conSap.PwBD};CS={conSap.NameBD}";
            //xConexion = "DSN=HANASERVER;UID=" + conSap.UserBD + ";PWD=" + conSap.PwBD + ";SERVERNODE=" + conSap.ServerBD.Replace("NDB@", "").Replace("30013", "30015") + ";DATABASE=" + conSap.NameBD + "";

            return xConexion;
        }
    }
}
