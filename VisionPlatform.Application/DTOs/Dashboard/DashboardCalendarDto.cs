namespace VisionPlatform.Application.DTOs.Dashboard
{
    public class DashboardCalendarDto
    {
        public DateTime Data { get; set; }
        public string Tipo { get; set; } // Planejamento, Teste, Deploy
        public int Quantidade { get; set; }
    }
}
