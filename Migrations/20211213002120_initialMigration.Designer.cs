﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using account_ms.Data;

namespace account_ms.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211213002120_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("account_ms.Models.Client", b =>
                {
                    b.Property<int>("idClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("active")
                        .HasColumnType("integer")
                        .HasColumnName("Active");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("email");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("fName");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("image");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("password");

                    b.Property<string>("sName")
                        .HasColumnType("varChar(150)")
                        .HasColumnName("sName");

                    b.Property<string>("sureName")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("sureName");

                    b.Property<long>("telNumber")
                        .HasMaxLength(10)
                        .HasColumnType("bigint")
                        .HasColumnName("telNumber");

                    b.HasKey("idClient");

                    b.HasIndex("email")
                        .IsUnique();

                    b.HasIndex("telNumber")
                        .IsUnique();

                    b.ToTable("Client");
                });

            modelBuilder.Entity("account_ms.Models.CreditCard", b =>
                {
                    b.Property<int>("idCard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("cardNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("cardNumber");

                    b.Property<int>("cvv")
                        .HasMaxLength(4)
                        .HasColumnType("integer")
                        .HasColumnName("cvv");

                    b.Property<string>("dueDate")
                        .IsRequired()
                        .HasColumnType("varChar(150)")
                        .HasColumnName("dueDate");

                    b.Property<int>("idClient")
                        .HasColumnType("integer")
                        .HasColumnName("idClient");

                    b.HasKey("idCard");

                    b.HasIndex("cardNumber")
                        .IsUnique();

                    b.HasIndex("idClient");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("account_ms.Models.CreditCard", b =>
                {
                    b.HasOne("account_ms.Models.Client", "client")
                        .WithMany("creditCards")
                        .HasForeignKey("idClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("account_ms.Models.Client", b =>
                {
                    b.Navigation("creditCards");
                });
#pragma warning restore 612, 618
        }
    }
}
