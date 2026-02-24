using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;
using  Microsoft.EntityFrameworkCore;


namespace VisionPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly VisionDbContext _context;

        public UserRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
