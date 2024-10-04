namespace DTOEntidades
{
    public class csCompany
    {
        string mServerBD = "";
        string mUserBD = "";
        string mPwBD = "";
        string mServerLic = "";
        string mNameBD = "";
        string mUserSAP = "";
        string mPwSAP = "";
        string mServerType = "";

        public string ServerBD
        {
            get { return mServerBD; }
            set { mServerBD = value; }
        }
       
        public string UserBD
        {
            get { return mUserBD; }
            set { mUserBD = value; }
        }
       
        public string PwBD
        {
            get { return mPwBD; }
            set { mPwBD = value; }
        }
      
        public string ServerLic
        {
            get { return mServerLic; }
            set { mServerLic = value; }
        }
       
        public string NameBD
        {
            get { return mNameBD; }
            set { mNameBD = value; }
        }
     
        public string UserSAP
        {
            get { return mUserSAP; }
            set { mUserSAP = value; }
        }
        
        public string PwSAP
        {
            get { return mPwSAP; }
            set { mPwSAP = value; }
        }
        

        public string ServerType
        {
            get { return mServerType; }
            set { mServerType = value; }
        }

    }
}
