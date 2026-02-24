using Microsoft.EntityFrameworkCore;
using VisionPlatform.Application.DTOs.Dashboard;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly VisionDbContext _context;

        public DashboardRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<DashboardVersionCardDto>> GetVersionCardsAsync(DateTime inicio, DateTime fim)
        {
            var query = await _context.Versions
                .Where(v => v.DataCriacao >= inicio && v.DataCriacao <= fim)
                .Select(v => new DashboardVersionCardDto
                {
                    VersionId = v.Id,
                    NumeroVersao = v.NumeroVersao,
                    Status = v.StatusVersao,

                    TotalTarefas = v.Tasks.Count(),
                    Confirmadas = v.Tasks.Count(t => t.StatusPlanejamento == "Confirmado"),
                    Desejaveis = v.Tasks.Count(t => t.StatusPlanejamento == "Desejavel"),
                    SemQA = v.Tasks.Count(t => t.QaUserId == null),
                    SemMerge = v.Tasks.Count(t => !t.MergeRealizado)
                })
                .ToListAsync();

            // calcular percentuais e risco em memória (leve)
            foreach (var item in query)
            {
                item.PercentualConfirmadas =
                    item.TotalTarefas == 0 ? 0 :
                    (double)item.Confirmadas / item.TotalTarefas * 100;

                item.PercentualDesejaveis =
                    item.TotalTarefas == 0 ? 0 :
                    (double)item.Desejaveis / item.TotalTarefas * 100;

                item.IndicadorRisco =
                    item.SemMerge > 0 ? "Vermelho" :
                    item.SemQA > 0 ? "Amarelo" :
                    "Verde";
            }

            return query;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(DateTime inicio, DateTime fim)
        {
            var versions = _context.Versions
                .Where(v => v.DataCriacao >= inicio && v.DataCriacao <= fim);

            return new DashboardSummaryDto
            {
                TotalVersions = await versions.CountAsync(),
                EmPlanejamento = await versions.CountAsync(v => v.StatusVersao == "Planejamento"),
                EmTestes = await versions.CountAsync(v => v.StatusVersao == "EmTestes"),
                Liberadas = await versions.CountAsync(v => v.StatusVersao == "Liberada")
            };
        }
    }
}