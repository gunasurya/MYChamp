using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.Authentication
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public bool Forcefully_logout { get; set; }
        public string Forcefully_logout_by { get; set; }
        public string UniqueId { get; set; }
        public string LogoutType { get; set; }
    }
}
