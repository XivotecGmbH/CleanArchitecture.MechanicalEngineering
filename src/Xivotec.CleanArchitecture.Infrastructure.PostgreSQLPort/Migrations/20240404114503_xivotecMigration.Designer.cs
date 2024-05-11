﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort;

#nullable disable

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Migrations
{
    [DbContext(typeof(PostgresPortDataContext))]
    [Migration("20240404114503_xivotecMigration")]
    partial class xivotecMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.RecipeAggregate.FeatherDeviceRecipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Interval")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDisplaySwitchedOn")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLedSwitchedOn")
                        .HasColumnType("boolean");

                    b.Property<int>("LedColor")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FeatherDeviceRecipes");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.RecipeAggregate.XivotecRecipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FeatherDeviceRecipeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FeatherDeviceRecipeId");

                    b.ToTable("XivotecRecipes");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ListId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Reminder")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.RecipeAggregate.XivotecRecipe", b =>
                {
                    b.HasOne("Xivotec.CleanArchitecture.Domain.RecipeAggregate.FeatherDeviceRecipe", "FeatherDeviceRecipe")
                        .WithMany()
                        .HasForeignKey("FeatherDeviceRecipeId");

                    b.Navigation("FeatherDeviceRecipe");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities.ToDoItem", b =>
                {
                    b.HasOne("Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities.ToDoList", "List")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");
                });

            modelBuilder.Entity("Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities.ToDoList", b =>
                {
                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
