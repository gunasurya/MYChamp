using Microsoft.EntityFrameworkCore;
using MYChamp.Models;

namespace MYChamp.DbContexts
{
    public class MYChampDbContext(DbContextOptions<MYChampDbContext> options) : DbContext(options)
    {
        public DbSet<VisitUsInformationModel> VisitUsInformation { get; set; }
    }
}
