using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, TeamManager, Auctioneer, etc.
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<Team> ManagedTeams { get; set; } = new List<Team>(); // For Team Managers
        [JsonIgnore]
        public ICollection<Player> Players { get; set; } = new List<Player>();// For Player Agents
        [JsonIgnore]
        public ICollection<PerformanceReport> PerformanceReports { get; set; } = new List<PerformanceReport>(); // For Analysts
        [JsonIgnore]
        public ICollection<Auction> Auctions { get; set; } = new List<Auction>();// For Auctioneers
        [JsonIgnore]
        public ICollection<Notification> Notifications  { get; set; } = new List<Notification>();
    }
}
