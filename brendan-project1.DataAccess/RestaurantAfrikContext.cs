using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace brendan_project1
{
    public partial class RestaurantAfrikContext : DbContext
    {
        public RestaurantAfrikContext()
        {
        }

        public RestaurantAfrikContext(DbContextOptions<RestaurantAfrikContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                                   
            config.AddUserSecrets<RestaurantAfrikContext>();
            var Configuration = config.Build();
            string conn = Configuration["ConnectionStrings:RestaurantAfrikContext"];
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conn);
            }
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Drinks> Drinks { get; set; }
        public virtual DbSet<Foods> Foods { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Orderline> Orderline { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<StoreLoc> StoreLoc { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(15)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Drinks>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.HasKey(e => e.FoodId)
                    .HasName("PK_Food");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Inventoryid).HasColumnName("inventoryid");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("Total Price")
                    .HasColumnType("money");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Foods");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_StoreId");
            });

            modelBuilder.Entity<Orderline>(entity =>
            {
                entity.HasKey(e => e.Orderline1)
                    .HasName("PK__orderlin__C193051137E1F846");

                entity.ToTable("orderline");

                entity.Property(e => e.Orderline1).HasColumnName("orderline");

                entity.Property(e => e.Foodid).HasColumnName("foodid");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Orderline)
                    .HasForeignKey(d => d.Foodid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__foodi__2BFE89A6");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderline)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__order__30C33EC3");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Orders_1");

                entity.Property(e => e.FoodId).HasColumnName("FoodID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime)
                    .HasColumnName("Order_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_CustomerId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores_StoreId");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Album).HasColumnName("album");

                entity.Property(e => e.Artist).HasColumnName("artist");

                entity.Property(e => e.Genre).HasColumnName("genre");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<StoreLoc>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK_Stores");

                entity.ToTable("StoreLoc", "AFRICANRESTAURANT");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("Zip Code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
