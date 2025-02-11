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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5NMO71P;Database=SkillExchangeManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0").LogTo(Console.WriteLine, LogLevel.Information); ;
            //optionsBuilder.UseSqlServer(@"Server=ASUS;Database=SkillExchangeManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0").LogTo(Console.WriteLine, LogLevel.Information); ;

            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-1H8PV8J\SQLEXPRESS01;Database=SkillExchangeManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0");
        }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRoleInfo> UserRoleInfo { get; set; }
    }

}