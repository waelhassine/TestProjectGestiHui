using AutoMapper;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class VenteHuileService : IDisposable, IVenteHuileService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        private IMapper _mapper;
        private ProduitHuille produitHuilleInsert;
        private ProduitHuille produitHuilleUpdate;
        public VenteHuile venteHuileUpdate;
        private ProduitHuille produitHuilleDelete;
        public VenteHuile venteHuileDelete;
        private ProduitHuille produitHuilleCheck;
        public VenteHuile venteHuileCheck;
        public VenteHuileService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            produitHuilleInsert = new ProduitHuille();
            produitHuilleUpdate = new ProduitHuille();
            venteHuileUpdate = new VenteHuile();
            produitHuilleDelete = new ProduitHuille();
            venteHuileDelete = new VenteHuile();
            produitHuilleCheck = new ProduitHuille();
            venteHuileCheck = new VenteHuile();
        }
        public async Task<IEnumerable<VenteHuile>> GetVenteHuileAsync()
        {
            return await _context.VenteHuiles.Include(c => c.Client).Include(c => c.Variete).ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetSClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }
        public async Task<IEnumerable<Variete>> GetVarieteAsync()
        {
            return await _context.Varietes.ToListAsync();
        }
        public async Task<IEnumerable<VenteHuile>> GetSerchVenteHuileAsync(Search search)
        {
            return await _context.VenteHuiles.Include(c => c.Client).Include(c => c.Variete)
                         .Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
        }
        public async Task<VenteHuile> GetVenteHuileByIdAsync(int id)
        {
            return await _context.VenteHuiles.Where(c => c.Id == id)
                .Include(c => c.Client).Include(c => c.Variete).SingleAsync();
        }
        public async Task InsertVenteHuileAsync(VenteHuile venteHuile)
        {
            produitHuilleInsert = null;
            produitHuilleInsert = await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefaultAsync();
            produitHuilleInsert.Qte_En_Stock = produitHuilleInsert.Qte_En_Stock - venteHuile.Qte_Vente;
            _context.Entry(produitHuilleInsert).State = EntityState.Modified;
            _context.VenteHuiles.Add(venteHuile);
            await SaveAsync();
        }
        public async Task UpdateVenteHuileAsync(VenteHuile venteHuile)
        {
            produitHuilleUpdate = null;
            venteHuileUpdate = null;
            produitHuilleUpdate = await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefaultAsync();
            venteHuileUpdate = await _context.VenteHuiles.AsNoTracking().SingleOrDefaultAsync(c => c.Id == venteHuile.Id);
            produitHuilleUpdate.Qte_En_Stock = produitHuilleUpdate.Qte_En_Stock + venteHuileUpdate.Qte_Vente;
            produitHuilleUpdate.Qte_En_Stock = produitHuilleUpdate.Qte_En_Stock - venteHuile.Qte_Vente;
            _context.Entry(produitHuilleUpdate).State = EntityState.Modified;
            _context.Entry(venteHuile).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task DeletVenteHuileAsync(int id)
        {
            produitHuilleDelete = null;
            venteHuileDelete = null;
            venteHuileDelete = await _context.VenteHuiles.FindAsync(id);
            produitHuilleDelete = await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuileDelete.VarieteId).SingleOrDefaultAsync();
            produitHuilleDelete.Qte_En_Stock = produitHuilleDelete.Qte_En_Stock + venteHuileDelete.Qte_Vente;
            _context.Entry(produitHuilleDelete).State = EntityState.Modified;
            _context.VenteHuiles.Remove(venteHuileDelete);
            await SaveAsync();
        }
        public bool CheckQantityHuileUpdateInf(VenteHuile venteHuile)
        {
            produitHuilleCheck = null; venteHuileCheck = null;
            produitHuilleCheck =  _context.ProduitHuilles.AsNoTracking().Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefault();
            venteHuileCheck =  _context.VenteHuiles.AsNoTracking().SingleOrDefault(c => c.Id == venteHuile.Id);
            produitHuilleCheck.Qte_En_Stock = produitHuilleCheck.Qte_En_Stock + venteHuileCheck.Qte_Vente;
            if (produitHuilleCheck.Qte_En_Stock < venteHuile.Qte_Vente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckQantityHuileInf(VenteHuile venteHuile)
        {
            var stockagehuile =  _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefault();
            if (stockagehuile.Qte_En_Stock < venteHuile.Qte_Vente)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool VenteHuileExists(int id)
        {
            return _context.VenteHuiles.Any(e => e.Id == id);
        }
        public bool VenteHuileFacture(int id)
        {
            return _context.Factures.Any(e => e.VenteHuileId == id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
