﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMaze.EfStuff;

namespace WebMaze.Migrations
{
    [DbContext(typeof(WebContext))]
    [Migration("20211115172430_ConnecteUserToCell")]
    partial class ConnecteUserToCell
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebMaze.EfStuff.DbModel.NewCellSuggestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ApproverId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreaterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FatigueChange")
                        .HasColumnType("int");

                    b.Property<int>("HealtsChange")
                        .HasColumnType("int");

                    b.Property<int>("MoneyChange")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("CreaterId");

                    b.ToTable("NewCellSuggestions");
                });

            modelBuilder.Entity("WebMaze.EfStuff.DbModel.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebMaze.EfStuff.DbModel.NewCellSuggestion", b =>
                {
                    b.HasOne("WebMaze.EfStuff.DbModel.User", "Approver")
                        .WithMany("CellSuggestionsWhichIAprove")
                        .HasForeignKey("ApproverId");

                    b.HasOne("WebMaze.EfStuff.DbModel.User", "Creater")
                        .WithMany("MyCellSuggestions")
                        .HasForeignKey("CreaterId");

                    b.Navigation("Approver");

                    b.Navigation("Creater");
                });

            modelBuilder.Entity("WebMaze.EfStuff.DbModel.User", b =>
                {
                    b.Navigation("CellSuggestionsWhichIAprove");

                    b.Navigation("MyCellSuggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
