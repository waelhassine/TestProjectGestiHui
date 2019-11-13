using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class CaisseService : IDisposable, ICaisseService
    {
        private readonly DataContext _context;

        public CaisseService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Caisse>> GetCaissesAsync()
        {
            return await _context.Caisses.ToListAsync();
        }

        public async Task<Caisse> GetCaissesByIdAsync(int id)
        {
            return await _context.Caisses.Where(c => c.Id.Equals(id)).DefaultIfEmpty(new Caisse()).SingleAsync();
        }
        public async Task<IEnumerable<Caisse>> GetSearchAchatAsync(Search search)
        {
            return await _context.Caisses.Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
        }

        public async Task InsertCaissesAsync(Caisse caisse)
        {
            _context.Caisses.Add(caisse);
            await SaveAsync();
        }

        public async Task DeleteCaissesAsync(Caisse caisse)
        {
            _context.Caisses.Remove(caisse);
            await SaveAsync();

        }

        public async Task UpdateCaissesAsync(Caisse caisse)
        {
            _context.Entry(caisse).State = EntityState.Modified;
            await SaveAsync();
        }

        public bool CaissesExists(int id)
        {
            return _context.Caisses.Any(e => e.Id == id);
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
