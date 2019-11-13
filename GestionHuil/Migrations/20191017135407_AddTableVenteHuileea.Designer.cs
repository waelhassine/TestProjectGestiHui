﻿// <auto-generated />
using System;
using GestionHuil.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionHuil.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191017135407_AddTableVenteHuileea")]
    partial class AddTableVenteHuileea
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionHuil.Models.Achat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("MontantAchat");

                    b.Property<string>("Note");

                    b.Property<float>("Prix_unitaire");

                    b.Property<int>("QteAchete");

                    b.Property<int>("TriturationId");

                    b.HasKey("Id");

                    b.HasIndex("TriturationId");

                    b.ToTable("Achats");
                });

            modelBuilder.Entity("GestionHuil.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gsm");

                    b.Property<string>("Nom");

                    b.Property<float>("Solde");

                    b.Property<string>("Tel");

                    b.Property<string>("Ville");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("GestionHuil.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Fonction");

                    b.Property<string>("NomPrenom");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Photo");

                    b.Property<string>("Status");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("GestionHuil.Models.ProduitHuille", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Prix_unitaire");

                    b.Property<int>("Qte_En_Stock");

                    b.Property<int>("VarieteId");

                    b.HasKey("Id");

                    b.HasIndex("VarieteId");

                    b.ToTable("ProduitHuilles");
                });

            modelBuilder.Entity("GestionHuil.Models.StockageOlive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Chauffeur");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Poids");

                    b.Property<int?>("TriturationId");

                    b.Property<int>("VarieteId");

                    b.Property<string>("Vehicule");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("TriturationId");

                    b.HasIndex("VarieteId");

                    b.ToTable("StockageOlives");
                });

            modelBuilder.Entity("GestionHuil.Models.Trituration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("HuileObtenu");

                    b.Property<int>("HuileRestante");

                    b.Property<float>("Montant");

                    b.Property<int>("Poids");

                    b.Property<float>("PrixUnitaire");

                    b.Property<int>("QteLivree");

                    b.Property<float>("Rendement");

                    b.Property<int>("VarieteId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("VarieteId");

                    b.ToTable("Triturations");
                });

            modelBuilder.Entity("GestionHuil.Models.Variete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Varietes");
                });

            modelBuilder.Entity("GestionHuil.Models.VenteHuile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<float>("MontantVente");

                    b.Property<float>("Prix_Unitaire");

                    b.Property<int>("Qte_Vente");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("VenteHuiles");
                });

            modelBuilder.Entity("GestionHuil.Models.Achat", b =>
                {
                    b.HasOne("GestionHuil.Models.Trituration", "Trituration")
                        .WithMany()
                        .HasForeignKey("TriturationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.ProduitHuille", b =>
                {
                    b.HasOne("GestionHuil.Models.Variete", "Variete")
                        .WithMany()
                        .HasForeignKey("VarieteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.StockageOlive", b =>
                {
                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionHuil.Models.Trituration", "Trituration")
                        .WithMany("StockageOlives")
                        .HasForeignKey("TriturationId");

                    b.HasOne("GestionHuil.Models.Variete", "Variete")
                        .WithMany()
                        .HasForeignKey("VarieteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.Trituration", b =>
                {
                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionHuil.Models.Variete", "Variete")
                        .WithMany()
                        .HasForeignKey("VarieteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.VenteHuile", b =>
                {
                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
