﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using net_ef_videogame;

#nullable disable

namespace net_ef_videogame.Migrations
{
    [DbContext(typeof(TournamentContext))]
    [Migration("20230421134001_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("net_ef_videogame.SoftwareHouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SoftwareHouses");
                });

            modelBuilder.Entity("net_ef_videogame.Videogame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("SoftwareHouseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SoftwareHouseId");

                    b.ToTable("Videogames");
                });

            modelBuilder.Entity("net_ef_videogame.Videogame", b =>
                {
                    b.HasOne("net_ef_videogame.SoftwareHouse", "SoftwareHouse")
                        .WithMany()
                        .HasForeignKey("SoftwareHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SoftwareHouse");
                });
#pragma warning restore 612, 618
        }
    }
}