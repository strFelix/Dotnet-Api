using api.Dtos;
using api.Models;

namespace api.Repository.UserRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> SelectAllAsync();
        public Task<User?> SelectUserByIdAsync(int id);
        public Task<User?> SelectUserByEmailAsync(string email);
        public Task InsertUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(User user);
    }
}