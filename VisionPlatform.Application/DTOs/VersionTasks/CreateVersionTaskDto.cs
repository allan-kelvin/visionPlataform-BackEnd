using VisionPlatform.Domain.Enums;
using TaskStatus = VisionPlatform.Domain.Enums.TaskStatus;

namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class CreateVersionTaskDto
    {
        public long VersionId { get; set; }
        public long AzureTaskId { get; set; }
        public string AzureTaskUrl { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;

        public long ClienteId { get; set; }
        public long AreaId { get; set; }

        public TaskType Tipo { get; set; }
        public TaskStatus StatusPlanejamento { get; set; } = TaskStatus.Planejada; 
        public long? QaUserId { get; set; }

        public int? OrdemExibicao { get; set; }
    }
}
