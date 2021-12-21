using Microsoft.EntityFrameworkCore;
using Sixgram.Auth.Database.Models;

namespace Sixgram.Auth.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RestoringCodeModel> RestoringCodes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>(user =>
            {
                user.Property(u => u.Firstname).IsRequired().HasMaxLength(50);
                user.Property(u => u.Name).IsRequired().HasMaxLength(50);
                user.Property(u => u.Firstname).HasMaxLength(30);
                user.Property(u => u.Age).IsRequired();
                user.Property(u => u.Email).IsRequired().HasMaxLength(50);
                user.Property(u => u.Password).IsRequired().HasMaxLength(100);
                user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                user.Property(u => u.DateCreated).IsRequired();
            });

            builder.Entity<RestoringCodeModel>(code =>
            {
                code.Property(c => c.Code).IsRequired().HasMaxLength(6);
                code.Property(c => c.Expiration).IsRequired();
                code.Property(u => u.DateCreated).IsRequired();
                code.Property(u => u.Email).IsRequired().HasMaxLength(50);
            });
        }
    }
}