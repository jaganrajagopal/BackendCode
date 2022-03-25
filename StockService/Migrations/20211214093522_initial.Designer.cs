﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Data.Entities;

namespace StockService.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20211214093522_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StockService.Data.Entities.Company", b =>
                {
                    b.Property<int>("CompanyCode")
                        .HasColumnType("int");

                    b.Property<string>("CompanyCEO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CompanyTurnover")
                        .HasColumnType("float");

                    b.Property<string>("CompanyWebsite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyCode");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("StockService.Data.Entities.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("StockDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockExchange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StockPrice")
                        .HasColumnType("float");

                    b.HasKey("StockId");

                    b.HasIndex("CompanyCode");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockService.Data.Entities.Stock", b =>
                {
                    b.HasOne("StockService.Data.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
