﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(RadioBattonDbContext))]
    [Migration("20200217120254_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnName("genrename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("DAL.Entities.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("LikeValue")
                        .HasColumnName("like")
                        .HasColumnType("bit");

                    b.Property<int>("SongId")
                        .HasColumnName("songId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.ToTable("likes");
                });

            modelBuilder.Entity("DAL.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Album")
                        .HasColumnName("album")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnName("artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnName("file")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreId")
                        .HasColumnName("genreid")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnName("releasedate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasColumnName("songname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnName("userid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("UserId");

                    b.ToTable("songs");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created_At")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Password")
                        .HasColumnName("password")
                        .HasColumnType("tinyint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DAL.Entities.Like", b =>
                {
                    b.HasOne("DAL.Entities.Song", "Song")
                        .WithMany("Likes")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.Song", b =>
                {
                    b.HasOne("DAL.Entities.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.User", "User")
                        .WithMany("Songs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
