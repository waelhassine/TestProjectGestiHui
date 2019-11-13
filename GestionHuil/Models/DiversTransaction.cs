using System;
namespace GestionHuil.Models
{
    public class DiversTransaction
    {
        public int Id { get; set; }
        public string TypeTransaction { get; set; }
        public DateTime Date { get; set; }
        public int Montant { get; set; }
        public string TypeDePaiement { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }

    }
}
