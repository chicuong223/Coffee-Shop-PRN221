using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebApp.Models
{
    public partial class CoffeeShopDBContext : DbContext
    {
        public CoffeeShopDBContext()
        {
        }

        public CoffeeShopDBContext(DbContextOptions<CoffeeShopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationDetail> NotificationDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                var config = builder.Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("CoffeeShopDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill")
                    .HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.BillDate).HasColumnType("date");

                entity.Property(e => e.StaffUsername)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.HasOne(d => d.StaffUsernameNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.StaffUsername)
                    .HasConstraintName("FK__Bill__StaffUsern__3B75D760");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Bill__VoucherID__3D5E1FD2");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.HasKey(e => new { e.BillId, e.ProductId })
                    .HasName("PK__BillDeta__DAB230248977E860");

                entity.ToTable("BillDetail");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillDetai__BillI__403A8C7D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillDetai__Produ__412EB0B6");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category")
                .HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification").HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.NotificationDate).HasColumnType("date");

                entity.Property(e => e.Sender)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Sender)
                    .HasConstraintName("FK__Notificat__Sende__32E0915F");
            });

            modelBuilder.Entity<NotificationDetail>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.ProductId });

                entity.ToTable("NotificationDetail");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationDetails)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationDetail_Notification");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.NotificationDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationDetail_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product").HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ImageURL).HasColumnName("ImageURL").HasMaxLength(256);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Product__Categor__38996AB5");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier").HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.SupplierId })
                    .HasName("PK__Supply__EA10D413C91BF00F");

                entity.ToTable("Supply");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.SupplyDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supply_Product");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Supply__Supplier__300424B4");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher").HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Staff__536C85E5B1F859B6");

                entity.ToTable("Staff");

                entity.HasIndex(e => e.Email, "UQ__Staff__A9D1053468E2B7DC")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
