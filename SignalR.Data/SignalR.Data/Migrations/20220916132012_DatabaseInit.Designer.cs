﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignalR.Data;

#nullable disable

namespace SignalR.Data.Migrations
{
    [DbContext(typeof(appContext))]
    [Migration("20220916132012_DatabaseInit")]
    partial class DatabaseInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SignalR.Shared.Entities.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PurchaseID")
                        .HasColumnType("int");

                    b.Property<bool>("isLeftDoorOpen")
                        .HasColumnType("bit");

                    b.Property<bool>("isRightDoorOpen")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("PurchaseID");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CarType = "BMW",
                            isLeftDoorOpen = false,
                            isRightDoorOpen = false
                        },
                        new
                        {
                            ID = 2,
                            CarType = "Mercedes",
                            isLeftDoorOpen = false,
                            isRightDoorOpen = false
                        });
                });

            modelBuilder.Entity("SignalR.Shared.Entities.Purchase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ProcessDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("SignalR.Shared.Entities.Car", b =>
                {
                    b.HasOne("SignalR.Shared.Entities.Purchase", null)
                        .WithMany("Cars")
                        .HasForeignKey("PurchaseID");
                });

            modelBuilder.Entity("SignalR.Shared.Entities.Purchase", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}