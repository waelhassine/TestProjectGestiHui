using System;
using System.Collections.Generic;
namespace GestionHuil.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public DateTime Date { get; set; }
        public TypeFacture TypeFacture { get; set; }
        public int TypeFactureId { get; set; }
        public Trituration Trituration { get; set; }
        public Nullable<int> TriturationId { get; set; }
        public Grignon Grignon { get; set; }
        public Nullable<int> GrignonId { get; set; }
        public Achat Achat { get; set; }
        public Nullable<int> AchatId { get; set; }
        public VenteHuile VenteHuile { get; set; }
        public Nullable<int> VenteHuileId { get; set; }
        public virtual ICollection<Reglement> Reglements { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }

    }
}
