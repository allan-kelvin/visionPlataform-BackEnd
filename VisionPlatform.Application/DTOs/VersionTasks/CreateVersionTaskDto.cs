using VisionPlatform.Domain.Enums;
using TaskStatus = VisionPlatform.Domain.Enums.TaskStatus;

namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class CreateVersionTaskDto
    {
        // ==========================================
        // VERSÃO
        // ==========================================

        public long VersionId { get; set; }


        // ==========================================
        // AZURE
        // ==========================================

        public long AzureTaskId { get; set; }

        public string AzureTaskUrl { get; set; } = string.Empty;


        // ==========================================
        // TAREFA
        // ==========================================

        public string Titulo { get; set; } = string.Empty;

        public long ClienteId { get; set; }

        public long AreaId { get; set; }

        public TaskType Tipo { get; set; }

        public TaskStatus StatusPlanejamento { get; set; }
            = TaskStatus.Planejada;

        public long? QaUserId { get; set; }


        // ==========================================
        // MERGE
        // ==========================================

        public bool MergeRealizado { get; set; }

        public DateTime? DataMerge { get; set; }


        // ==========================================
        // SCRIPT
        // ==========================================

        public bool PossuiScript { get; set; }

        public string? ScriptDescricao { get; set; }


        // ==========================================
        // TAG
        // ==========================================

        public bool PossuiTagVersao { get; set; }

        public string? NomeTagGerada { get; set; }


        // ==========================================
        // ORDEM
        // ==========================================

        public int? OrdemExibicao { get; set; }
    }
}

