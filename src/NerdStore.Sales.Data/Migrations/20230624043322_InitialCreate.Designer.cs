﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStore.Sales.Data;

#nullable disable

namespace NerdStore.Sales.Data.Migrations
{
    [DbContext(typeof(SalesContext))]
    [Migration("20230624043322_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence<int>("MySequence")
                .StartsAt(1001L);

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("CouponType")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Usage")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UsageDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Coupons", (string)null);
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CouponId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CouponUsed")
                        .HasColumnType("bit");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MySequence");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CouponId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.Order", b =>
                {
                    b.HasOne("NerdStore.Sales.Domain.Order.Coupon", "Coupon")
                        .WithMany("Orders")
                        .HasForeignKey("CouponId")
                        .IsRequired();

                    b.Navigation("Coupon");
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.OrderItem", b =>
                {
                    b.HasOne("NerdStore.Sales.Domain.Order.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderItemId")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.Coupon", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("NerdStore.Sales.Domain.Order.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
