﻿// <auto-generated />
using ConsoleAppEFCoreConcurrency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleAppEFCoreConcurrency.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20220328030815_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConsoleAppEFCoreConcurrency.House", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Owner")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
