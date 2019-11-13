using System;
namespace GestionHuil.Models
{
    public class Reglement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ModeReglement { get; set; }
        public float Montant { get; set; }
        public Facture Facture { get; set; }
        public int FactureId { get; set; }
    }
}
