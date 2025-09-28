using Finance_it.API.Dtos.ApiResponsesDtos;
using Finance_it.API.Dtos.UserDtos;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models;
using Finance_it.API.Repositories.CustomRepositories;

namespace Finance_it.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenService _TokenServices;
        private readonly IJwtTokenGenerator _TokenGenerator;

        public UserService(IUserRepository userRepository, IRefreshTokenService tokenServices, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _TokenServices = tokenServices;
            _TokenGenerator = tokenGenerator;
        }
        public async Task<ApiResponseDto<AuthenticationResponseDto>> RegisterAsync (RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new ApiResponseDto<AuthenticationResponseDto>(400, "Email is already in use.");
            }
            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashedPassword,
                Role = dto.Role ?? Role.User,
            };
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();

            string accessToken = _TokenGenerator.GenerateAccessToken(newUser);
            var NewRefreshToken = await _TokenServices.AddRefreshTokenAsync(newUser.Id);

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = NewRefreshToken.Data.Token,
            }; 
            return new ApiResponseDto<AuthenticationResponseDto>(201, response);
        }
    }
}
