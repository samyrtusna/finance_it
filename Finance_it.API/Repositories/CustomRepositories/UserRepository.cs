using Finance_it.API.Models;
using Finance_it.API.Models.AppDbContext;
using Finance_it.API.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Repositories.CustomRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmailAsync(string email) 
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
