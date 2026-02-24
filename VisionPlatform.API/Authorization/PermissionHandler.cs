using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using VisionPlatform.Application.Interfaces;

namespace VisionPlatform.API.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;

        public PermissionHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return;

            var userId = long.Parse(userIdClaim.Value);

            var hasPermission = await _permissionService
                .HasPermissionAsync(userId, requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);
        }
    }
}
