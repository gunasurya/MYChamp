using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MYChamp.AuthModel;
using MYChamp.Models;

namespace MYChamp.DbContexts
{
    public class MYChampDbContext : IdentityDbContext<AppUser>
    {
        public MYChampDbContext(DbContextOptions<MYChampDbContext> options) : base(options)
        {
        }

        // DbSet properties for other entities
        public DbSet<VisitUsInformationModel> VisitUsInformation { get; set; }
        public DbSet<TestRegistrationModel> testregistration { get; set; }
        public DbSet<PersonalityQuestionModel> personality_questions { get; set; }
        public DbSet<TestAttemptModel> TestAttempts { get; set; }
        public DbSet<Session_model> sessionlog  { get; set; }
        public DbSet<ForcefulLogout> forcefulLogouts { get; set; }
        public DbSet<Register_Model> signinaccounts { get; set; }


    }
}
