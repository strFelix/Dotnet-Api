using api.Dtos;

namespace api.Services.UserServices
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllAsync();
        public Task<UserDto> GetUserByIdAsync(int id);
        public Task PostUserAsync(UserDto userDto);
        public Task PutUserAsync(int id, UserDto userDto);
        public Task DeleteUserAsync(int id);
    }
}
