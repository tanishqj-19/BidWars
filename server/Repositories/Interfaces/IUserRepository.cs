using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IUserRepository
    {

        public Task<User> AddUser(User user);
        public Task<User?> GetUserByEmail(string email);
        //public Task DeleteUser (User user);

        //public Task UpdateUser (User user);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User?> GetUserById(int id);
    }
}
