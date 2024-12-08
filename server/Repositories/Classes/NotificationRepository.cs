using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SportsDbContext _context;

        public NotificationRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(Notification notication)
        {
            _context.Notifications.Remove(notication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId).OrderByDescending(n => n.Timestamp)
                .AsQueryable()
                .ToListAsync();
        }

    }
}
