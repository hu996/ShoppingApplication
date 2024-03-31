using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {


        public DbSet<Customer>Customers { get; set; }
        public DbSet<Item>Items { get; set; }
        public DbSet<OrderDetails>OrdersDetails { get; set; }
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("User", "Identity");
            builder.Entity<IdentityRole>().ToTable("Role", "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");
        }
    }
}
