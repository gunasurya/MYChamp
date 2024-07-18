using Microsoft.EntityFrameworkCore;
using MYChamp.Models;

namespace MYChamp.DbContexts
{
    public class MYChampDbContext(DbContextOptions<MYChampDbContext> options) : DbContext(options)
    {
        public DbSet<VisitUsInformationModel> VisitUsInformation { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<GST>GSTs { get; set; }
    }
}
