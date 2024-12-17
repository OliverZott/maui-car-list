﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace maui_car_list.Api.Migrations
{
    [DbContext(typeof(CarListDbContext))]
    [Migration("20241029165423_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Make = "BMW",
                            Model = "5",
                            Vin = "bmwvin1"
                        },
                        new
                        {
                            Id = 2,
                            Make = "Subaru",
                            Model = "Impreza",
                            Vin = "subaruvin1"
                        },
                        new
                        {
                            Id = 3,
                            Make = "Honda",
                            Model = "Civic",
                            Vin = "hondavin1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}