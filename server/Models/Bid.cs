using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class Bid
    {

        public int BidId { get; set; }
        public int AuctionId { get; set; } // FK to Auction
        public int PlayerId { get; set; } // FK to Player
        public int TeamId { get; set; } // FK to Team
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public string Status { get; set; } // Highest, Outbid, etc.

        [JsonIgnore]
        public Auction Auction { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public Team Team
        {
            get; set;
        }
    }
}
