using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MYChamp.Models;
using MYChamp.Models.Authentication;

namespace MYChamp.DbContexts
{
    public class MYChampDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<VisitUsInformationModel> VisitUsInformation { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<ForcefulLogout> ForcefulLogouts { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public MYChampDbContext(DbContextOptions<MYChampDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VisitUsInformationModel>().HasData(
                new VisitUsInformationModel
                {
                    Id = 1,
                    Name = "saikumar",
                    Icon = "Icon1",
                    ImageType = "Type1",
                    Link = "Link1",
                    Active = 1
                },
                new VisitUsInformationModel
                {
                    Id = 2,
                    Name = "Another Name",
                    Icon = "Another Icon",
                    ImageType = "Another Type",
                    Link = "Another Link",
                    Active = 1
                }
            );


        }
    }
}
