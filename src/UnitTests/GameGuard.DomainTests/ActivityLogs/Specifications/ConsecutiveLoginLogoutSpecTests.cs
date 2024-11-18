using FluentAssertions;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Specifications;
using Moq;

namespace GameGuard.DomainTests.ActivityLogs.Specifications
{
    public class ConsecutiveLoginLogoutSpecTests
    {
        [Fact]
        public async Task ShouldReturnTrue_WhenConsecutiveLoginLogoutDetected()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new ConsecutiveLoginLogoutSpec(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.Login, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.Login, currentTime.AddMinutes(-1)),
                new ActivityLog(1, ActivityActionType.Logout, currentTime.AddMinutes(-2)),
                new ActivityLog(1, ActivityActionType.Login, currentTime.AddMinutes(-3)),
                new ActivityLog(1, ActivityActionType.Logout, currentTime.AddMinutes(-4))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 4, null))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFalse_WhenNoConsecutiveLoginLogoutDetected()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new ConsecutiveLoginLogoutSpec(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.Login, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.Login, currentTime.AddMinutes(-1)),
                new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime.AddMinutes(-2)),
                new ActivityLog(1, ActivityActionType.Login, currentTime.AddMinutes(-3)),
                new ActivityLog(1, ActivityActionType.Logout, currentTime.AddMinutes(-4))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 4, null))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeFalse();
        }
    }
}
