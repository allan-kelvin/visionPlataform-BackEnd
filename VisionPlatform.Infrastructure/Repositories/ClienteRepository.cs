using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly VisionDbContext _context;

        public ClienteRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(long id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
