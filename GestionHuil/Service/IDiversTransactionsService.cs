using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IDiversTransactionsService : IDisposable
    {
        Task<FileDto> Createpdf(int id);
        Task DeleteDiversTransactionAsync(DiversTransaction diversTransaction);
        bool DiversTransactionExists(int id);
        Task<DiversTransaction> GetDiversTransactionsByIdAsync(int id);
        Task<IEnumerable<DiversTransaction>> GetDiversTransactionAsync();
        Task<IEnumerable<DiversTransaction>> GetDiversTransactionsByClient(int id);
        Task<IEnumerable<DiversTransaction>> GetSearchDiversTransactionAsync(Search search);
        Task InsertDiversTransactionAsync(DiversTransaction diversTransaction);
        Task SaveAsync();
        Task UpdateDiversTransactionAsync(DiversTransaction diversTransaction);
    }
}