using GestionHuil.Controllers.Ressources;
using GestionHuil.Data;
using GestionHuil.Models;
using GestionHuil.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class FactureService : IDisposable, IFactureService
    {

        private readonly DataContext _context;
        private ITemplateGeneratorAll _templateGenerator;
        List<FactureRessource> NewListFacture;
        float s=0;

        public FactureService(DataContext context , ITemplateGeneratorAll templateGeneratorAll)
        {
            _context = context;
            _templateGenerator = templateGeneratorAll;
            NewListFacture = new List<FactureRessource>();
        }
        public async Task<IEnumerable<Facture>> GetFactureAsync()
        {
            return await _context.Factures.Include(c => c.Reglements).Include(c => c.TypeFacture).ToListAsync();
        }

        public async Task<Facture> GetFactureByIdAsync(int id)
        {
            return await _context.Factures.FindAsync(id);
        }
        public async Task<IEnumerable<FactureRessource>> GetFactureByIdClientAsync(int id)
        {
            NewListFacture.Clear();
            var facture = await _context.Factures.Include(c => c.Reglements).Include(c => c.TypeFacture).Where(c => c.ClientId == id).ToListAsync();

            foreach (var fac in facture)
            {
                float s = 0;
                foreach (var reg in fac.Reglements)
                {
                    s = s + reg.Montant;
                }
                NewListFacture.Add(new FactureRessource
                {
                    Id = fac.Id,
                    Montant = fac.Montant,
                    Date = fac.Date,
                    TypeFacture = fac.TypeFacture,
                    TypeFactureId = fac.TypeFactureId,
                    RestApayer = fac.Montant - s
                });
            }
            return NewListFacture;
        }
 
        public async Task InsertFactureAsync(Facture facture)
        {
            _context.Factures.Add(facture);
            await SaveAsync();
        }

        public async Task DeleteFactureAsync(Facture facture)
        {
            _context.Factures.Remove(facture);
            await SaveAsync();

        }

        public async Task<FileDto> Createpdf(int id)
        {
            
            var facture = await _context.Factures.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
            var rega = await _context.Reglements.AsNoTracking().Where(c => c.FactureId == facture.Id).ToListAsync(); 
            s = 0;
            foreach (var reg in rega)
            {
                s = s + reg.Montant;
            }
            if (facture.TypeFactureId == 1)
            {
                
                var test = await _context.Triturations.Where(c => c.Id == facture.TriturationId)
                .Include(c => c.Client).Include(c => c.Variete).SingleAsync();
                var file = _templateGenerator.GetUsersAsPdfAsync(test , s);
                return file;

            }
            else if(facture.TypeFactureId == 2)
            {
                var test = await _context.Achats.Include(c => c.Trituration.Client).Include(c => c.Trituration.Variete).SingleOrDefaultAsync(i => i.Id == facture.AchatId);
                var file = _templateGenerator.GetUsersAsPdfAsync(test , s);
                return file;
            }
            else if (facture.TypeFactureId == 3)
            {
                var test = await _context.VenteHuiles.Include(c => c.Client).Include(c => c.Variete).SingleOrDefaultAsync(i => i.Id == facture.VenteHuileId);
                var file = _templateGenerator.GetUsersAsPdfAsync(test ,s);
                return file;
            }
            else if (facture.TypeFactureId == 4)
            {
                var test = await _context.Grignons.Include(c => c.Client).SingleOrDefaultAsync(i => i.Id == facture.GrignonId);
                var file = _templateGenerator.GetUsersAsPdfAsync(test, s);
                return file;
            }
            else
            {
                return null;
            }
          


        }
        public bool FactureExists(int id)
        {
            return _context.Factures.Any(e => e.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

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
