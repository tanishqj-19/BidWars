using Microsoft.AspNetCore.Http.HttpResults;
using server.Dto;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly AuthService _authService;
        private readonly IUserRepository userRepository;
        private readonly INotificationService _notificationService; 
        public UserService(AuthService authService, IUserRepository userRepository, INotificationService notificationService)
        {
            _authService = authService;
            this.userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task<string> RegisterUser(UserDto newUserDto)
        {
            //var existingUser = await userRepository.GetUserById(newUser.UserId);
            
            var emailUsed = await userRepository.GetUserByEmail(newUserDto.Email);

            if (emailUsed != null)
            {
                throw new Exception("User already exists");
            }

            var newUser = new User
            {
                Username = newUserDto.Username,
                Email = newUserDto.Email,
                Password = newUserDto.Password,
                IsActive = newUserDto.IsActive,
                Role = newUserDto.Role,
            };

            
            // Hashed the user PASSWORD
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
           
            var user = await userRepository.AddUser(newUser);
            await _notificationService.SendRegistrationConfirmation(user);

            var notify = new Notification
            {
                UserId = user.UserId,
                Message = $"Hi {user.Username}, welcome to Bidwars!.",
                Type = "Registration",
                Timestamp = DateTime.Now,
            };
            await _notificationService.AddNotification(notify);

            return "Registration Successfull ";

        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();

            return users;
        }
        public async Task<string> LoginUser(string email, string password)
        {
            var existingUser = await userRepository.GetUserByEmail(email);

            if (existingUser == null)
            {
                throw new Exception("User does not exist!");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
            {
                throw new Exception("Either email or password is not correct");
            }


            return _authService.GenerateToken(existingUser).ToString();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var currUser = await userRepository.GetUserByEmail(email);
            if (currUser == null)
                throw new Exception("No User Exist");

            return currUser;
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }
    }
}
