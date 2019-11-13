using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class ReglementService : IDisposable, IReglementService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        public ReglementService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reglement>> GetReglementsAsync()
        {
            return await _context.Reglements.ToListAsync();
        }
        public async Task<Reglement> GetRegelementByIdAsync(int id)
        {
            return await _context.Reglements.FindAsync(id);
        }
        public async Task<IEnumerable<Reglement>> GetReglementByClient(int id)
        {
            return await _context.Reglements.Where(c => c.FactureId == id).ToListAsync();
        }
        public async Task InsertReglementAsync(Reglement reglement)
        {
            _context.Reglements.Add(reglement);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        private bool ReglementExists(int id)
        {
            return _context.Reglements.Any(e => e.Id == id);
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
