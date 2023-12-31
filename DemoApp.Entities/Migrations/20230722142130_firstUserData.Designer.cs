﻿// <auto-generated />
using System;
using DemoApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoApp.Entities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230722142130_firstUserData")]
    partial class firstUserData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DemoApp.Entities.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Tombstoned")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"),
                            Created = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "test@gmail.com",
                            FirstName = "Azer",
                            LastName = "Halovic",
                            Modified = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)),
                            PhoneNumber = "559001122",
                            Tombstoned = false,
                            UserId = new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751")
                        },
                        new
                        {
                            Id = new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"),
                            Created = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6467), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "test3@gmail.com",
                            FirstName = "Veli",
                            LastName = "Halovic",
                            Modified = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6468), new TimeSpan(0, 0, 0, 0, 0)),
                            PhoneNumber = "559001122",
                            Tombstoned = false,
                            UserId = new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751")
                        });
                });

            modelBuilder.Entity("DemoApp.Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastSigninDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Tombstoned")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"),
                            Created = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "administrator@pa.local",
                            HashedPassword = "10000.UTSxiyDe7455x7TCeB9ELg==.PhkV6KCeskBhxM1vgtBUOzXreczKGiYyJYcWjNHQTss=",
                            Modified = new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)),
                            Tombstoned = false,
                            Username = "Administrator"
                        });
                });

            modelBuilder.Entity("DemoApp.Entities.Models.Contact", b =>
                {
                    b.HasOne("DemoApp.Entities.Models.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DemoApp.Entities.Models.User", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
