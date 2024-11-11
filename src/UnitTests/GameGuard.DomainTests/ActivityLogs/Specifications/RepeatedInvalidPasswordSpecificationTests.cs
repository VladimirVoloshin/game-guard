using FluentAssertions;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Specifications;
using Moq;

namespace GameGuard.DomainTests.ActivityLogs.Specifications
{
    public class RepeatedInvalidPasswordSpecificationTests
    {
        [Fact]
        public async Task ShouldReturnTrue_WhenThreeConsecutiveInvalidPasswords()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new RepeatedInvalidPasswordSpecification(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime.AddMinutes(-1)),
                new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime.AddMinutes(-2)),
                new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime.AddMinutes(-3))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 3, null))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFalse_WhenNotThreeConsecutiveInvalidPasswords()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new RepeatedInvalidPasswordSpecification(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime.AddMinutes(-1)),
                new ActivityLog(1, ActivityActionType.Login, currentTime.AddMinutes(-2)),
                new ActivityLog(1, ActivityActionType.InvalidPassword, currentTime.AddMinutes(-3))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 3, null))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeFalse();
        }
    }
}
