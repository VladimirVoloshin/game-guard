using FluentAssertions;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Specifications;

namespace GameGuard.DomainTests.ActivityLogs.Specifications
{
    public class MaxScoreSpecificationTests
    {
        [Fact]
        public async Task ShouldReturnTrue_WhenMaxScoreActivityDetected()
        {
            // Arrange
            var specification = new MaxScoreSpecification();
            var activityLog = new ActivityLog(
                1,
                ActivityActionType.MaxScorePlayer,
                DateTime.UtcNow
            );

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFalse_WhenNotMaxScoreActivity()
        {
            // Arrange
            var specification = new MaxScoreSpecification();
            var activityLog = new ActivityLog(1, ActivityActionType.CompleteLevel, DateTime.UtcNow);

            // Act
            var result = await specification.IsSatisfiedByAsync(activityLog);

            // Assert
            result.Should().BeFalse();
        }
    }
}
