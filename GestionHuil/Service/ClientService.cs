using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Service
{
    public class ClientService : IClientService , IDisposable
    {
        private readonly DataContext _context;

        public ClientService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.Where(c => c.Id.Equals(id)).DefaultIfEmpty(new Client()).SingleAsync();
        }

        public async Task InsertClientAsync(Client client)
        {
             _context.Clients.Add(client);
            await SaveAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
           _context.Clients.Remove(client);
            await SaveAsync();

        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await SaveAsync();
        }

        public  bool ClientExists(int id)
        {
            return  _context.Clients.Any(e => e.Id == id);
        }
        public bool ClientExistByNumber(string tel)
        {
            return _context.Clients.Any(e => e.Tel == tel);
        }

        public async Task SaveAsync()
        {
         await  _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
