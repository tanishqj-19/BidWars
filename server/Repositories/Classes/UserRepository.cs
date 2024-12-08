using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly SportsDbContext context;
        public UserRepository(SportsDbContext context)
        {
            this.context = context;
        }

        public async Task<User> AddUser(User newUser)
        {
            var user = await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return user.Entity;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await context.Users.Where(u => u.Role == "Team Manager" || u.Role == "Player Agent" || u.Role == "Admin").ToListAsync();
        }

       
        public async Task<User?> GetUserById(int id)
        {
            var currUser = await context.Users.FindAsync(id);

            return currUser;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var currUser = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            return currUser;
        }

        //public async Task UpdatePassword(string email, string oldPassword, string newPassword)
        //{
        //    var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        //    if (user == null)
        //        throw new Exception("User doesn't exist");


        //}

        //public async Task DeleteUser(int id)
        //{
        //    var user = await context.Users.FindAsync(id);
        //    context.Users.Remove(user);
        //    await context.SaveChangesAsync();


        //}
    }
}
