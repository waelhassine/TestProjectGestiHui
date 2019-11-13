using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IAchatsService : IDisposable
    {
      //  Task<FileDto> Createpdf(int id);
        Task DeleteAchatAsync(Achat achat);
        Task<Achat> GetAchatByIdAsync(int id);
        Task<IEnumerable<Achat>> GetSearchAchatAsync(Search search);
        Task<IEnumerable<Achat>> GetStockageAchatsAsync();
        Task<IEnumerable<TriturationAchatRessourcecs>> GetTrituration();
        Task InsertAchatAsync(Achat achat);
        Task SaveAsync();
        Task UpdateAchatAsync(Achat achat, Trituration triturationUpdate);
      //  void UpdateStockageOliveAfterAddAchat(Achat achat);
        void UpdateStockageOliveBeforeDeleteAchat(Achat achat);
        void UpdateStokageOlivieAferEdit(Achat achat, Achat achatBase);
     //   void UpdateTriturationAfterAddAchat(Achat achat);
        void UpdateTriturationBeforeDeleteAchat(Achat achat);
        bool AchatExists(int id);
        bool AchatHuileExitsFacture(int id);
        Task<Trituration> GeTriturationByIdAchat(int id);
    }
}