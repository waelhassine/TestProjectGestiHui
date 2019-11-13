using System;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IStatistiqueService : IDisposable
    {
        Task<Statistique> GetAllStatistiqueAsync();
        Task<Statistique> GetAllStatistiqueByWeekAsync(TimeByWeek timeWeek);
    }
}