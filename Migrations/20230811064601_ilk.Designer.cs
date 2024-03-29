﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using codefirst_deneme.Repositories.Abstract.EfCore;

#nullable disable

namespace codefirst_deneme.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230811064601_ilk")]
    partial class ilk
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("codefirst_deneme.Models.Envanter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("EkleyenId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EkleyenId");

                    b.ToTable("Envanters");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Kullanici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Kullanicis");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rols");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Envanter", b =>
                {
                    b.HasOne("codefirst_deneme.Models.Kullanici", "EkleyenKullanici")
                        .WithMany("Envanters")
                        .HasForeignKey("EkleyenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EkleyenKullanici");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Kullanici", b =>
                {
                    b.HasOne("codefirst_deneme.Models.Rol", "Rol")
                        .WithMany("Kullanicis")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Kullanici", b =>
                {
                    b.Navigation("Envanters");
                });

            modelBuilder.Entity("codefirst_deneme.Models.Rol", b =>
                {
                    b.Navigation("Kullanicis");
                });
#pragma warning restore 612, 618
        }
    }
}
