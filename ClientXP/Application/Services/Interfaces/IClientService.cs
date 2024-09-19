using ClientXP.Application.Models;
using ClientXP.Domain.Entities;

namespace ClientXP.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetByIdAsync(int id);
        Task CreateAsync(ClientModel clModel);
        Task UpdateAsync(Client client, ClientModel clModel);
        Task UpdateEmailAsync(Client client, string email);
        Task DeleteAsync(Client client);
    }
}
