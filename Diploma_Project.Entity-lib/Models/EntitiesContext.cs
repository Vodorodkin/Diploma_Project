using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diploma_Project.Entity_lib.Models
{
    public class EntitiesContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex("Login")
                .IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            optionsBuilder.UseSqlServer("workstation id=ZarayskyBakeryDataBase.mssql.somee.com;packet size=4096;user id=Vodorodkin_SQLLogin_1;pwd=ckjs5lhnap;data source=ZarayskyBakeryDataBase.mssql.somee.com;persist security info=False;initial catalog=ZarayskyBakeryDataBase");
            //Server = (localdb)\\mssqllocaldb; Database = DiplomaProjectDataBase; Trusted_Connection = True;
        }
        public DbSet<Client> Clients { get; set; }


        public DbSet<Manager> Managers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ShopCartProduct> ShopCartProducts { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<ShopCart> ShopCarts { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<TypeOperation> TypeOperations { get; set; }
      
    }
}
