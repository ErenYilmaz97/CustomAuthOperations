using CustomUserOperations.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomUserOperations.MVC.DbContext
{
    public class UserOperationsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IConfiguration Configuration;


        public UserOperationsDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("UserOperationsDbContext"));
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ResetPasswordOperation> ResetPasswordOperations { get; set; }
        public DbSet<ConfirmEmailOperation> ConfirmEmailOperations { get; set; }
    }
}