using Finance_it.API.Models;
using Finance_it.API.Models.AppDbContext;
using Finance_it.API.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Repositories.CustomRepositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context; 

        public RefreshTokenRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
