//SkillExchangeContext.cs
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database.Context
{
    public class SkillExchangeContext : DbContext
    {
        // Add both constructors
        public SkillExchangeContext() { }

        public SkillExchangeContext(DbContextOptions<SkillExchangeContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-5NMO71P;Database=SkillExchangeManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0")
                    .LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UserInfo entity
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");
                entity.HasNoDiscriminator();

                // Configure properties
                entity.Property(e => e.PasswordResetToken)
                    .HasMaxLength(100);

                // Configure index for Email
                entity.HasIndex(u => u.Email)
                    .IsClustered(false)
                    .HasDatabaseName("IX_UserInfo_Email");

                // Add any additional configurations for UserInfo here
            });

            // Configure other entities
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");
                // Add Course configurations...
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("Content");
                // Add Content configurations...
            });

            // Continue with other entity configurations...
            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.ToTable("Reviews");
                // Add Reviews configurations...
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");
                // Add Subscription configurations...
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");
                // Add Payment configurations...
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                // Add Role configurations...
            });

            modelBuilder.Entity<UserRoleInfo>(entity =>
            {
                entity.ToTable("UserRoleInfo");
                // Add UserRoleInfo configurations...
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");
                // Add PaymentMethod configurations...
            });
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
