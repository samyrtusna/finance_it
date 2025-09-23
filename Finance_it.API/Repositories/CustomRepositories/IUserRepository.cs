using Finance_it.API.Models;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Repositories.CustomRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}
