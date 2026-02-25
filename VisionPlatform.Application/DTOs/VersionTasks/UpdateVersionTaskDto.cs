using VisionPlatform.Domain.Enums;
using TaskStatus = VisionPlatform.Domain.Enums.TaskStatus;

namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class UpdateVersionTaskDto
    {
        public string Titulo { get; set; } = string.Empty;
        public TaskType Tipo { get; set; }
        public TaskStatus StatusPlanejamento { get; set; }
        public int? OrdemExibicao { get; set; }
    }
}
