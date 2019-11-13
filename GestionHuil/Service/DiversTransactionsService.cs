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
    public class DiversTransactionsService : IDisposable, IDiversTransactionsService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        private ITemplateDiversTransactionGenerator _templateGenerator;
        public DiversTransactionsService(DataContext context, ITemplateDiversTransactionGenerator templateGenerator)
        {
            _context = context;
            _templateGenerator = templateGenerator;
        }
        public async Task<IEnumerable<DiversTransaction>> GetDiversTransactionAsync()
        {
            return await _context.DiversTransactions.ToListAsync();
        }

        public async Task<DiversTransaction> GetDiversTransactionsByIdAsync(int id)
        {
            return await _context.DiversTransactions.FindAsync(id);
        }
        public async Task<IEnumerable<DiversTransaction>> GetDiversTransactionsByClient(int id)
        {
            return await _context.DiversTransactions.Where(c => c.ClientId == id).ToListAsync();
        }
        public async Task<IEnumerable<DiversTransaction>> GetSearchDiversTransactionAsync(Search search)
        {
            return await _context.DiversTransactions.Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
        }

        public async Task InsertDiversTransactionAsync(DiversTransaction diversTransaction)
        {
            _context.DiversTransactions.Add(diversTransaction);
            await SaveAsync();
        }

        public async Task DeleteDiversTransactionAsync(DiversTransaction diversTransaction)
        {
            _context.DiversTransactions.Remove(diversTransaction);
            await SaveAsync();

        }

        public async Task UpdateDiversTransactionAsync(DiversTransaction diversTransaction)
        {
            _context.Entry(diversTransaction).State = EntityState.Modified;
            await SaveAsync();
        }

        public bool DiversTransactionExists(int id)
        {
            return _context.DiversTransactions.Any(e => e.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<FileDto> Createpdf(int id)
        {
            var divers = await _context.DiversTransactions.Include(c => c.Client).SingleOrDefaultAsync(c => c.Id == id);
            if (divers == null)
            {
                return null;
            }
            var file = _templateGenerator.GetUsersAsPdfAsync(divers);
            return file;
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
