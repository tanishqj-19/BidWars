using server.Dto;
using server.Models;

namespace server.Services.Interfaces
{
    public interface IUserService
    {

        public Task<string> RegisterUser(UserDto newUserDto);

        public Task<string> LoginUser(string email, string password);

        public Task<User> GetUserByEmail(string email);

        public Task<User> GetUserById(int id);

        public Task<IEnumerable<User>> GetAllUsers();
    }
}
