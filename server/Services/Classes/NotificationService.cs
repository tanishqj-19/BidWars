using Microsoft.AspNetCore.SignalR;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.SignalRHub;

namespace server.Services.Classes
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<AuctionHub> _hubContext;
        private readonly IEmailService _emailService;
        public NotificationService(INotificationRepository notificationRepository, IHubContext<AuctionHub> hubContext, IEmailService emailService)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsForUserAsync(userId);

            return notifications;
        }

        public async Task SendBidWinningConfirmation(User user, Player player, Auction auction)
        {
            var subject = "Congratulations! You've Won the Bid!";
            var message = $@"
            <h1>Bid Winning Confirmation</h1>
            <p>Dear {user.Username},</p>
            <p>Congratulations! You have successfully won the bid for the following player:</p>
            <p>
                <strong>Player Name:</strong> {player.Name} <br>
                <strong>Sport:</strong> {player.Sport} <br>
                <strong>Position:</strong> {player.Position} <br>
                <strong>Country:</strong> {player.Country} <br>
                <strong>Age:</strong> {player.Age} <br>
                <strong>Final Price:</strong> ${player.BasePrice.ToString("N2")} <br>
            </p>
            <p>
                <strong>Auction Details:</strong><br>
                <strong>Auction Date:</strong> {auction.Date.ToString("yyyy-MM-dd")} <br>
                <strong>Status:</strong> {auction.Status} <br>
                <strong>Start Time:</strong> {auction.StartTime.ToString("hh:mm tt")} <br>
                <strong>End Time:</strong> {auction.EndTime.ToString("hh:mm tt")} <br>
            </p>
            <p>Thank you for participating in the auction. We hope you enjoy managing your new player!</p>";

            await _emailService.SendEmail(user.Email, subject, message);
        }

        public async Task AddNotification(Notification notification)
        {
            //var users = await 

            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task SendRegistrationConfirmation(User user)
        {
            var subject = "Registration Confirmation";
            var message = $@"
            <h1>Account Creation Successfully</h1>
            <p>Dear Customer,</p>
            <p>Your registration is confirmed!</p>
            <p>
                <strong>Username:</strong> {user.Username} <br>
                <strong>Role:</strong> {user.Role} <br>
            </p>
            <p>Thank you for choosing us!</p>";

            await _emailService.SendEmail(user.Email, subject, message);
        }

        //public async Task AddAndBroadcastNotification(string message, IHubCallerClients clients, int? userId = null)
        //{
        //        // Save notification to the database

        //        await _notificationRepository.AddNotificationAsync(notification);

        //        await clients.All.SendAsync("ReciveNotification", new
        //        {
        //            UserId = userId,
        //            Message = message,
        //            Timestamp = DateTime.UtcNow,
        //            Type = "Auction",
        //        });

        //        //// Broadcast notification via SignalR
        //        //if (!string.IsNullOrEmpty(group))
        //        //{
        //        //    await _hubContext.Clients.Group(group).SendAsync("ReceiveNotification", message);
        //        //}
        //        //else
        //        //{
        //        //    await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        //        //}
        //    }
        //}
    }

}
