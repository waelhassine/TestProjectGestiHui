using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionHuil.Models;

namespace GestionHuil.Service
{
    public interface IClientService : IDisposable
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task DeleteClientAsync(Client client);
        Task InsertClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task SaveAsync();
        bool ClientExists(int id);
        bool ClientExistByNumber(string tel);
    }
}