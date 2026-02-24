using VisionPlatform.Application.DTOs.Area;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AreaResponseDto>> GetAllAsync()
        {
            var areas = await _repository.GetAllAsync();

            return areas.Select(a => new AreaResponseDto
            {
                Id = a.Id,
                Descricao = a.Descricao,
                Ativo = a.Ativo
            }).ToList();
        }

        public async Task<long> CreateAsync(CreateAreaDto dto)
        {
            var area = new Area
            {
                Descricao = dto.Descricao,
                Ativo = dto.Ativo
            };

            await _repository.AddAsync(area);
            return area.Id;
        }

        public async Task UpdateAsync(long id, CreateAreaDto dto)
        {
            var area = await _repository.GetByIdAsync(id);
            if (area == null)
                throw new Exception("Área não encontrada.");

            area.Descricao = dto.Descricao;
            area.Ativo = dto.Ativo;

            await _repository.UpdateAsync(area);
        }

        public async Task DeleteAsync(long id)
        {
            var area = await _repository.GetByIdAsync(id);
            if (area == null)
                throw new Exception("Área não encontrada.");

            await _repository.DeleteAsync(area);
        }
    }
}
