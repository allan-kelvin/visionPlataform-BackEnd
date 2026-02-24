using VisionPlatform.Application.DTOs.Users;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasherService _passwordHasher;

        public UserService(
            IUserRepository repository,
            IPasswordHasherService passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role.Nome,
                Ativo = u.Ativo
            }).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Role = user.Role.Nome,
                Ativo = user.Ativo
            };
        }

        public async Task<long> CreateAsync(CreateUserDto dto)
        {
            var existing = await _repository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email já cadastrado.");

            var user = new User
            {
                Nome = dto.Nome,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(dto.Senha),
                RoleId = dto.RoleId,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.AddAsync(user);

            return user.Id;
        }

        public async Task UpdateAsync(long id, UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            user.Nome = dto.Nome;
            user.RoleId = dto.RoleId;
            user.Ativo = dto.Ativo;

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            user.Ativo = false;

            await _repository.UpdateAsync(user);
        }
    }
}
