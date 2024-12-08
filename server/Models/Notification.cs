namespace server.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Type { get; set; } // Auction Start, High Bid Alert, etc.
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        bool IsRead { get; set; } = false;
        public int? UserId { get; set; } // FK to User
                                        // Navigation Properties
        public User User { get; set; }
    }
}
