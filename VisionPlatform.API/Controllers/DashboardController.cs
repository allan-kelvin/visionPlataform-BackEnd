using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionPlatform.Application.Interfaces;

namespace VisionPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime? inicio,
            [FromQuery] DateTime? fim)
        {
            var result = await _service.GetDashboardAsync(inicio, fim);
            return Ok(result);
        }
    }
}
