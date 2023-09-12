﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using orderService.domain;

#nullable disable

namespace ShopCart.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230911200109_interview")]
    partial class interview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("orderService.domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IsbnNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, implementing interfaces, and inheriting classes. Next, you'll take on .NET APIs for performing tasks like managing and querying data, working with the filesystem, and serialization. As you progress, you'll also explore examples of cross-platform projects you can build and deploy, such as websites and services using ASP.NET Core",
                            IsbnNumber = "978-1803237800",
                            Price = 200m,
                            Title = "C# 11 and .NET 7"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7",
                            IsbnNumber = "978-1803237800",
                            Price = 400m,
                            Title = ".NET 7 book"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7,this latest edition of our guide will get you coding in C# with confidence.",
                            IsbnNumber = "978-1803237800",
                            Price = 200m,
                            Title = "C# 11 book"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, ",
                            IsbnNumber = "978-1803237800",
                            Price = 200m,
                            Title = "C# book"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, implementing interfaces, and inheriting classes.",
                            IsbnNumber = "978-1803237800",
                            Price = 400m,
                            Title = ".Net Book "
                        });
                });

            modelBuilder.Entity("orderService.domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("orderService.domain.Entities.OrderBook", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int?>("BookId1")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "BookId");

                    b.HasIndex("BookId");

                    b.HasIndex("BookId1");

                    b.ToTable("OrderBooks");
                });

            modelBuilder.Entity("orderService.domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("orderService.domain.Entities.Order", b =>
                {
                    b.HasOne("orderService.domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("orderService.domain.Entities.OrderBook", b =>
                {
                    b.HasOne("orderService.domain.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("orderService.domain.Entities.Book", null)
                        .WithMany("OrderBooks")
                        .HasForeignKey("BookId1");

                    b.HasOne("orderService.domain.Entities.Order", "Order")
                        .WithMany("OrderBooks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("orderService.domain.Entities.Book", b =>
                {
                    b.Navigation("OrderBooks");
                });

            modelBuilder.Entity("orderService.domain.Entities.Order", b =>
                {
                    b.Navigation("OrderBooks");
                });

            modelBuilder.Entity("orderService.domain.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
