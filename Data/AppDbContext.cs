using PhoneShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Software_Phone>().HasKey(am => new
            {
                am.SoftwareId,
                am.PhoneId
            });

            modelBuilder.Entity<Software_Phone>().HasOne(m => m.Phone).WithMany(am => am.Softwares_Phones).HasForeignKey(m => m.PhoneId);
            modelBuilder.Entity<Software_Phone>().HasOne(m => m.Software).WithMany(am => am.Softwares_Phones).HasForeignKey(m => m.SoftwareId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Software> Softwares { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Software_Phone> Softwares_Phones { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Country> Countries { get; set; }


        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
