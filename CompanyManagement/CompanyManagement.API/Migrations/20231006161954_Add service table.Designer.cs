﻿// <auto-generated />
using CompanyManagement.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyManagement.API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231006161954_Add service table")]
    partial class Addservicetable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("CompanyManagement.API.Models.AddressModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("AddressTypeId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "addressTypeId");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "city");

                    b.Property<string>("ClientId")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "clientId");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "street");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "zipCode");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("ClientId");

                    b.ToTable("Addresses");

                    b.HasAnnotation("Relational:JsonPropertyName", "addresses");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.AddressTypeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "label");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");

                    b.HasAnnotation("Relational:JsonPropertyName", "addressType");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.ClientModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "phoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasAnnotation("Relational:JsonPropertyName", "client");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.ServiceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "price");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "unit");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.AddressModel", b =>
                {
                    b.HasOne("CompanyManagement.API.Models.AddressTypeModel", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("CompanyManagement.API.Models.ClientModel", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("AddressType");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.AddressTypeModel", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("CompanyManagement.API.Models.ClientModel", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
