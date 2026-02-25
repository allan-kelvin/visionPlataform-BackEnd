using VisionPlatform.Domain.Enums;

namespace VisionPlatform.Application.DTOs.Dashboard
{
    public class DashboardVersionCardDto
    {
        public long VersionId { get; set; }
        public string NumeroVersao { get; set; }
        public string Status { get; set; }

        public int TotalTarefas { get; set; }
        public int Confirmadas { get; set; }
        public int Desejaveis { get; set; }

        public int SemQA { get; set; }
        public int SemMerge { get; set; }

        public double PercentualConfirmadas { get; set; }
        public double PercentualDesejaveis { get; set; }

        public string IndicadorRisco { get; set; } // Verde | Amarelo | Vermelho
    }
}
