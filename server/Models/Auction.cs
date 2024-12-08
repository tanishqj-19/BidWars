using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class Auction
    {

        
        public int AuctionId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Sport category is required")]

        public string Sport { get; set; }

        public int PlayerId { get; set; }
        public int AuctioneerId { get; set; } // Foreign Key to User

        [Required(ErrorMessage = "Auction start time required")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Auction end time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Auction status is required")]
        public string Status { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public User Auctioneer { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();   

        [JsonIgnore]
        public ICollection<AuctionResult> AuctionResults { get; set; } = new List<AuctionResult>();

    }
}
