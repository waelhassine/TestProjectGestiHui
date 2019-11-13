using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IStockageOlivesService : IDisposable
    {
        Task<FileDto> Createpdf(int id);
        Task DeleteStockOliveAsync(StockageOlive stockageOlive);
        Task<IEnumerable<StockageOlive>> GetStockageOliveAsync();
        Task<IEnumerable<StockageOlive>> GetSerchStockageOliveAsync(Search search);
        Task<IEnumerable<Variete>> GetAllVarietesAsync();
        Task<StockageOlive> GetStockOlivesByIdAsync(int id);
        Task InsertStockOliveAsync(StockageOlive stockageOlive);
        Task SaveAsync();
        bool StockageOliveExists(int id);
        bool StockageOliveExistsTrituration(int id);
        Task UpdateStockageOliveAsync(StockageOlive stockageOlive);
    }
}