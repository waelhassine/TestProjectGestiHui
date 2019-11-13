using System;
namespace GestionHuil.Models
{
    public class Grignon
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Poids { get; set; }
        public float Prix_unitaire { get; set; }
        public float MontantAchat { get; set; }
        public string Vehicule { get; set; }
        public string Matricule { get; set; }
        public string Transporteur { get; set; }
        public string Chaufeur { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
