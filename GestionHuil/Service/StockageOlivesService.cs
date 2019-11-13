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
    public class StockageOlivesService : IDisposable, IStockageOlivesService
    {
        private readonly DataContext _context;
        private bool disposed = false;
        private ITemplateStockageOlivesGeneratorcs _templateGenerator;
        public StockageOlivesService(DataContext context , ITemplateStockageOlivesGeneratorcs templateGenerator)
        {
            _context = context;
            _templateGenerator = templateGenerator;
        }
        public async Task<IEnumerable<StockageOlive>> GetStockageOliveAsync()
        {
            return await _context.StockageOlives.AsNoTracking().Include(c => c.Client).Include(c => c.Variete).ToListAsync();
        }
        public async Task<IEnumerable<Variete>> GetAllVarietesAsync()
        {
            return  await _context.Varietes.ToListAsync();
        }
        public async Task<IEnumerable<StockageOlive>> GetSerchStockageOliveAsync(Search search)
        {
            return await _context.StockageOlives.Include(c => c.Client).Include(c => c.Variete).Where(f => f.Date >= search.Fromdo).Where(f => f.Date <= search.Fromto).ToListAsync();
        }
        public async Task<StockageOlive> GetStockOlivesByIdAsync(int id)
        {
            return await _context.StockageOlives.Where(c => c.Id.Equals(id))
            .DefaultIfEmpty(new StockageOlive()).Include(c => c.Client).Include(c => c.Variete).SingleAsync();
        }
        public async Task InsertStockOliveAsync(StockageOlive stockageOlive)
        {
            _context.StockageOlives.Add(stockageOlive);
            await SaveAsync();
        }
        public async Task DeleteStockOliveAsync(StockageOlive stockageOlive)
        {
            _context.StockageOlives.Remove(stockageOlive);
            await SaveAsync();
        }
        public async Task UpdateStockageOliveAsync(StockageOlive stockageOlive)
        {
            _context.Entry(stockageOlive).State = EntityState.Modified;
            await SaveAsync();
        }

        public bool StockageOliveExists(int id)
        {
            return _context.StockageOlives.AsNoTracking().Any(e => e.Id == id);
        }
        public bool StockageOliveExistsTrituration(int id)
        {
            var check = _context.StockageOlives.AsNoTracking().Where(e => e.Id == id).SingleOrDefault();

            if (check.TriturationId == null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<FileDto> Createpdf(int id)
        {
            var stockolives = await GetStockOlivesByIdAsync(id);
            if(stockolives == null)
            {
                return null;
            }
            var file = _templateGenerator.GetUsersAsPdfAsync(stockolives);
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
