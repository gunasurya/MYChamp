using Microsoft.AspNetCore.Identity; // Import this namespace
using MYChamp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MYChamp.AuthModel
{
    public class Session_model
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }

       // [ForeignKey("AspNetUsers")] // Specify the foreign key relationship
        //public string UserId { get; set; }

      //  public AppUser AspNetUsers { get; set; } // Use your custom user class AppUser

        public string UserName { get; set; }

        public string IPAddress { get; set; }

        public DateTime LoginTime { get; set; }

        public bool IsActive { get; set; }

        public int status { get; set; }
          
        public bool forcefully_logout { get; set; }

        public string forcefully_logout_by { get; set; }

        public string logoutType { get; set; }
        public DateTime LogoutTime {  get; set; }
    }
}
 