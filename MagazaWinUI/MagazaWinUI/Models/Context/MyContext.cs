using MagazaWinUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazaWinUI.Models.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Siparişler");
            modelBuilder.Entity<Product>().ToTable("Ürünler");
            modelBuilder.Entity<OrderDetail>().ToTable("Sipariş Detayları");

            //BaseEntity class'ımız olmadığı için ignore edilecek bir ID prop'u bulunmamakta OrderDetail'in
            modelBuilder.Entity<OrderDetail>().HasKey(x => new
            {
                x.OrderID,
                x.ProductID
            });


            modelBuilder.Entity<OrderDetail>().Property(x => x.Quantity).HasColumnName("Adet");

            modelBuilder.Entity<Order>().Property(x => x.FullName).HasColumnName("İsim Soyisim");
            modelBuilder.Entity<Order>().Property(x => x.Phone).HasColumnName("Telefon");
            modelBuilder.Entity<Order>().Property(x => x.Country).HasColumnName("Ülke");
            modelBuilder.Entity<Order>().Property(x => x.City).HasColumnName("Şehir");
            modelBuilder.Entity<Order>().Property(x => x.District).HasColumnName("İlçe");
            modelBuilder.Entity<Order>().Property(x => x.Neighborhood).HasColumnName("Mahalle");
            modelBuilder.Entity<Order>().Property(x => x.Street).HasColumnName("Sokak");
            modelBuilder.Entity<Order>().Property(x => x.AptNo).HasColumnName("Apt No");
            modelBuilder.Entity<Order>().Property(x => x.Flat).HasColumnName("Kapı No");
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> Details { get; set; }


    }
}
