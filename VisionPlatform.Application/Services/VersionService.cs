using VisionPlatform.Application.DTOs.Versions;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class VersionService : IVersionService
    {
        private readonly IVersionRepository _repository;
        private readonly IVersionTaskRepository _versionTaskRepository;


        public VersionService(IVersionRepository repository,
                        IVersionTaskRepository versionTaskRepository)
        {
            _repository = repository;
            _versionTaskRepository = versionTaskRepository;
        }

        public async Task<List<VersionResponseDto>> GetAllAsync()
        {
            var versions = await _repository.GetAllAsync();

            return versions.Select(v => new VersionResponseDto
            {
                Id = v.Id,
                NumeroVersao = v.NumeroVersao,
                StatusVersao = v.StatusVersao,
                DataLimiteTarefas = v.DataLimiteTarefas,
                DataPrevistaLiberacao = v.DataPrevistaLiberacao,
                DataLiberacaoReal = v.DataLiberacaoReal,
                Observacoes = v.Observacoes
            }).ToList();
        }

        public async Task<VersionResponseDto?> GetByIdAsync(long id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;

            return new VersionResponseDto
            {
                Id = v.Id,
                NumeroVersao = v.NumeroVersao,
                StatusVersao = v.StatusVersao,
                DataLimiteTarefas = v.DataLimiteTarefas,
                DataPrevistaLiberacao = v.DataPrevistaLiberacao,
                DataLiberacaoReal = v.DataLiberacaoReal,
                Observacoes = v.Observacoes
            };
        }

        public async Task<long> CreateAsync(CreateVersionDto dto, long userId)
        {
            var version = new ReleaseVersion
            {
                NumeroVersao = dto.NumeroVersao,
                StatusVersao = dto.StatusVersao,
                DataLimiteTarefas = dto.DataLimiteTarefas,
                DataPrevistaLiberacao = dto.DataPrevistaLiberacao,
                Observacoes = dto.Observacoes,
                CriadorId = userId,
                DataCriacao = DateTime.UtcNow
            };



            await _repository.AddAsync(version);

            return version.Id;
        }

        public async Task UpdateAsync(long id, UpdateVersionDto dto)
        {
            var version = await _repository.GetByIdAsync(id);
            if (version == null)
                throw new Exception("Versão não encontrada.");

            if (version.StatusVersao == "Liberada")
                throw new Exception("Versão já liberada não pode ser alterada");

            version.NumeroVersao = dto.NumeroVersao;
            version.StatusVersao = dto.StatusVersao;
            version.DataLimiteTarefas = dto.DataLimiteTarefas;
            version.DataPrevistaLiberacao = dto.DataPrevistaLiberacao;
            version.DataLiberacaoReal = dto.DataLiberacaoReal;
            version.Observacoes = dto.Observacoes;

            await _repository.UpdateAsync(version);
        }

        public async Task DeleteAsync(long id)
        {
            var version = await _repository.GetByIdAsync(id);
            if (version == null)
                throw new Exception("Versão não encontrada.");
            if (version.StatusVersao == "Liberada")
                throw new Exception("Versão liberada não pode ser excluida.");

            await _repository.DeleteAsync(version);
        }

        public async Task ReleaseVersionAsync(long versionId)
        {
            var version = await _repository.GetByIdAsync(versionId);
            if (version == null)
                throw new Exception("Versão não encontrada.");

            var tasks = await _versionTaskRepository.GetByVersionIdAsync(versionId);

            var hasPendingMerge = tasks.Any(t => !t.MergeRealizado);

            if (hasPendingMerge)
                throw new Exception("Não é possível liberar a versão. Existem tarefas sem merge realizado.");

            version.StatusVersao = "Liberada";
            version.DataLiberacaoReal = DateTime.UtcNow;

            await _repository.UpdateAsync(version);
        }
    }
}
