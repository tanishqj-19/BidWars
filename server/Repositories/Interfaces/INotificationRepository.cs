using server.Models;

namespace server.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId);

        Task DeleteNotificationAsync(Notification notication);
    }
}
