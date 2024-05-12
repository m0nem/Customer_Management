using Customer_Management.Identity.Configuartions;
using Customer_Management.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Customer_Management.Identity
{
    public class CustomerManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CustomerManagementIdentityDbContext(DbContextOptions<CustomerManagementIdentityDbContext> options) 
            : base(options)
        {

        }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
