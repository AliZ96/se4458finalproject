using Microsoft.AspNetCore.Mvc;
using SE4458FinalProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SE4458FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetByUser(string userId)
        {
            var notifications = await _notificationService.GetAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetById(string id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> Create(Notification notification)
        {
            await _notificationService.CreateAsync(notification);
            return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return NotFound();
            await _notificationService.RemoveAsync(id);
            return NoContent();
        }
    }
} 