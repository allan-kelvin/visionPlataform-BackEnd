using VisionPlatform.Application.DTOs.Dashboard;

namespace VisionPlatform.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardAsync(DateTime? inicio, DateTime? fim);

    }
}
