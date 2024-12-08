namespace server.Dto
{
    public class UserDto
    {   
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, TeamManager, Auctioneer, etc.
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
