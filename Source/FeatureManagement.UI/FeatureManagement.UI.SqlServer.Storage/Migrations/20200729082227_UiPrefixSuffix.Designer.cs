﻿// <auto-generated />
using System;
using FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FeatureManagement.UI.SqlServer.Storage.Migrations
{
    [DbContext(typeof(FeatureManagementDb))]
    [Migration("20200729082227_UiPrefixSuffix")]
    partial class UiPrefixSuffix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.ClientFeatureData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BooleanValue")
                        .HasColumnType("bit");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal?>("DecimalValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int?>("IntValue")
                        .HasColumnType("int");

                    b.Property<string>("StringValue")
                        .HasColumnType("NVARCHAR(MAX)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId", "ClientId")
                        .IsUnique();

                    b.ToTable("ClientFeatureData","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.DecimalFeatureChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Choice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId", "Choice")
                        .IsUnique();

                    b.ToTable("DecimalFeatureChoice","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("UiPrefix")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("UiSuffix")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ValueType")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Feature","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.IntFeatureChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Choice")
                        .HasColumnType("int");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId", "Choice")
                        .IsUnique();

                    b.ToTable("IntFeatureChoice","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.ServerFeatureData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BooleanValue")
                        .HasColumnType("bit");

                    b.Property<decimal?>("DecimalValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int?>("IntValue")
                        .HasColumnType("int");

                    b.Property<string>("StringValue")
                        .HasColumnType("NVARCHAR(MAX)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId")
                        .IsUnique();

                    b.ToTable("ServerFeatureData","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.StringFeatureChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Choice")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId", "Choice")
                        .IsUnique();

                    b.ToTable("StringFeatureChoice","FeatureManagement");
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.ClientFeatureData", b =>
                {
                    b.HasOne("FeatureManagement.UI.Core.Data.Feature", "Feature")
                        .WithMany("Clients")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.DecimalFeatureChoice", b =>
                {
                    b.HasOne("FeatureManagement.UI.Core.Data.Feature", "Feature")
                        .WithMany("DecimalFeatureChoices")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.IntFeatureChoice", b =>
                {
                    b.HasOne("FeatureManagement.UI.Core.Data.Feature", "Feature")
                        .WithMany("IntFeatureChoices")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.ServerFeatureData", b =>
                {
                    b.HasOne("FeatureManagement.UI.Core.Data.Feature", "Feature")
                        .WithOne("Server")
                        .HasForeignKey("FeatureManagement.UI.Core.Data.ServerFeatureData", "FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FeatureManagement.UI.Core.Data.StringFeatureChoice", b =>
                {
                    b.HasOne("FeatureManagement.UI.Core.Data.Feature", "Feature")
                        .WithMany("StringFeatureChoices")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}