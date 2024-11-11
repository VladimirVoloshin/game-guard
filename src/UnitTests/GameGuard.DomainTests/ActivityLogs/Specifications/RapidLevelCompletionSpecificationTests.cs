using FluentAssertions;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Specifications;
using Moq;

namespace GameGuard.DomainTests.ActivityLogs.Specifications
{
    public class RapidLevelCompletionSpecificationTests
    {
        [Fact]
        public async Task RapidLevelCompletionSpecification_ShouldReturnFalse_WhenLevelsNotCompletedRapidly()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new RapidLevelCompletionSpecification(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime.AddSeconds(-45)),
                new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime.AddSeconds(-90))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 2, ActivityActionType.CompleteLevel))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnTrue_WhenLevelsCompletedRapidly()
        {
            // Arrange
            var mockRepository = new Mock<IActivityLogRepository>();

            var specification = new RapidLevelCompletionSpecification(mockRepository.Object);

            var currentTime = DateTime.UtcNow;
            var activityLog = new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime);
            var recentLogs = new List<ActivityLog>
            {
                new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime.AddSeconds(-15)),
                new ActivityLog(1, ActivityActionType.CompleteLevel, currentTime.AddSeconds(-25))
            };

            mockRepository
                .Setup(r => r.GetRecentActivityLogsAsync(1, 2, ActivityActionType.CompleteLevel))
                .ReturnsAsync(recentLogs);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeTrue();
        }
    }
}
