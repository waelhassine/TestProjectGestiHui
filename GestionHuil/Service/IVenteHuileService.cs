using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IVenteHuileService : IDisposable
    {
        bool CheckQantityHuileInf(VenteHuile venteHuile);
        bool CheckQantityHuileUpdateInf(VenteHuile venteHuile);
        bool VenteHuileExists(int id);
        bool VenteHuileFacture(int id);
        Task DeletVenteHuileAsync(int id);
        Task<IEnumerable<Client>> GetSClientsAsync();
        Task<IEnumerable<VenteHuile>> GetSerchVenteHuileAsync(Search search);
        Task<IEnumerable<Variete>> GetVarieteAsync();
        Task<IEnumerable<VenteHuile>> GetVenteHuileAsync();
        Task<VenteHuile> GetVenteHuileByIdAsync(int id);
        Task InsertVenteHuileAsync(VenteHuile venteHuile);
        Task SaveAsync();
        Task UpdateVenteHuileAsync(VenteHuile venteHuile);
    }
}