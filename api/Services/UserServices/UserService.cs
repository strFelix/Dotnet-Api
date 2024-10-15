using api.Dtos;
using api.Repository.UserRepositories;
using AutoMapper;
using api.Models;
using api.Http.Exceptions;

namespace api.Services.UserServices
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            List<User> users = await _userRepository.SelectAllAsync();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            User? user = await _userRepository.SelectUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found.", "GET: api/User/");
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task PostUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            User? userExists = await _userRepository.SelectUserByEmailAsync(user.Email);
            if (userExists != null && !userExists.Equals(user))
            {
                throw new ConflictException("User already exists", "POST: api/User/");
            }
            await _userRepository.InsertUserAsync(user);
        }

        public async Task PutUserAsync(int id, UserDto userDto)
        {
            User? userExists = await _userRepository.SelectUserByIdAsync(id);
            if(userExists is null)
            {
                throw new NotFoundException("User not found", "PUT: api/User/");
            }
            
            userExists.Name = userDto.Name;
            userExists.Email = userDto.Email;
            userExists.PhoneNumber = userDto.PhoneNumber;

            await _userRepository.UpdateUserAsync(userExists);
        }

        public async Task DeleteUserAsync(int id)
        {
            User? userExists = await _userRepository.SelectUserByIdAsync(id);
            if (userExists is null)
            {
                throw new NotFoundException("User not found", "DELETE: api/User/");
            }

            await _userRepository.DeleteUserAsync(userExists);
        }
    }
}
