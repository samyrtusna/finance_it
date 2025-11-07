using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.RefreshTokenServices;

namespace Finance_it.API.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IRefreshTokenService _TokenServices;
        private readonly IJwtTokenGenerator _TokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IGenericRepository<User> userRepository, 
            IRefreshTokenService tokenServices, 
            IJwtTokenGenerator tokenGenerator,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userRepository = userRepository;
            _TokenServices = tokenServices;
            _TokenGenerator = tokenGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthenticationResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, $"the argument {nameof(dto)} is null");

            var existingUser = await _userRepository.GetByFilterAsync(u => u.Email.Equals(dto.Email));
            if (existingUser != null)
            {
                throw new BadRequestException("Email is already in use.");
            }
            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashedPassword,
                Role = dto.Role ?? Role.User,
                LastLogin = DateTime.UtcNow
            };
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();

            string accessToken = _TokenGenerator.GenerateAccessToken(newUser);
            var NewRefreshToken = await _TokenServices.AddRefreshTokenAsync(newUser.Id);

            await _userRepository.SaveAsync();

            var response = _httpContextAccessor.HttpContext!.Response;

            _TokenServices.SetTokenToCookies(NewRefreshToken.Token, response);



            return new AuthenticationResponseDto
            {
                Name = newUser.Name,
                Role = newUser.Role,
                AccessToken = accessToken,
            };
        }

        public async Task<ConfirmationResponseDto> ChangePassword(PasswordChangeRequestDto dto, string email)
        {
            ArgumentNullException.ThrowIfNull(dto, $"the argument {nameof(dto)} is null");

            var user = await _userRepository.GetByFilterAsync(u => u.Email == email) ?? throw new NotFoundException("User not found.");
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid current password.");
            }

            var HashedNewPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.Password = HashedNewPassword;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return new ConfirmationResponseDto
            {
                Message = "Password changed successfully."
            };

        }
    }
}
