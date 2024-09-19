using ClientXP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientXP.Infraestructure.Context
{
    public class XpClientsContext : DbContext
    {
        public XpClientsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Name = "Paulo",
                    Email = "PAULO.ROBCONC@GMAIL.COM",
                    CPF = "000.111.222-33",
                }, new Client
                {
                    Id = 2,
                    Name = "Thiago",
                    Email = "THIAGO.BORGES@XPI.COM.BR",
                    CPF = "000.111.222-34"
                }, new Client
                {
                    Id = 3,
                    Name = "DevelopersTeam",
                    Email = "DEVELOPERS.TEAM@XPI.COM.BR",
                    CPF = "000.111.222-35"
                });
        }
    }
}
