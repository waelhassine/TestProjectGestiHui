using System;

namespace GestionHuil.Controllers.Ressources
{
    public class TriturationAllRessource
    {
        public TriturationAllRessource()
        {
            StockageOlive = new StockAllTrituration();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float PrixUnitaire { get; set; }
        public float Montant { get; set; }
        public float Rendement { get; set; }
        public int HuileObtenu { get; set; }
        public int QteLivree { get; set; }
        public int HuileRestante { get; set; }
        public int Poids { get; set; }
        public StockAllTrituration StockageOlive { get; set; }
    }
}
