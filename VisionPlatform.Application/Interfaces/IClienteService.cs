using VisionPlatform.Application.DTOs.Clientes;

namespace VisionPlatform.Application.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> GetAllAsync();
        Task<long> CreateAsync(CreateClienteDto dto);
        Task UpdateAsync(long id, UpdateClienteDto dto);
        Task DeleteAsync(long id);
    }
}
