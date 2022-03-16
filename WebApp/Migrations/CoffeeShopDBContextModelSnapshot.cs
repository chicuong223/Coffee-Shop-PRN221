﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(CoffeeShopDBContext))]
    partial class CoffeeShopDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BillDate")
                        .HasColumnType("date");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("StaffUsername")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("VoucherId")
                        .HasColumnType("int")
                        .HasColumnName("VoucherID");

                    b.HasKey("Id");

                    b.HasIndex("StaffUsername");

                    b.HasIndex("VoucherId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("WebApp.Models.BillDetail", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int")
                        .HasColumnName("BillID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal?>("SubTotal")
                        .HasColumnType("money");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("BillId", "ProductId")
                        .HasName("PK__BillDeta__DAB230248977E860");

                    b.HasIndex("ProductId");

                    b.ToTable("BillDetail");
                });

            modelBuilder.Entity("WebApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebApp.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("NotificationDate")
                        .HasColumnType("date");

                    b.Property<string>("Sender")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Sender");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("WebApp.Models.NotificationDetail", b =>
                {
                    b.Property<int>("NotificationId")
                        .HasColumnType("int")
                        .HasColumnName("NotificationID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("NotificationId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("NotificationDetail");
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("ImageURL");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("Stock")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("WebApp.Models.Staff", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .IsUnicode(false)
                        .HasColumnType("varchar(14)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Username")
                        .HasName("PK__Staff__536C85E5B1F859B6");

                    b.HasIndex(new[] { "Email" }, "UQ__Staff__A9D1053468E2B7DC")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("WebApp.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .IsUnicode(false)
                        .HasColumnType("varchar(14)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("WebApp.Models.Supply", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("SupplierID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SupplyDate")
                        .HasColumnType("date");

                    b.HasKey("ProductId", "SupplierId")
                        .HasName("PK__Supply__EA10D413C91BF00F");

                    b.HasIndex("SupplierId");

                    b.ToTable("Supply");
                });

            modelBuilder.Entity("WebApp.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("Percentage")
                        .HasColumnType("float");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("UsageCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("WebApp.Models.Bill", b =>
                {
                    b.HasOne("WebApp.Models.Staff", "StaffUsernameNavigation")
                        .WithMany("Bills")
                        .HasForeignKey("StaffUsername")
                        .HasConstraintName("FK__Bill__StaffUsern__3B75D760");

                    b.HasOne("WebApp.Models.Voucher", "Voucher")
                        .WithMany("Bills")
                        .HasForeignKey("VoucherId")
                        .HasConstraintName("FK__Bill__VoucherID__3D5E1FD2");

                    b.Navigation("StaffUsernameNavigation");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("WebApp.Models.BillDetail", b =>
                {
                    b.HasOne("WebApp.Models.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillId")
                        .HasConstraintName("FK__BillDetai__BillI__403A8C7D")
                        .IsRequired();

                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("BillDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__BillDetai__Produ__412EB0B6")
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApp.Models.Notification", b =>
                {
                    b.HasOne("WebApp.Models.Staff", "SenderNavigation")
                        .WithMany("Notifications")
                        .HasForeignKey("Sender")
                        .HasConstraintName("FK__Notificat__Sende__32E0915F");

                    b.Navigation("SenderNavigation");
                });

            modelBuilder.Entity("WebApp.Models.NotificationDetail", b =>
                {
                    b.HasOne("WebApp.Models.Notification", "Notification")
                        .WithMany("NotificationDetails")
                        .HasForeignKey("NotificationId")
                        .HasConstraintName("FK_NotificationDetail_Notification")
                        .IsRequired();

                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("NotificationDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_NotificationDetail_Product")
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.HasOne("WebApp.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Product__Categor__38996AB5")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebApp.Models.Supply", b =>
                {
                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("Supplies")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Supply_Product")
                        .IsRequired();

                    b.HasOne("WebApp.Models.Supplier", "Supplier")
                        .WithMany("Supplies")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK__Supply__Supplier__300424B4")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("WebApp.Models.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("WebApp.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApp.Models.Notification", b =>
                {
                    b.Navigation("NotificationDetails");
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("NotificationDetails");

                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("WebApp.Models.Staff", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("WebApp.Models.Supplier", b =>
                {
                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("WebApp.Models.Voucher", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
