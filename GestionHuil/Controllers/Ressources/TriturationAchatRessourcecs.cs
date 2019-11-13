using GestionHuil.Models;
using System;
namespace GestionHuil.Controllers.Ressources
{
    public class TriturationAchatRessourcecs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float PrixUnitaire { get; set; }
        public float Montant { get; set; }
        public float Rendement { get; set; }
        public int HuileObtenu { get; set; }
        public int QteLivree { get; set; }
        public int HuileRestante { get; set; }
        public int Poids { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Variete Variete { get; set; }
        public int VarieteId { get; set; }
    }
}
