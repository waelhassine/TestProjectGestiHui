using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IFactureService : IDisposable
    {
        Task<FileDto> Createpdf(int id);
        Task DeleteFactureAsync(Facture facture);
        bool FactureExists(int id);
        Task<IEnumerable<Facture>> GetFactureAsync();
        Task<Facture> GetFactureByIdAsync(int id);
        Task<IEnumerable<FactureRessource>> GetFactureByIdClientAsync(int id);
        Task InsertFactureAsync(Facture facture);
        Task SaveAsync();
    }
}