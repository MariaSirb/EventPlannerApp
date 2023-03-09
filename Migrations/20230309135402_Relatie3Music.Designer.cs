﻿// <auto-generated />
using System;
using EventPlannerApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventPlannerApp.Migrations
{
    [DbContext(typeof(EventPlannerAppContext))]
    [Migration("20230309135402_Relatie3Music")]
    partial class Relatie3Music
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EventPlannerApp.Models.MyEvent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EventTypeID")
                        .HasColumnType("int");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("Mention")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MusicID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("EventTypeID");

                    b.HasIndex("LocationID");

                    b.HasIndex("MusicID");

                    b.ToTable("MyEvent");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.EventType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LocationPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaximumCapacity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("ItemImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MenuTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MenuTypeID");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.MenuType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MenuType");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Music", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("DjImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DjName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DjPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Music");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Photograph", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("PhotographImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotographName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PhotographPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Photograph");
                });

            modelBuilder.Entity("EventPlannerApp.Models.MyEvent", b =>
                {
                    b.HasOne("EventPlannerApp.Models.Services.EventType", "EventType")
                        .WithMany("MyEvents")
                        .HasForeignKey("EventTypeID");

                    b.HasOne("EventPlannerApp.Models.Services.Location", "Location")
                        .WithMany("MyEvents")
                        .HasForeignKey("LocationID");

                    b.HasOne("EventPlannerApp.Models.Services.Music", "Music")
                        .WithMany("MyEvents")
                        .HasForeignKey("MusicID");

                    b.Navigation("EventType");

                    b.Navigation("Location");

                    b.Navigation("Music");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Menu", b =>
                {
                    b.HasOne("EventPlannerApp.Models.Services.MenuType", "MenuType")
                        .WithMany("Menues")
                        .HasForeignKey("MenuTypeID");

                    b.Navigation("MenuType");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.EventType", b =>
                {
                    b.Navigation("MyEvents");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Location", b =>
                {
                    b.Navigation("MyEvents");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.MenuType", b =>
                {
                    b.Navigation("Menues");
                });

            modelBuilder.Entity("EventPlannerApp.Models.Services.Music", b =>
                {
                    b.Navigation("MyEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
