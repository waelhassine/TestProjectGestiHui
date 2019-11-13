using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Trituration> Triturations { get; set; }
        public DbSet<Variete> Varietes { get; set; }
        public DbSet<Achat> Achats { get; set; }
       public DbSet<StockageOlive> StockageOlives { get; set; }
        public DbSet<ProduitHuille> ProduitHuilles { get; set; }
        public DbSet<VenteHuile> VenteHuiles { get; set; }
        public DbSet<DiversTransaction> DiversTransactions { get; set; }
        public DbSet<TypeFacture> TypeFactures { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Reglement> Reglements { get; set; }
        public DbSet<Grignon> Grignons { get; set; }
        public DbSet<Caisse> Caisses { get; set; }
        

    }
}
