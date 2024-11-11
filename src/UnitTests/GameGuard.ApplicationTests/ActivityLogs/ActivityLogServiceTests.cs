using FluentAssertions;
using GameGuard.Application.ActivityLogs;
using GameGuard.Application.ActivityLogs.Dtos;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.Common;
using Moq;

namespace GameGuard.ApplicationTests.ActivityLogs
{
    public class ActivityLogServiceTests
    {
        private readonly Mock<IActivityLogRepository> _mockRepository;
        private readonly ActivityLogService _service;

        public ActivityLogServiceTests()
        {
            _mockRepository = new Mock<IActivityLogRepository>();
            _service = new ActivityLogService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllPlayerActivitiesAsync_ReturnsPagedResult()
        {
            // Arrange
            var filter = new ActivityLogFilterDto(new[] { 1, 2 }, true);
            var page = 1;
            var pageSize = 10;

            var activities = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.Login),
                new ActivityLog(2, ActivityActionType.Logout)
            };

            _mockRepository
                .Setup(r => r.GetAllAsync(It.IsAny<ISpecification<ActivityLog>>(), page, pageSize))
                .ReturnsAsync((activities, 2));

            // Act
            var result = await _service.GetActivityLogs(filter, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.TotalCount.Should().Be(2);
            result.Page.Should().Be(page);
            result.PageSize.Should().Be(pageSize);
            result.Items.Should().HaveCount(2);
            result.Items.Should().AllBeOfType<ActivityLogDto>();
        }

        [Fact]
        public async Task GetAllPlayerActivitiesAsync_EmptyResult()
        {
            // Arrange
            var filter = new ActivityLogFilterDto(new[] { 3 }, false);
            var page = 1;
            var pageSize = 10;

            _mockRepository
                .Setup(r => r.GetAllAsync(It.IsAny<ISpecification<ActivityLog>>(), page, pageSize))
                .ReturnsAsync((new List<ActivityLog>(), 0));

            // Act
            var result = await _service.GetActivityLogs(filter, page, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.TotalCount.Should().Be(0);
            result.Items.Should().BeEmpty();
        }
    }
}
