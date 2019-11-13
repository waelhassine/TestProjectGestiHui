using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Models
{
    public class Caisse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Montant { get; set; }
        public string Personne { get; set; }
    }
}
