using GestionHuil.Models;
using System;
namespace GestionHuil.Controllers.Ressources
{
    public class FactureRessource
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public DateTime Date { get; set; }
        public TypeFacture TypeFacture { get; set; }
        public int TypeFactureId { get; set; }
        public float RestApayer { get; set; }
    }
}
