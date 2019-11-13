using System;
namespace GestionHuil.Models
{
    public class VenteHuile
    {
        public int Id { get; set; }
        public int Qte_Vente { get; set; }
        public float Prix_Unitaire { get; set; }
        public DateTime Date { get; set; }
        public float MontantVente { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Variete Variete { get; set; }
        public int VarieteId { get; set; }
        public int Qte_bidon { get; set; }
        public float Prix_bidon { get; set; }


    }
}
