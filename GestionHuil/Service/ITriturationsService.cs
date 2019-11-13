using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface ITriturationsService : IDisposable
    {
      //  Task<FileDto> Createpdf(int id);
        Task DeletTriturationsAsync(Trituration trituration);
        Task<IEnumerable<Trituration>> GetSerchTriturationAsync(Search search);
        Task<IEnumerable<Trituration>> GetStockageTriturationAsync();
        Task<IEnumerable<StockageOliveRessource>> GetStockageOliveNew(int id);
        Task<Trituration> GetTriturationByIdAsync(int id);
        bool TriturationExists(int id);
        bool TriturationExitsFacture(int id);
        bool TriturationExitsAchat(int id);
        Task InsertTriturationAsync(Trituration trituration);
        Task SaveAsync();
        Task UpdateTriturationsAsync(Trituration trituration);
    }
}