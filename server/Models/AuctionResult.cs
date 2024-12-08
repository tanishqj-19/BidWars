namespace server.Models
{
    public class AuctionResult
    {
        public int ResultId { get; set; }
        public int AuctionId { get; set; } // FK to Auction
        public int PlayerId { get; set; } // FK to Player
        public int? WinningTeamId { get; set; } // FK to Team (nullable)
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; } // Sold, Unsold, Withdrawn

        // Navigation Properties
        public Auction Auction { get; set; }
        public Player Player { get; set; }
        public Team WinningTeam { get; set; }
    }
}
