using GameGuard.Application.ActivityLogs;
using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace GameGuard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityLogsController : ControllerBase
    {
        private const int FIRST_PAGE = 1;
        private const int DEFAULT_PAGE_SIZE = 10;
        private readonly IActivityLogService _activityService;

        public ActivityLogsController(IActivityLogService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ActivityLogDto>>> GetAllPlayersActivitiesAsync(
            [FromQuery] ActivityLogFilterDto filter,
            [FromQuery] int page = FIRST_PAGE,
            [FromQuery] int pageSize = DEFAULT_PAGE_SIZE
        )
        {
            var activities = await _activityService.GetActivityLogs(filter, page, pageSize);
            return Ok(activities);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityAsync(
            [FromBody] AddActivityLogDto addPlayerActivity
        )
        {
            await _activityService.AddActivityAsync(addPlayerActivity);
            return Created();
        }

        [HttpPut("{id}/review")]
        public async Task<IActionResult> ReviewActivityAsync(
            int id,
            [FromBody] ReviewActivityDto request
        )
        {
            await _activityService.ReviewActivityAsync(id, request.IsSuspicious);
            return NoContent();
        }
    }
}
