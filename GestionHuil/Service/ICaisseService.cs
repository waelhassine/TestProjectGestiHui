using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface ICaisseService : IDisposable
    {
        bool CaissesExists(int id);
        Task DeleteCaissesAsync(Caisse caisse);
        Task<IEnumerable<Caisse>> GetCaissesAsync();
        Task<Caisse> GetCaissesByIdAsync(int id);
        Task<IEnumerable<Caisse>> GetSearchAchatAsync(Search search);
        Task InsertCaissesAsync(Caisse caisse);
        Task SaveAsync();
        Task UpdateCaissesAsync(Caisse caisse);
    }
}