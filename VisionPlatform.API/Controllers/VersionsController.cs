using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VisionPlatform.API.Authorization;
using VisionPlatform.Application.DTOs.Versions;

namespace VisionPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VersionsController : ControllerBase
    {
        private readonly IVersionService _service;

        public VersionsController(IVersionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HasPermission("Version.Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVersionDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = long.Parse(userIdClaim.Value);

            var id = await _service.CreateAsync(dto, userId);

            return Ok(id);
        }

        [HasPermission("Version.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateVersionDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HasPermission("Version.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HasPermission("Version.Release")]
        [HttpPost("{id}/release")]
        public async Task<IActionResult> Release(long id)
        {
            await _service.ReleaseVersionAsync(id);
            return NoContent();
        }
    }
}
