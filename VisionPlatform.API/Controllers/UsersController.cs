using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisionPlatform.API.Authorization;
using VisionPlatform.Application.DTOs.Users;
using VisionPlatform.Application.Interfaces;

namespace VisionPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HasPermission("User.View")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HasPermission("User.View")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HasPermission("User.Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }

        [HasPermission("User.Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateUserDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HasPermission("User.Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
