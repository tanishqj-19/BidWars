using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet("{userId}")]

        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications(int userId)
        {
            var notifications = await notificationService.GetNotificationsAsync(userId);

            return Ok(notifications);   
        }

    }
}
