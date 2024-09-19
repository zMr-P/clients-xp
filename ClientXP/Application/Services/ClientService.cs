using ClientXP.Application.Models;
using ClientXP.Application.Services.Interfaces;
using ClientXP.Domain.Entities;
using ClientXP.Domain.Interfaces;
using ClientXP.Infraestructure.Context;
using FluentValidation;

namespace ClientXP.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IValidator<Client> _validator;

        public ClientService(IClientRepository repository, IValidator<Client> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            var dataClients = await _repository.GetAllAsync();

            return dataClients;
        }
        public Task<Client> GetByIdAsync(int id)
        {
            var dataClient = _context.Clients
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return Task.FromResult(dataClient);
        }
        public async Task CreateAsync(ClientModel clModel)
        {
            var client = new Client
            {
                Name = clModel.Name,
                Email = clModel.Email.ToUpper(),
                CPF = clModel.CPF
            };

            var validationResult = _validator.Validate(client);
            if (!validationResult.IsValid)
            {
                var errors = String.Join("; ",
                    validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            var dataClients = await GetAllClientsAsync();
            if (dataClients != null && dataClients.Any())
            {
                foreach (var dataClient in dataClients)
                {
                    if (dataClient.CPF.Equals(client.CPF))
                    {
                        throw new ArgumentException("O CPF ja está cadastrado");
                    }
                }
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Client client, ClientModel clModel)
        {
            if (!client.CPF.Equals(clModel.CPF))
            {
                var dataClients = await GetAllClientsAsync();
                if (dataClients != null && dataClients.Count > 0)
                {
                    foreach (var dataClient in dataClients)
                    {
                        if (dataClient.CPF.Equals(clModel.CPF))
                        {
                            throw new ArgumentException("O CPF já está cadastrado");
                        }
                    }
                }
            }

            client.Name = clModel.Name;
            client.Email = clModel.Email;
            client.CPF = clModel.CPF;

            var validationResult = _validator.Validate(client);
            if (!validationResult.IsValid)
            {
                var errors = String.Join("; ",
                    validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmailAsync(Client client, string email)
        {
            client.Email = email;
            var validationResult = _validator.Validate(client);
            if (!validationResult.IsValid)
            {
                var errors = String.Join("; ",
                    validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }

            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}

