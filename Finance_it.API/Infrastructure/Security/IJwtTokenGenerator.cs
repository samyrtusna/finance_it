using Finance_it.API.Data.Entities;

namespace Finance_it.API.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user); 
        string GenerateRefreshToken();
    }
}
