namespace VisionPlatform.Application.DTOs.Dashboard
{
    public class DashboardResponseDto
    {
        public DashboardSummaryDto Summary { get; set; }
        public List<DashboardVersionCardDto> Versions { get; set; }
    }
}
