using VisionPlatform.Application.DTOs.Clientes;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClienteResponseDto>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();

            return clientes.Select(c => new ClienteResponseDto
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();
        }

        public async Task<long> CreateAsync(CreateClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome
            };

            await _repository.AddAsync(cliente);
            return cliente.Id;
        }

        public async Task UpdateAsync(long id, UpdateClienteDto dto)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            cliente.Nome = dto.Nome;
            await _repository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(long id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            await _repository.DeleteAsync(cliente);
        }
    }
}
