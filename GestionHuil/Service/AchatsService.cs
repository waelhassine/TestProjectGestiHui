using AutoMapper;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class AchatsService : IAchatsService , IDisposable
    {
        private readonly DataContext _context;
        private bool disposed = false;
        private Achat achat;
        private Achat achatUpdateOne;
        private ICollection<Achat> achatsUpdateOne;
        private ICollection<Achat> achatsUpdateTwo;
        private Trituration triturationUpdate;
        private Trituration triturationDelete;
        private ProduitHuille produitHuile;
        private ProduitHuille produitHuileDelete;
        private ProduitHuille produitHuilleUpdate;
        private ProduitHuille produitHuilleTwo;
        private ICollection<Achat> achats;
        private ICollection<Achat> achatsDelete;
        int AllHuile = 0;
        float AllPrixUnitaire = 0;
        float sommeAllAchat = 0;
        int AllHuileUpdate = 0;
        float AllPrixUnitaireUpdate = 0;
        float sommeAllAchatUpdate = 0;
        int AllHuileUpdateOne = 0;
        float AllPrixUnitaireUpdateOne = 0;
        float sommeAllAchatUpdateOne = 0;
        int AllHuileUpdateTwo = 0;
        float AllPrixUnitaireUpdateTwo = 0;
        float sommeAllAchatUpdateTwo = 0;
        //   private ITemplateAchatGenerator _templateGenerator;
        private IMapper _mapper;
        public AchatsService(DataContext context, IMapper mapper)
        {
            _context = context;
         //   _templateGenerator = templateGenerator;
            _mapper = mapper;
            achat = new Achat();
            achatUpdateOne = new Achat();
            achatsUpdateOne = new Collection<Achat>();
            achatsUpdateTwo = new Collection<Achat>();
            triturationUpdate = new Trituration();
            triturationDelete = new Trituration();
            produitHuile = new ProduitHuille();
            produitHuileDelete = new ProduitHuille();
            achats = new Collection<Achat>();
            achatsDelete = new Collection<Achat>();
            produitHuilleUpdate = new ProduitHuille();
            produitHuilleTwo = new ProduitHuille();
        }
        public async Task<IEnumerable<Achat>> GetStockageAchatsAsync()
        {
            return await _context.Achats.Include(c => c.Trituration.Client).Include(c => c.Trituration.Variete).ToListAsync();
        }
        public async Task<Achat> GetAchatByIdAsync(int id)
        {
            return await _context.Achats.SingleOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Achat>> GetSearchAchatAsync(Search search)
        {
            return await _context.Achats.Include(c => c.Trituration.Client).Include(c => c.Trituration.Variete).Where(f => f.Trituration.Date >= search.Fromdo).Where(f => f.Trituration.Date <= search.Fromto).ToListAsync();
        }

        public async Task<IEnumerable<TriturationAchatRessourcecs>> GetTrituration()
        {
            var triturations = await _context.Triturations.Include(c => c.Client).Include(c => c.Variete).Where(c => c.HuileRestante > 0).ToListAsync();
            return _mapper.Map<IEnumerable<TriturationAchatRessourcecs>>(triturations);
        }
        public async Task<Trituration> GeTriturationByIdAchat(int id)
        {
            return await _context.Triturations.SingleOrDefaultAsync(i => i.Id == id);
        }
        public async Task InsertAchatAsync(Achat achat)
        {
             _context.Achats.Add(achat);
             _context.SaveChanges();
            triturationUpdate = null;
            triturationUpdate = await _context.Triturations.SingleOrDefaultAsync(c => c.Id == achat.TriturationId);
            triturationUpdate.HuileRestante = triturationUpdate.HuileRestante - achat.QteAchete;
            _context.Entry(triturationUpdate).State = EntityState.Modified;

            AllHuile = 0;  AllPrixUnitaire = 0;  sommeAllAchat = 0;
            achats.Clear();
            achats = await _context.Achats.AsNoTracking().Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
            foreach (var achato in achats)
            {
                AllHuile = AllHuile + achato.QteAchete;
                sommeAllAchat = sommeAllAchat + achato.MontantAchat;
            }
            AllPrixUnitaire = sommeAllAchat / AllHuile;
            produitHuile = null;
            produitHuile = await _context.ProduitHuilles.AsNoTracking().SingleOrDefaultAsync(c => c.VarieteId == achat.Trituration.VarieteId);


            produitHuile.Qte_En_Stock = produitHuile.Qte_En_Stock + achat.QteAchete;
            produitHuile.Prix_unitaire = AllPrixUnitaire;
            _context.ProduitHuilles.Attach(produitHuile);
            _context.Entry(produitHuile).State = EntityState.Modified;
             await SaveAsync();
            
        }

        public async Task DeleteAchatAsync(Achat achat)
        {
            // var achata = await _context.Achats.AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);
            // UpdateTriturationBeforeDeleteAchat(achata);
            // UpdateStockageOliveBeforeDeleteAchat(achata);
            triturationDelete = null;
            triturationDelete = await _context.Triturations.SingleOrDefaultAsync(c => c.Id == achat.TriturationId);
            triturationDelete.HuileRestante = triturationDelete.HuileRestante + achat.QteAchete;
            _context.Entry(triturationDelete).State = EntityState.Modified;
            _context.Achats.Remove(achat);
            _context.SaveChanges();
            
             AllHuileUpdate = 0;   AllPrixUnitaireUpdate = 0;  sommeAllAchatUpdate = 0;
            achatsDelete.Clear(); produitHuileDelete = null;
            achatsDelete = await _context.Achats.Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
            produitHuileDelete = await _context.ProduitHuilles.SingleOrDefaultAsync(c => c.VarieteId == achat.Trituration.VarieteId);
            if (achatsDelete != null || achatsDelete.Count != 0)
            {
                foreach (var achato in achatsDelete)
                {
                    AllHuileUpdate = AllHuileUpdate + achato.QteAchete;
                    sommeAllAchatUpdate = sommeAllAchatUpdate + achato.MontantAchat;
                }
                AllPrixUnitaireUpdate = sommeAllAchatUpdate / AllHuileUpdate;
                if (sommeAllAchatUpdate == 0)
                {
                    produitHuileDelete.Qte_En_Stock = 0;
                    produitHuileDelete.Prix_unitaire = 0;
                    _context.Entry(produitHuileDelete).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    produitHuileDelete.Qte_En_Stock = produitHuileDelete.Qte_En_Stock - achat.QteAchete;
                    produitHuileDelete.Prix_unitaire = AllPrixUnitaireUpdate;
                    _context.Entry(produitHuileDelete).State = EntityState.Modified;
                    _context.SaveChanges();

                }
                
            }
            

            await SaveAsync();
        }

        public async Task UpdateAchatAsync(Achat achat , Trituration triturationUpdate)
        {
            AllHuileUpdateOne = 0;  AllPrixUnitaireUpdateOne = 0; sommeAllAchatUpdateOne = 0;

            achatUpdateOne = await _context.Achats.AsNoTracking().SingleOrDefaultAsync(i => i.Id == achat.Id);
            triturationUpdate.HuileRestante = triturationUpdate.HuileRestante + achatUpdateOne.QteAchete;

            if (triturationUpdate.HuileRestante >= achat.QteAchete)
            {

                triturationUpdate.HuileRestante = triturationUpdate.HuileRestante - achat.QteAchete;
                _context.Entry(triturationUpdate).State = EntityState.Modified;
                _context.Entry(achat).State = EntityState.Modified;
                _context.SaveChanges();
                achatsUpdateOne.Clear();
                achatsUpdateOne = await _context.Achats.Include(c => c.Trituration).Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
                produitHuilleUpdate = null;
                produitHuilleUpdate = await _context.ProduitHuilles.SingleOrDefaultAsync(c => c.VarieteId == achat.Trituration.VarieteId);
                produitHuilleUpdate.Qte_En_Stock = produitHuilleUpdate.Qte_En_Stock - achatUpdateOne.QteAchete;
                produitHuilleUpdate.Qte_En_Stock = produitHuilleUpdate.Qte_En_Stock + achat.QteAchete;
                foreach (var achato in achatsUpdateOne)
                {
                    AllHuileUpdateOne = AllHuileUpdateOne + achato.QteAchete;
                    sommeAllAchatUpdateOne = sommeAllAchatUpdateOne + achato.MontantAchat;
                }
                AllPrixUnitaireUpdateOne = sommeAllAchatUpdateOne / AllHuileUpdateOne;
                produitHuilleUpdate.Prix_unitaire = AllPrixUnitaireUpdateOne;
                _context.Entry(produitHuilleUpdate).State = EntityState.Modified;
                _context.SaveChanges();

            }
            await SaveAsync();
        }
        public async void UpdateStokageOlivieAferEdit(Achat achat , Achat achatBase)
        {

            await SaveAsync();
        }

        public async void UpdateTriturationBeforeDeleteAchat(Achat achat)
        {
            var triturationUpdate = await _context.Triturations.SingleOrDefaultAsync(c => c.Id == achat.TriturationId);
            triturationUpdate.HuileRestante = triturationUpdate.HuileRestante + achat.QteAchete;
            _context.Entry(triturationUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async void UpdateStockageOliveBeforeDeleteAchat(Achat achat)
        {
             AllHuileUpdateTwo = 0; AllPrixUnitaireUpdateTwo = 0; sommeAllAchatUpdateTwo = 0;
            achatsUpdateTwo.Clear();
            achatsUpdateTwo = await _context.Achats.AsNoTracking().Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
            produitHuilleTwo = null;
            produitHuilleTwo = await _context.ProduitHuilles.SingleOrDefaultAsync(c => c.VarieteId == achat.Trituration.VarieteId);
            if (achatsUpdateTwo != null || achatsUpdateTwo.Count != 0)
            {
                foreach (var achato in achatsUpdateTwo)
                {
                    AllHuileUpdateTwo = AllHuileUpdateTwo + achato.QteAchete;
                    sommeAllAchatUpdateTwo = sommeAllAchatUpdateTwo + achato.MontantAchat;
                }
                if (sommeAllAchatUpdateTwo == 0)
                {
                    produitHuilleTwo.Qte_En_Stock = 0;
                    produitHuilleTwo.Prix_unitaire = 0;
                    _context.Entry(produitHuilleTwo).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                AllPrixUnitaireUpdateTwo = sommeAllAchatUpdateTwo / AllHuileUpdateTwo;
                produitHuilleTwo.Qte_En_Stock = produitHuilleTwo.Qte_En_Stock - achat.QteAchete;
                produitHuilleTwo.Prix_unitaire = AllPrixUnitaireUpdateTwo;
                _context.Entry(produitHuilleTwo).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public bool AchatExists(int id)
        {
            return _context.Achats.Any(e => e.Id == id);
        }
        public bool TriturationExists(int id)
        {
            return _context.Achats.Any(e => e.TriturationId == id);
        }
        //public async Task<FileDto> Createpdf(int id)
        //{
        //    var achat = await GetAchatByIdAsync(id);
        //    if (achat == null)
        //    {
        //        return null;
        //    }
        //    var file = _templateGenerator.GetUsersAsPdfAsync(achat);
        //    return file;
        //}
        public bool AchatHuileExitsFacture(int id)
        {
            return _context.Factures.Any(e => e.AchatId == id);
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
    }
}
