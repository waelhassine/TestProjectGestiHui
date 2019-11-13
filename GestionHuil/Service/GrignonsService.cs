using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class GrignonsService : IDisposable, IGrignonsService
    {
        private readonly DataContext _context;

    public GrignonsService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Grignon>> GetGrignonsAsync()
    {
        return await _context.Grignons.Include(c => c.Client).ToListAsync();
    }
    public async Task<Grignon> GetGrignonsByIdAsync(int id)
    {
            return await _context.Grignons.Include(c => c.Client).Where(c => c.Id.Equals(id)).SingleAsync();
    }
    public async Task<IEnumerable<Grignon>> GetSerchVenteHuileAsync(Search search)
    {
            return await _context.Grignons.Include(c => c.Client)
                         .Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
    }
        public async Task InsertGrignonsAsync(Grignon grignon)
        {
            _context.Grignons.Add(grignon);
            await SaveAsync();
        }

        public async Task DeleteGrignonsAsync(Grignon grignon)
        {
            _context.Grignons.Remove(grignon);
            await SaveAsync();

        }

        public async Task UpdateGrignonsAsync(Grignon grignon)
        {
            _context.Entry(grignon).State = EntityState.Modified;
            await SaveAsync();
        }

        public bool GrignonsExists(int id)
        {
            return _context.Grignons.Any(e => e.Id == id);
        }
        public bool GrigonsFacture(int id)
        {
            return _context.Factures.Any(e => e.GrignonId == id);
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
