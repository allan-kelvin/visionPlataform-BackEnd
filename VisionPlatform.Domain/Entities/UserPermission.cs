namespace VisionPlatform.Domain.Entities
{
    public class UserPermission
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long PermissionId { get; set; }
        public Permission Permission { get; set; }

        public bool IsAllowed { get; set; }
    }
}
