using VisionPlatform.Application.DTOs.Dashboard;

namespace VisionPlatform.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<DashboardVersionCardDto>> GetVersionCardsAsync(DateTime inicio, DateTime fim);
        Task<DashboardSummaryDto> GetSummaryAsync(DateTime inicio, DateTime fim);

    }
}
