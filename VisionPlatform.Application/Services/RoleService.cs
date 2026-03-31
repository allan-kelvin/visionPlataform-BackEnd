using VisionPlatform.Application.DTOs.Roles;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();

            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Nome = r.Nome
            }).ToList();
        }
    }
}
