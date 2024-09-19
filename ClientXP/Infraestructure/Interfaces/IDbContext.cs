using ClientXP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientXP.Infraestructure.Interfaces
{
    public interface IDbContext
    {
        DbSet<Client> Clients { get; }
        Task<int> SaveChangesAsync();
    }
}
