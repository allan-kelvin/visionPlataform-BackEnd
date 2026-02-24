using VisionPlatform.Application.DTOs.Dashboard;
using VisionPlatform.Application.Interfaces;


namespace VisionPlatform.Application.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardResponseDto> GetDashboardAsync(DateTime? inicio, DateTime? fim)
        {
            inicio ??= DateTime.UtcNow.AddDays(-30);
            fim ??= DateTime.UtcNow;

            var summary = await _repository.GetSummaryAsync(inicio.Value, fim.Value);
            var cards = await _repository.GetVersionCardsAsync(inicio.Value, fim.Value);

            return new DashboardResponseDto
            {
                Summary = summary,
                Versions = cards
            };
        }
    }
}