using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Models
{
    public class Statistique 
    {
        public Statistique()
        {
            this.WeekTriturations = new Collection<StatiqueWeekTrituration>();
            this.WeekAchats = new Collection<StatiqueWeekTrituration>();
            this.WeekVentes = new Collection<StatiqueWeekTrituration>();
            this.WeekGrignons = new Collection<StatiqueWeekTrituration>();
            this.WeekCaisse = new Collection<StatiqueWeekTrituration>();
        }

        public int NbrTriturationTotal { get; set; }
        public float MontantTriturationTotal { get; set; }

        public int NbrAchatTotal { get; set; }
        public float MontantAchatTotal { get; set; }

        public int NbrVenteTotal { get; set; }
        public float MontantVenteTotal { get; set; }

        public int NbrGrigonsTotal { get; set; }
        public float MontantGrigonsTotal { get; set; }

        public int NbrCaisseTotal { get; set; }
        public float MontantCaiseTotal { get; set; }
        public ICollection<StatiqueWeekTrituration> WeekTriturations { get; set; }
        public ICollection<StatiqueWeekTrituration> WeekAchats { get; set; }
        public ICollection<StatiqueWeekTrituration> WeekVentes { get; set; }
        public ICollection<StatiqueWeekTrituration> WeekGrignons { get; set; }
        public ICollection<StatiqueWeekTrituration> WeekCaisse { get; set; }

    }
}
