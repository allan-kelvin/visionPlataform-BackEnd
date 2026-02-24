using VisionPlatform.Application.DTOs.Auth;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Application.Interfaces;


namespace VisionPlatform.Application.Services
{
    public class AuthService :IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasherService passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Usuário ou senha inválidos.");

            if (!user.Ativo)
                throw new Exception("Usuário inativo.");

            var validPassword = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!validPassword)
                throw new Exception("Usuário ou senha inválidos.");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.Nome
            };
        }

    }
}
