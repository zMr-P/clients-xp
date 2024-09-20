using ClientXP.Application.Models;
using ClientXP.Application.Services;
using ClientXP.Domain.Entities;
using ClientXP.Domain.Interfaces;
using FluentValidation;
using Moq;

namespace ClientXPTests.Application.Services
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _mockRepository;
        private readonly Mock<IValidator<Client>> _mockValidator;
        private readonly ClientService _clientService;
        public ClientServiceTest()
        {
            _mockRepository = new Mock<IClientRepository>();
            _mockValidator = new Mock<IValidator<Client>>();
            _clientService = new ClientService(_mockRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task GetAllClientsShouldReturnListClients()
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

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeClients);

            var result = await _clientService.GetAllClientsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("test", result[0].Name);
        }

        [Fact]
        public async Task GetClientByIdShouldReturnEspecificClient()
        {
            var fakeClient = new Client
            {
                Id = 1,
                Name = "test",
                Email = "vouentrarna@xpi.com.br",
                CPF = "145.578.669-56"
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeClient);

            var result = await _clientService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(fakeClient.Id, result.Id);
            Assert.Equal(fakeClient.Email, result.Email);
        }
        [Fact]
        public async Task CreateAsyncValidClientAndAdd()
        {
            var clientModel = new ClientModel
            {
                Name = "test",
                Email = "vouentrarna@xpi.com.br",
                CPF = "412.711.520-37"
            };
            var client = new Client
            {
                Name = clientModel.Name,
                Email = clientModel.Email.ToUpper(),
                CPF = clientModel.CPF,
            };

            _mockValidator.Setup(v => v.Validate(It.IsAny<Client>())).Returns(new FluentValidation.Results.ValidationResult());
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Client>());
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Client>())).Returns(Task.CompletedTask);

            await _clientService.CreateAsync(clientModel);

            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Client>()), Times.Once());
        }
        [Fact]
        public async Task CreateAsyncDuplicateCPFThrowsArgumentException()
        {
            var clientModel = new ClientModel
            {
                Name = "Client",
                Email = "client@sdsd.com",
                CPF = "192.168.000-51"
            };
            var existingClient = new Client
            {
                Name = "jujuba",
                Email = "queroser@xpi.com.br",
                CPF = clientModel.CPF
            };

            _mockValidator.Setup(v => v.Validate(It.IsAny<Client>())).Returns(new FluentValidation.Results.ValidationResult());
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Client> { existingClient });

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _clientService.CreateAsync(clientModel));
            Assert.Equal("O CPF já está cadastrado", exception.Message);
        }

        [Fact]
        public async Task HaveToUpdateClientAsyncAndSuccess()
        {
            var clientModel = new ClientModel
            {
                Name = "Paulo",
                Email = "paulo.souza@xpi.com.br",
                CPF = "412.711.520-37"
            };

            var client = new Client
            {
                Id = 1,
                Name = clientModel.Name,
                Email = "paulo.robconc@gmail.com",
                CPF = clientModel.CPF
            };

            _mockValidator.Setup(v => v.Validate(It.IsAny<Client>())).Returns(new FluentValidation.Results.ValidationResult());
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Client> { client });

            await _clientService.UpdateAsync(client, clientModel);

            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Client>(c =>
            c.Name == clientModel.Name &&
            c.Email == clientModel.Email &&
            c.CPF == clientModel.CPF)), Times.Once);
        }

        [Fact]
        public async Task HaveToUpdateEmailAsyncAndReturnSuccess()
        {
            var clientModel = new ClientEmailModel
            {
                Id = 1,
                Email = "idonein@xpi.com.br"
            };

            var client = new Client
            {
                Id = 1,
                Name = "Erick",
                Email = "paulo.robconc@gmail.com",
                CPF = "412.711.520-37"
            };

            _mockValidator.Setup(v => v.Validate(It.IsAny<Client>())).Returns(new FluentValidation.Results.ValidationResult());

            await _clientService.UpdateEmailAsync(client, clientModel.Email);

            Assert.Equal(clientModel.Email, client.Email);

            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Client>(c =>
            c.Id == client.Id &&
            c.Email == clientModel.Email)), Times.Once);
        }
        [Fact]
        public async Task HaveToDeleteClientByIdAndReturnSuccess()
        {
            var mockClient = new Client
            {
                Id = 1,
                Name = "Paulo",
                Email = "paulo.robconc@xpi.com.br",
                CPF = "412.711.520-37"
            };
            _mockRepository.Setup(repo => repo.DeleteAsync(mockClient)).Returns(Task.CompletedTask);

            await _clientService.DeleteAsync(mockClient);

            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Client>(c =>
                c.Id == mockClient.Id &&
                c.Name == mockClient.Name &&
                c.Email == mockClient.Email)), Times.Once);
        }
    }
}