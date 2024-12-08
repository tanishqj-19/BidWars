using Microsoft.AspNetCore.SignalR;
using server.Models;

namespace server.Services.Interfaces
{
    public interface INotificationService
    {
        //Task AddAndBroadcastNotification(string message, int? userId = null);
        Task SendBidWinningConfirmation(User user, Player player, Auction auction);
        Task SendRegistrationConfirmation(User user);
        Task AddNotification(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsAsync(int userId);
    }
}
