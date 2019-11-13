using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IGrignonsService : IDisposable
    {
        Task DeleteGrignonsAsync(Grignon grignon);
        Task<IEnumerable<Grignon>> GetGrignonsAsync();
        Task<Grignon> GetGrignonsByIdAsync(int id);
        bool GrignonsExists(int id);
        Task InsertGrignonsAsync(Grignon grignon);
        Task SaveAsync();
        Task UpdateGrignonsAsync(Grignon grignon);
        bool GrigonsFacture(int id);
        Task<IEnumerable<Grignon>> GetSerchVenteHuileAsync(Search search);
    }
}