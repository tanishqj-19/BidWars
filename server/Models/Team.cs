using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class Team
    {
        
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team name is required")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Team manager is required")]

        public int ManagerId { get; set; } // FK to User (Team Manager)


        [Required(ErrorMessage = "Team sport is required")]
        public string Sport { get; set; }

        [Required(ErrorMessage = "Team budget is required")]
        [Range(20000, Int64.MaxValue, ErrorMessage = "Team budget should be greater than 20000")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Team region is required")]

        public string Region { get; set; }

        
        public int RosterSize { get; set; } = 0;

        
        public decimal TotalExpenditure { get; set; } = 0;
        
        public User Manager { get; set; }

        [JsonIgnore]
        public ICollection<Player> Players { get; set; } = new List<Player>();

        [JsonIgnore]
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();

        [JsonIgnore]
        public ICollection<Finance> Finances { get; set; } = new List<Finance>();
    }
}
