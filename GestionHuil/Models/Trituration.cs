using System;
using System.Collections.Generic;
namespace GestionHuil.Models
{
    public class Trituration
    {
        
        public Trituration()
        {
            this.StockageOlives =  new List<StockageOlive>(); 
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
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Variete Variete { get; set; }
        public int VarieteId { get; set; }
        public virtual IList<StockageOlive> StockageOlives { get; set; }
    }
}
