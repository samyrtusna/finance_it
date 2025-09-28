using Finance_it.API.Models;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Repositories.CustomRepositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenByTokenAsync (string token);
    }
}
