using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IReglementService : IDisposable
    {
        Task<Reglement> GetRegelementByIdAsync(int id);
        Task<IEnumerable<Reglement>> GetReglementByClient(int id);
        Task<IEnumerable<Reglement>> GetReglementsAsync();
        Task InsertReglementAsync(Reglement reglement);
        Task SaveAsync();
    }
}