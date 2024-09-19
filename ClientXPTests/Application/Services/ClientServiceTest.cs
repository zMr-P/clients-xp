using ClientXP.Application.Services;
using ClientXP.Domain.Entities;

namespace ClientXPTests.Application.Services
{
    public class ClientServiceTest
    {
        private readonly ClientService _service;
        public ClientServiceTest(ClientService service)
        {
            _service = service;
        }

        [Fact]
        public async Task GetAllClientsShouldReturnCorrectData()
        {
            var fakeClients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "test",
                    Email = "Jhon@gmail.com",
                    CPF = "412.711.520-37"
                },
                new Client
                {
                    Id= 2,
                    Name = "test",
                    Email = "jhonzin@gmail.com",
                    CPF = "141.556.258-45"
                }
            };
        }
    }
}
