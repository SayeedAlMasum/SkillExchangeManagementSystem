//SkillExchangeContext.cs
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database.Context
{
    public class SkillExchangeContext : DbContext
    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5NMO71P;Database=SkillExchangeManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map entities to their own tables
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
       
            // Prevent EF Core from using a discriminator column
            modelBuilder.Entity<UserInfo>().HasNoDiscriminator();
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRoleInfo> UserRoleInfo { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
    }
}
