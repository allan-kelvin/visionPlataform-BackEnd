namespace VisionPlatform.Domain.Entities
{
    public class Role
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
