using ClientXP.Domain.Entities;
using ClientXP.Domain.Interfaces;
using ClientXP.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ClientXP.Infraestructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly XpClientsContext _context;
        public ClientRepository(XpClientsContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
