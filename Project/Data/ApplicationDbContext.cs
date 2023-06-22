using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Reflection.Emit;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       /* public DbSet<User> Users { get; set; }*/
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Bill> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           /* builder.Entity<User>().ToTable("User");*/
            builder.Entity<Bill>().ToTable("Order");
            builder.Entity<OrderDetail>().ToTable("OrderDetail");
            builder.Entity<Drink>().ToTable("Drink");
            builder.Entity<Topping>().ToTable("Topping");
            builder.Entity<Category>().ToTable("Category");

            builder.Entity<Size>()
               .HasKey(o => new { o.DrinkId, o.DrinkSize });
        }

       /* public DbSet<Project.Models.Cart> Cart { get; set; }*/
    }
}