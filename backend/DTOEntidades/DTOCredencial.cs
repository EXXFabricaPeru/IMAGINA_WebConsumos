using System.Runtime.Serialization;

namespace DTOEntidades
{
    [DataContract]
    public class DTOCredencial
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Token { get; set; }
    }
}
