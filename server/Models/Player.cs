using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class Player
    {

      
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Player Name is required")]
        [MinLength(3, ErrorMessage = "Name should be of minimum length 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Player Sport is required")]
        public string Sport { get; set; }

        [Required(ErrorMessage = "Player Age is required")]
        [Range(16, 40, ErrorMessage = "Player age should be between 17 and 40")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Player Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Player Position is required")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Base Price is required")]
        [Range(50000, Int64.MaxValue, ErrorMessage = "Base Price should be greater than 50000")]
        public decimal BasePrice { get; set; }
        
        public int AgentId { get; set; } // FK to User (Player Agent)
        public string Status { get; set; } // Available, Sold, etc.
        public int? TeamId {  get; set; }


        [JsonIgnore]
        public User Agent { get; set; }

        [JsonIgnore]
        public Team Contractor { get; set; }
        [JsonIgnore]
        public ICollection<AuctionResult> AuctionResults { get; set; } = new List<AuctionResult>();

        [JsonIgnore]
        public ICollection<PerformanceReport> PerformanceReports { get; set; } = new List<PerformanceReport>();

        [JsonIgnore]
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
