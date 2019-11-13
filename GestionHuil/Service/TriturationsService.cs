using AutoMapper;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class TriturationsService : IDisposable, ITriturationsService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        public Trituration triturationNew;
        public StockageOlive stockageOlivess;
        //  private ITemplateGenerator _templateGenerator;
        private IMapper _mapper;
        public TriturationsService(DataContext context, IMapper mapper)
        {
            _context = context;
        //    _templateGenerator = templateGenerator;
            _mapper = mapper;
            triturationNew = new Trituration();
            stockageOlivess = new StockageOlive();
        }
        public async Task<IEnumerable<Trituration>> GetStockageTriturationAsync()
        {
            return await _context.Triturations.Include(c => c.Client).Include(c => c.Variete).ToListAsync();
        }
        public async Task<IEnumerable<StockageOliveRessource>> GetStockageOliveNew(int id)
        {
            var stocknewclient = await _context.StockageOlives.AsNoTracking().Include(c => c.Client).Include(c => c.Variete).Where(s => s.ClientId == id).Where(c => c.TriturationId == null).ToListAsync();
            return _mapper.Map<List<StockageOlive>, List<StockageOliveRessource>>(stocknewclient);
        }
        public async Task<Trituration> GetTriturationByIdAsync(int id)
        {
            return await _context.Triturations.Where(c => c.Id == id)
                .Include(c => c.Client).Include(c => c.Variete).SingleAsync();
        }
        public async Task<IEnumerable<Trituration>> GetSerchTriturationAsync(Search search)
        {
            return await _context.Triturations.Include(c => c.StockageOlives).Include(c => c.Client).Include(c => c.Variete)
                         .Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
        }
        public async Task InsertTriturationAsync(Trituration trituration)
        {

            triturationNew.Date = trituration.Date;
            triturationNew.PrixUnitaire = trituration.PrixUnitaire;
            triturationNew.Montant = trituration.Montant;
            triturationNew.Rendement = trituration.Rendement;
            triturationNew.HuileObtenu = trituration.HuileObtenu;
            triturationNew.QteLivree = trituration.QteLivree;
            triturationNew.HuileRestante = trituration.HuileRestante;
            triturationNew.Poids = trituration.Poids;
            triturationNew.ClientId = trituration.ClientId;
            triturationNew.VarieteId = trituration.VarieteId;

            _context.Triturations.Add(triturationNew);
            _context.SaveChanges();
            // var stockolivesDb = _context.StockageOlives.Where(c => c.TriturationId == trituration.Id)
            foreach (var stock in trituration.StockageOlives)
            {
                StockageOlive stok = new StockageOlive()
                {
                    Id = stock.Id,
                    Poids = stock.Poids,
                    Chauffeur = stock.Chauffeur,
                    Vehicule = stock.Vehicule,
                    Date = stock.Date,
                    VarieteId = stock.VarieteId,
                    ClientId = stock.ClientId,
                    TriturationId = triturationNew.Id,


                };
                _context.Entry(stok).State = EntityState.Modified;
                _context.SaveChanges();
            }
            await SaveAsync();
            triturationNew = null;
        }
        public async Task DeletTriturationsAsync(Trituration trituration)
        {
            var stockolives = await _context.StockageOlives.Where(c => c.TriturationId == trituration.Id).ToListAsync();
            foreach (var stock in trituration.StockageOlives)
            {
                stock.TriturationId = null;
                _context.StockageOlives.Update(stock);
            }
            _context.Triturations.Remove(trituration);
            await SaveAsync();
        }
        public async Task UpdateTriturationsAsync(Trituration trituration)
        {
            _context.Entry(trituration).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        //public async Task<FileDto> Createpdf(int id)
        //{
        //    var trituration = await GetTriturationByIdAsync(id);
        //    if (trituration == null)
        //    {
        //        return null;
        //    }
        //    var file =  _templateGenerator.GetUsersAsPdfAsync(trituration);
        //    return file;
        //}
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
        public bool TriturationExists(int id)
        {
            return _context.Triturations.Any(e => e.Id == id);
        }
        public bool TriturationExitsFacture(int id)
        {
            return   _context.Factures.Any(e => e.TriturationId == id);
            

        }
        public bool TriturationExitsAchat(int id)
        {

           return _context.Achats.Any(e => e.TriturationId == id);
        

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
