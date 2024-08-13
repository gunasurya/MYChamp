namespace MYChamp.Models.Authentication
{
    public class ForcefulLogout
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public DateTime LogoutTime { get; set; }
        public string Forceful_logout_by { get; set; }
    }
}
