using VisionPlatform.Application.DTOs.VersionTasks;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class VersionTaskService : IVersionTaskService
    {
        private readonly IVersionTaskRepository _repository;
        private readonly IVersionRepository _versionRepository;

        public VersionTaskService(IVersionTaskRepository repository,IVersionRepository versionRepository)
        {
            _repository = repository;
            _versionRepository = versionRepository;
        }

        public async Task<List<VersionTaskResponseDto>> GetByVersionIdAsync(long versionId)
        {
            var tasks = await _repository.GetByVersionIdAsync(versionId);

            return tasks.Select(t => new VersionTaskResponseDto
            {
                Id = t.Id,
                VersionId = t.VersionId,
                Titulo = t.Titulo,
                Tipo = t.Tipo,
                StatusPlanejamento = t.StatusPlanejamento,
                MergeRealizado = t.MergeRealizado,
                PossuiScript = t.PossuiScript,
                PossuiTagVersao = t.PossuiTagVersao
            }).ToList();
        }

        public async Task<long> CreateAsync(CreateVersionTaskDto dto)
        {
            var task = new VersionTask
            {
                VersionId = dto.VersionId,
                AzureTaskId = dto.AzureTaskId,
                AzureTaskUrl = dto.AzureTaskUrl,
                Titulo = dto.Titulo,
                ClienteId = dto.ClienteId,
                AreaId = dto.AreaId,
                Tipo = dto.Tipo,
                StatusPlanejamento = dto.StatusPlanejamento,
                QaUserId = dto.QaUserId,
                OrdemExibicao = dto.OrdemExibicao,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.AddAsync(task);
            return task.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            await _repository.DeleteAsync(task);
        }

        public async Task UpdateAsync(long id, UpdateVersionTaskDto dto)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            task.Titulo = dto.Titulo;
            task.Tipo = dto.Tipo;
            task.StatusPlanejamento = dto.StatusPlanejamento;
            task.OrdemExibicao = dto.OrdemExibicao;

            var version = await _versionRepository.GetByIdAsync(task.VersionId);

            if (version!.StatusVersao == "Liberada")
                throw new Exception("Não é possível alterar tarefa de versão liberada.");

            await _repository.UpdateAsync(task);
        }
        public async Task MarkMergeAsync(long id, long userId)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            task.MergeRealizado = true;
            task.DataMerge = DateTime.UtcNow;
            task.QuemFezMerge = userId;

            var version = await _versionRepository.GetByIdAsync(task.VersionId);
            if (version!.StatusVersao == "Liberada")
                throw new Exception("Não é possivel alterar a tarefa de versão Liberada");

            await _repository.UpdateAsync(task);
        }

        public async Task AssignQaAsync(long id, long qaUserId)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            task.QaUserId = qaUserId;

            await _repository.UpdateAsync(task);
        }

    }
}
