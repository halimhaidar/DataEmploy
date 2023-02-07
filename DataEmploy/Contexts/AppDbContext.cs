using DataEmploy.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace DataEmploy.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AccountRoles> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRoles>()
                .HasKey(ar => new { ar.NIK, ar.Role_Id });
            modelBuilder.Entity<AccountRoles>()
               .HasOne(a => a.Accounts)
               .WithMany(b => b.AccountRoles)
               .HasForeignKey(a => a.NIK);
            modelBuilder.Entity<AccountRoles>()
                .HasOne(a => a.Roles)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(a => a.Role_Id);
        }
      

    }
}
