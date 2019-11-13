using System;
namespace GestionHuil.Models
{
    public class StockageOlive
    {

        public int Id { get; set; }
        public int Poids { get; set; } 
        public string Chauffeur { get; set; }
        public string Vehicule { get; set; }
        public DateTime Date { get; set; }
        public Variete Variete { get; set; }
        public int VarieteId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Trituration Trituration { get; set; }
        public Nullable<int> TriturationId { get; set; }


    }
}
