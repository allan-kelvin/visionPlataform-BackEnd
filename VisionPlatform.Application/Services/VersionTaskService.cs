using VisionPlatform.Application.DTOs.VersionTasks;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Enums;
using VisionPlatform.Domain.Interfaces;
using TaskStatus = VisionPlatform.Domain.Enums.TaskStatus;

namespace VisionPlatform.Application.Services
{
     public class VersionTaskService : IVersionTaskService
    {
        private readonly IVersionTaskRepository _repository;
        private readonly IVersionRepository _versionRepository;

        public VersionTaskService(
            IVersionTaskRepository repository,
            IVersionRepository versionRepository)
        {
            _repository = repository;
            _versionRepository = versionRepository;
        }

        // ============================================================
        // LISTAR TAREFAS DA VERSÃO
        // ============================================================

        public async Task<List<VersionTaskResponseDto>> GetByVersionIdAsync(long versionId)
        {
            var tasks = await _repository.GetByVersionIdAsync(versionId);

            return tasks.Select(t => new VersionTaskResponseDto
            {
                Id = t.Id,
                VersionId = t.VersionId,

                AzureTaskId = t.AzureTaskId,
                AzureTaskUrl = t.AzureTaskUrl,

                Titulo = t.Titulo,

                Cliente = t.Cliente?.Nome ?? "",

                // Temporariamente sem mapeamento da Área
                Area = "",

                Tipo = t.Tipo.ToString(),
                StatusPlanejamento = t.StatusPlanejamento.ToString(),

                Qa = t.QaUser?.Nome ?? "",

                MergeRealizado = t.MergeRealizado,
                PossuiScript = t.PossuiScript,
                PossuiTagVersao = t.PossuiTagVersao

            }).ToList();
        }


        // ============================================================
        // CRIAR TAREFA
        // ============================================================

        public async Task<long> CreateAsync(CreateVersionTaskDto dto)
        {
            var version = await _versionRepository.GetByIdAsync(dto.VersionId);

            if (version == null)
                throw new Exception("Versão não encontrada.");


            // ========================================================
            // VERSÃO LIBERADA
            // ========================================================

            if (version.StatusVersao == VersionStatus.Liberada)
            {
                throw new Exception(
                    "Não é possível adicionar tarefa em uma versão liberada."
                );
            }


            // ========================================================
            // CRIA TAREFA
            // ========================================================

            var task = new VersionTask
            {
                // ------------------------------------
                // VERSÃO
                // ------------------------------------

                VersionId = dto.VersionId,


                // ------------------------------------
                // AZURE
                // ------------------------------------

                AzureTaskId = dto.AzureTaskId,

                AzureTaskUrl = dto.AzureTaskUrl,


                // ------------------------------------
                // DADOS DA TAREFA
                // ------------------------------------

                Titulo = dto.Titulo,

                ClienteId = dto.ClienteId,

                AreaId = dto.AreaId,

                Tipo = dto.Tipo,

                StatusPlanejamento = dto.StatusPlanejamento,

                QaUserId = dto.QaUserId,


                // ------------------------------------
                // MERGE
                // ------------------------------------

                MergeRealizado = dto.MergeRealizado,

                DataMerge = dto.MergeRealizado
                    ? dto.DataMerge
                    : null,


                // ------------------------------------
                // SCRIPT
                // ------------------------------------

                PossuiScript = dto.PossuiScript,

                ScriptDescricao = dto.PossuiScript
                    ? dto.ScriptDescricao
                    : null,


                // ------------------------------------
                // TAG
                // ------------------------------------

                PossuiTagVersao = dto.PossuiTagVersao,

                NomeTagGerada = dto.PossuiTagVersao
                    ? dto.NomeTagGerada
                    : null,


                // ------------------------------------
                // ORDEM
                // ------------------------------------

                OrdemExibicao = dto.OrdemExibicao,


                // ------------------------------------
                // DATA
                // ------------------------------------

                DataCriacao = DateTime.UtcNow
            };


            // ========================================================
            // SALVA
            // ========================================================

            await _repository.AddAsync(task);


            return task.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            var version = await _versionRepository.GetByIdAsync(task.VersionId);

            if (version == null)
                throw new Exception("Versão não encontrada.");

            if (version.StatusVersao == VersionStatus.Liberada)
                throw new Exception("Não é possível excluir tarefa de versão liberada.");

            await _repository.DeleteAsync(task);
        }

        public async Task UpdateAsync(long id, UpdateVersionTaskDto dto)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            var version = await _versionRepository.GetByIdAsync(task.VersionId);

            if (version == null)
                throw new Exception("Versão não encontrada.");

            // Versão liberada é imutável
            if (version.StatusVersao == VersionStatus.Liberada)
                throw new Exception(
                    "Não é possível alterar tarefa de uma versão liberada."
                );

            // Para confirmar, precisa ter QA
            if (dto.StatusPlanejamento == TaskStatus.Confirmado &&
                task.QaUserId == null)
            {
                throw new Exception(
                    "Não é possível confirmar uma tarefa sem um QA atribuído."
                );
            }

            task.Titulo = dto.Titulo;
            task.Tipo = dto.Tipo;
            task.StatusPlanejamento = dto.StatusPlanejamento;
            task.OrdemExibicao = dto.OrdemExibicao;

            await _repository.UpdateAsync(task);
        }

        public async Task MarkMergeAsync(long id, long userId)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            var version = await _versionRepository.GetByIdAsync(task.VersionId);

            if (version == null)
                throw new Exception("Versão não encontrada.");

            if (version.StatusVersao == VersionStatus.Liberada)
                throw new Exception(
                    "Não é possível alterar tarefa de uma versão liberada."
                );

            task.MergeRealizado = true;
            task.DataMerge = DateTime.UtcNow;
            task.QuemFezMerge = userId;

            await _repository.UpdateAsync(task);
        }

        public async Task AssignQaAsync(long id, long qaUserId)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Tarefa não encontrada.");

            var version = await _versionRepository.GetByIdAsync(task.VersionId);

            if (version == null)
                throw new Exception("Versão não encontrada.");

            // Versão liberada é imutável
            if (version.StatusVersao == VersionStatus.Liberada)
                throw new Exception(
                    "Não é possível alterar tarefa de uma versão liberada."
                );

            task.QaUserId = qaUserId;

            await _repository.UpdateAsync(task);
        }
    }
}
