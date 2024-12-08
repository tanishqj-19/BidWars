namespace server.Dto
{
    public class NotificationDto
    {
        public string Type { get; set; } // Auction Start, High Bid Alert, etc.
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        bool IsRead { get; set; } = false;
        public int? UserId { get; set; }
    }
}
