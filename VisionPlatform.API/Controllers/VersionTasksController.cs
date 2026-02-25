using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VisionPlatform.API.Authorization;
using VisionPlatform.Application.DTOs.VersionTasks;
using VisionPlatform.Application.Interfaces;

namespace VisionPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VersionTasksController : ApiControllerBase
    {
        private readonly IVersionTaskService _service;

        public VersionTasksController(IVersionTaskService service)
        {
            _service = service;
        }

        // ===============================
        // LISTAR POR VERSÃO
        // ===============================

        [HttpGet("by-version/{versionId}")]
        public async Task<IActionResult> GetByVersion(long versionId)
        {
            var result = await _service.GetByVersionIdAsync(versionId);
            return Ok(result);
        }

        // ===============================
        // CRIAR TAREFA
        // ===============================

        [HasPermission("VersionTask.Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVersionTaskDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }

        // ===============================
        // ATUALIZAR TAREFA
        // ===============================

        [HasPermission("VersionTask.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateVersionTaskDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        // ===============================
        // MARCAR MERGE
        // ===============================

        [HasPermission("VersionTask.MarkMerge")]
        [HttpPost("{id}/merge")]
        public async Task<IActionResult> MarkMerge(long id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = long.Parse(userIdClaim.Value);

            await _service.MarkMergeAsync(id, userId);

            return NoContent();
        }

        // ===============================
        // ATRIBUIR QA
        // ===============================

        [HasPermission("VersionTask.AssignQA")]
        [HttpPost("{id}/assign-qa/{qaUserId}")]
        public async Task<IActionResult> AssignQa(long id, long qaUserId)
        {
            await _service.AssignQaAsync(id, qaUserId);
            return NoContent();
        }

        // ===============================
        // EXCLUIR TAREFA
        // ===============================

        [HasPermission("VersionTask.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
