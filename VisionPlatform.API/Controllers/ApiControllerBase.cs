using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VisionPlatform.API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected long GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return 0;
            return long.Parse(userIdClaim.Value);
        }
    }
}
