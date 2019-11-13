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
    [Migration("20191104181956_AddTableCaisse")]
    partial class AddTableCaisse
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

            modelBuilder.Entity("GestionHuil.Models.Caisse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<float>("Montant");

                    b.Property<string>("Personne");

                    b.HasKey("Id");

                    b.ToTable("Caisses");
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

            modelBuilder.Entity("GestionHuil.Models.DiversTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Montant");

                    b.Property<string>("TypeDePaiement");

                    b.Property<string>("TypeTransaction");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("DiversTransactions");
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

            modelBuilder.Entity("GestionHuil.Models.Facture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AchatId");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("GrignonId");

                    b.Property<float>("Montant");

                    b.Property<int?>("TriturationId");

                    b.Property<int>("TypeFactureId");

                    b.Property<int?>("VenteHuileId");

                    b.HasKey("Id");

                    b.HasIndex("AchatId");

                    b.HasIndex("ClientId");

                    b.HasIndex("GrignonId");

                    b.HasIndex("TriturationId");

                    b.HasIndex("TypeFactureId");

                    b.HasIndex("VenteHuileId");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("GestionHuil.Models.Grignon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Chaufeur");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Matricule");

                    b.Property<float>("MontantAchat");

                    b.Property<int>("Poids");

                    b.Property<float>("Prix_unitaire");

                    b.Property<string>("Transporteur");

                    b.Property<string>("Vehicule");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Grignons");
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

            modelBuilder.Entity("GestionHuil.Models.Reglement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("FactureId");

                    b.Property<string>("ModeReglement");

                    b.Property<float>("Montant");

                    b.HasKey("Id");

                    b.HasIndex("FactureId");

                    b.ToTable("Reglements");
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

            modelBuilder.Entity("GestionHuil.Models.TypeFacture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nom");

                    b.HasKey("Id");

                    b.ToTable("TypeFactures");
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

                    b.Property<DateTime>("Date");

                    b.Property<float>("MontantVente");

                    b.Property<float>("Prix_Unitaire");

                    b.Property<int>("Qte_Vente");

                    b.Property<int>("VarieteId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("VarieteId");

                    b.ToTable("VenteHuiles");
                });

            modelBuilder.Entity("GestionHuil.Models.Achat", b =>
                {
                    b.HasOne("GestionHuil.Models.Trituration", "Trituration")
                        .WithMany()
                        .HasForeignKey("TriturationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.DiversTransaction", b =>
                {
                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.Facture", b =>
                {
                    b.HasOne("GestionHuil.Models.Achat", "Achat")
                        .WithMany()
                        .HasForeignKey("AchatId");

                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionHuil.Models.Grignon", "Grignon")
                        .WithMany()
                        .HasForeignKey("GrignonId");

                    b.HasOne("GestionHuil.Models.Trituration", "Trituration")
                        .WithMany()
                        .HasForeignKey("TriturationId");

                    b.HasOne("GestionHuil.Models.TypeFacture", "TypeFacture")
                        .WithMany()
                        .HasForeignKey("TypeFactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionHuil.Models.VenteHuile", "VenteHuile")
                        .WithMany()
                        .HasForeignKey("VenteHuileId");
                });

            modelBuilder.Entity("GestionHuil.Models.Grignon", b =>
                {
                    b.HasOne("GestionHuil.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.ProduitHuille", b =>
                {
                    b.HasOne("GestionHuil.Models.Variete", "Variete")
                        .WithMany()
                        .HasForeignKey("VarieteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionHuil.Models.Reglement", b =>
                {
                    b.HasOne("GestionHuil.Models.Facture", "Facture")
                        .WithMany("Reglements")
                        .HasForeignKey("FactureId")
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

                    b.HasOne("GestionHuil.Models.Variete", "Variete")
                        .WithMany()
                        .HasForeignKey("VarieteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
