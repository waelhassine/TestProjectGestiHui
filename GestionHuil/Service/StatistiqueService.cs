using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class StatistiqueService : IDisposable, IStatistiqueService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        private Statistique statistiquea;
        private Statistique statistiqueb;
        public StatiqueWeekTrituration columntrituration;
        int totaltrituration = 0;
        int totalachat = 0;
        int totalvente = 0;
        int totalgrignons = 0;
        int totalCaisse = 0;
        float montanttrituration = 0;
        float montantachat = 0;
        float montantvente = 0;
        float montantgrignons = 0;
        float montantcaisse = 0;

        public StatistiqueService(DataContext context)
        {
            _context = context;
            statistiquea = new Statistique();
            statistiqueb = new Statistique();

        }
        public async Task<Statistique> GetAllStatistiqueAsync()
        {
            //statistique.NbrTriturationTotal = await _context.Factures.Where(c => c.TypeFactureId == 1).CountAsync();
            var trituration = await _context.Factures.AsNoTracking().Include(c => c.Trituration).Where(c => c.TypeFactureId == 1).ToListAsync();
            foreach (var item in trituration)
            {
                statistiquea.MontantTriturationTotal = statistiquea.MontantTriturationTotal + item.Montant;
                statistiquea.NbrTriturationTotal = statistiquea.NbrTriturationTotal + item.Trituration.Poids;
            }
            ////////////////////////////////////
           // statistique.NbrAchatTotal = await _context.Factures.Where(c => c.TypeFactureId == 2).CountAsync();
            var achats = await _context.Factures.Include(c => c.Achat).AsNoTracking().Where(c => c.TypeFactureId == 2).ToListAsync();
            foreach (var item in achats)
            {
                statistiquea.MontantAchatTotal = statistiquea.MontantAchatTotal + item.Montant;
                statistiquea.NbrAchatTotal = statistiquea.NbrAchatTotal + item.Achat.QteAchete;
            }
            ////////////////////////////////////
            //statistique.NbrVenteTotal = await _context.Factures.Where(c => c.TypeFactureId == 3).CountAsync();
            var ventes = await  _context.Factures.Include(c => c.VenteHuile).AsNoTracking().Where(c => c.TypeFactureId == 3).ToListAsync();
            foreach (var item in ventes)
            {
                statistiquea.MontantVenteTotal = statistiquea.MontantVenteTotal + item.Montant;
                statistiquea.NbrVenteTotal = statistiquea.NbrVenteTotal + item.VenteHuile.Qte_Vente;
                // statistiquea.NbrVenteTotal = 0;
            }
            ///////////////////////////////////
            //statistique.NbrGrigonsTotal = await _context.Factures.Include(c => c.Grignon).Where(c => c.TypeFactureId == 4).CountAsync();
            var grigons =  await _context.Factures.Where(c => c.TypeFactureId == 4).Include(c => c.Grignon).AsNoTracking().ToListAsync();
            foreach (var item in grigons)
            {
                statistiquea.MontantGrigonsTotal = statistiquea.MontantGrigonsTotal + item.Montant;
                statistiquea.NbrGrigonsTotal = statistiquea.NbrGrigonsTotal + item.Grignon.Poids;
            }
            ///////////////////////
            statistiquea.NbrCaisseTotal = await _context.Caisses.CountAsync();
            var caises = await _context.Caisses.AsNoTracking().ToListAsync();
            foreach (var item in caises)
            {
                statistiquea.MontantCaiseTotal = statistiquea.MontantCaiseTotal + item.Montant;
            }
            //////////////////////////////
            ///

            var timeNows = FirstDateOfWeekISO8601(DateTime.Now.Date.Year, GetIso8601WeekOfYear(DateTime.Now));
            var weeaka = WeekDays(timeNows);
            
            foreach (var item in weeaka)
            {
                totaltrituration = 0; totalachat = 0; totalvente = 0; totalgrignons = 0;totalCaisse = 0;
                montanttrituration = 0; montantachat = 0; montantvente = 0; montantgrignons = 0;montantcaisse = 0;
                var toatalCaisseMon = await _context.Caisses.Where(c => c.Date.DayOfYear == item.DayOfYear).ToListAsync();
               

                foreach(var itema in toatalCaisseMon)
                {
                    montantcaisse = montantcaisse + itema.Montant;
                    totalCaisse++;
                }
                StatiqueWeekTrituration statcaisee = new StatiqueWeekTrituration()
                {
                    Total = totalCaisse,
                    Montant = montantcaisse
                };
                statistiquea.WeekCaisse.Add(statcaisee);
                var gloablFacture = _context.Factures.Include(c => c.Grignon).Include(c => c.Trituration).Include(c => c.Achat).Include(c => c.VenteHuile).Where(c => c.Date.DayOfYear == item.DayOfYear).Include(c => c.TypeFacture).ToList();
                foreach (var itemaw in gloablFacture)
                {
                    if(itemaw.TypeFactureId == 1)
                    {
                        montanttrituration = montanttrituration + itemaw.Montant;
                        totaltrituration = totaltrituration + itemaw.Trituration.Poids;
                    }
                    else if (itemaw.TypeFactureId == 2)
                    {
                        montantachat = montantachat + itemaw.Montant;
                        totalachat = totalachat + itemaw.Achat.QteAchete;
                    }
                    else if (itemaw.TypeFactureId == 3)
                    {
                        montantvente = montantvente + itemaw.Montant;
                        totalvente = totalvente + itemaw.VenteHuile.Qte_Vente;
                    }
                    else if (itemaw.TypeFactureId == 4)
                    {
                        montantgrignons = montantgrignons + itemaw.Montant;
                        totalgrignons = totalgrignons + itemaw.Grignon.Poids;
                    }
                    else
                    {
                        montanttrituration = 0; montantachat = 0; montantvente = 0; montantgrignons = 0; montantcaisse = 0;
                    }


                }
                StatiqueWeekTrituration statrituration = new StatiqueWeekTrituration()
                {
                    Total = totaltrituration,
                    Montant = montanttrituration
                };
                statistiquea.WeekTriturations.Add(statrituration);
                ///////////////////////
                StatiqueWeekTrituration statachat = new StatiqueWeekTrituration()
                {
                    Total = totalachat,
                    Montant = montantachat
                };
                statistiquea.WeekAchats.Add(statachat);
                //////////////////////////
                StatiqueWeekTrituration statvente = new StatiqueWeekTrituration()
                {
                    Total = totalvente,
                    Montant = montantvente
                };
                statistiquea.WeekVentes.Add(statvente);
                //////////////////////////
                StatiqueWeekTrituration statgrignons = new StatiqueWeekTrituration()
                {
                    Total = totalgrignons,
                    Montant = montantgrignons
                };
                statistiquea.WeekGrignons.Add(statgrignons);

            }
            var staLocal = statistiquea;
            statistiquea = null;
            return staLocal;
            
        



        }

        public async Task<Statistique> GetAllStatistiqueByWeekAsync(TimeByWeek timeWeek)
        {
            int yeara = timeWeek.AnyDayInWeek.Year;
            var timeNows = FirstDateOfWeekISO8601(yeara, GetIso8601WeekOfYear(timeWeek.AnyDayInWeek));
            var weeaka = WeekDays(timeNows);

            foreach (var item in weeaka)
            {
                totaltrituration = 0; totalachat = 0; totalvente = 0; totalgrignons = 0; totalCaisse = 0;
                montanttrituration = 0; montantachat = 0; montantvente = 0; montantgrignons = 0; montantcaisse = 0;
                var gloablFacture = await _context.Factures.Include(c => c.Grignon).Include(c => c.Trituration)
                    .Include(c => c.Achat).Include(c => c.VenteHuile).Where(c => c.Date.DayOfYear == item.DayOfYear)
                    .Include(c => c.TypeFacture).ToListAsync();
                var toatalCaisseMon = await _context.Caisses.Where(c => c.Date.DayOfYear == item.DayOfYear).ToListAsync();


                foreach (var itemaa in toatalCaisseMon)
                {
                    montantcaisse = montantcaisse + itemaa.Montant;
                    statistiqueb.MontantCaiseTotal = statistiqueb.MontantCaiseTotal + itemaa.Montant;
                    statistiqueb.NbrCaisseTotal++;
                    totalCaisse++;
                }
                StatiqueWeekTrituration statcaisee = new StatiqueWeekTrituration()
                {
                    Total = totalCaisse,
                    Montant = montantcaisse
                };
                statistiqueb.WeekCaisse.Add(statcaisee);
                foreach (var itema in gloablFacture)
                {
                    if (itema.TypeFactureId == 1)
                    {
                        montanttrituration = montanttrituration + itema.Montant;
                        statistiqueb.MontantTriturationTotal = statistiqueb.MontantTriturationTotal + itema.Montant;
                        statistiqueb.NbrTriturationTotal = statistiqueb.NbrTriturationTotal + itema.Trituration.Poids;
                         totaltrituration = statistiqueb.NbrTriturationTotal;
                    }
                    else if (itema.TypeFactureId == 2)
                    {
                        montantachat = montantachat + itema.Montant;
                        statistiqueb.MontantAchatTotal = statistiqueb.MontantAchatTotal+ itema.Montant;
                        statistiqueb.NbrAchatTotal = statistiqueb.NbrAchatTotal + itema.Achat.QteAchete; ;
                        totalachat = statistiqueb.NbrAchatTotal;
                    }
                    else if (itema.TypeFactureId == 3)
                    {
                        montantvente = montantvente + itema.Montant;
                        statistiqueb.MontantVenteTotal = statistiqueb.MontantVenteTotal + itema.Montant;
                        statistiqueb.NbrVenteTotal = statistiqueb.NbrVenteTotal + itema.VenteHuile.Qte_Vente;
                        totalvente = statistiqueb.NbrVenteTotal;
                    }
                    else if (itema.TypeFactureId == 4)
                    {
                        montantgrignons = montantgrignons + itema.Montant;
                        statistiqueb.MontantGrigonsTotal = statistiqueb.MontantGrigonsTotal + itema.Montant;
                        statistiqueb.NbrGrigonsTotal = statistiqueb.NbrGrigonsTotal + itema.Grignon.Poids;
                        totalgrignons = statistiqueb.NbrGrigonsTotal;
                    }


                }
                StatiqueWeekTrituration statrituration = new StatiqueWeekTrituration()
                {
                    Total = totaltrituration,
                    Montant = montanttrituration
                };
                statistiqueb.WeekTriturations.Add(statrituration);
                ///////////////////////
                StatiqueWeekTrituration statachat = new StatiqueWeekTrituration()
                {
                    Total = totalachat,
                    Montant = montantachat
                };
                statistiqueb.WeekAchats.Add(statachat);
                //////////////////////////
                StatiqueWeekTrituration statvente = new StatiqueWeekTrituration()
                {
                    Total = totalvente,
                    Montant = montantvente
                };
                statistiqueb.WeekVentes.Add(statvente);
                //////////////////////////
                StatiqueWeekTrituration statgrignons = new StatiqueWeekTrituration()
                {
                    Total = totalgrignons,
                    Montant = montantgrignons
                };
                statistiqueb.WeekGrignons.Add(statgrignons);

            }
            var staLocal = statistiqueb;
            statistiqueb = null;
            return staLocal;





        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();

                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static DateTime[] WeekDays(DateTime dateTime)
        {
            return Enumerable.Range(0, 7).Select(num => dateTime.AddDays(num)).ToArray();
        }
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;

            if (firstWeek == 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);

            return result.AddDays(-3);
        }
    }
   

}



