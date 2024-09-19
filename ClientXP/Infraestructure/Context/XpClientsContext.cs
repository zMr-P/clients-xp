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
                    CPF = "412.711.520-37",
                }, new Client
                {
                    Id = 2,
                    Name = "Thiago",
                    Email = "THIAGO.BORGES@XPI.COM.BR",
                    CPF = "195.636.090-50"
                }, new Client
                {
                    Id = 3,
                    Name = "DevelopersTeam",
                    Email = "DEVELOPERS.TEAM@XPI.COM.BR",
                    CPF = "009.094.570-00"
                });
        }
    }
}
