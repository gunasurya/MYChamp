using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MYChamp.AuthModel
{
    public class Login_model
    {
        [Required(ErrorMessage = "Username is requried")]

        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is Requried")]
        [DataType(DataType.Password)]

        public string? Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }


    }
    
    public static class MyChampIdentityList 
    {
        public static List<MyChampIdentity> MyChampIdentities; 
        static MyChampIdentityList() { MyChampIdentities = new List<MyChampIdentity>();
        }
        public static MyChampIdentity GetUserIdentity(string loginId) { 
            return MyChampIdentities.FirstOrDefault(t => t.LoginId == loginId); 
        }  
    }

    public class MyChampIdentity {
        public string SessionId { get; set; }
        public string SeqNo { get; set; } 
        public string LoginId { get; set; }
        public string IpAddress { get; set; }
        public DateTime LastAccTime { get; set; } 

        public bool IsSessionTaken { get; set; }
        public int TimezoneDateDiffInMin { get; set; } 
      
        public string DateTimeDisplayFormat { get; set; } 
        public string ShortDateDisplayFormat { get; set; }
        public string UserTimeZone { get; set; }
        public string ServerTimeZone { get; set; } 
        public int TimeZoneType { get; set; }
        }


}
