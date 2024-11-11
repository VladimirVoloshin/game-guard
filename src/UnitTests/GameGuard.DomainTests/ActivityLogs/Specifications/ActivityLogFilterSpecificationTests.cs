using FluentAssertions;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Specifications;

namespace GameGuard.DomainTests.ActivityLogs.Specifications
{
    public class ActivityLogFilterSpecificationTests
    {
        [Fact]
        public void ToExpression_WithPlayerIds_ShouldFilterCorrectly()
        {
            // Arrange
            var playerIdsFilter = new[] { 1, 2 };
            var spec = new ActivityLogFilterSpecification(playerIdsFilter, null);

            // Act
            var expression = spec.ToExpression();
            var func = expression.Compile();

            // Assert
            func(new ActivityLog(1, ActivityActionType.Login)).Should().BeTrue();
            func(new ActivityLog(2, ActivityActionType.MaxScorePlayer)).Should().BeTrue();
            func(new ActivityLog(3, ActivityActionType.CompleteLevel)).Should().BeFalse();
        }

        [Fact]
        public void ToExpression_WithIsSuspicious_ShouldFilterCorrectly()
        {
            // Arrange
            var isSuspiciousFilter = true;
            IEnumerable<int> playerIdsFilter = [];
            var spec = new ActivityLogFilterSpecification(playerIdsFilter, isSuspiciousFilter);
            var suspiciousLog = new ActivityLog(1, ActivityActionType.MaxScorePlayer);
            suspiciousLog.MarkAsSuspicious();
            var normalLog = new ActivityLog(1, ActivityActionType.CompleteLevel);

            // Act
            var expression = spec.ToExpression();
            var func = expression.Compile();

            // Assert
            func(suspiciousLog).Should().BeTrue();
            func(normalLog).Should().BeFalse();
        }

        [Fact]
        public void ToExpression_WithBothFilters_ShouldCombineCorrectly()
        {
            // Arrange
            var playerIdsFilter = new[] { 1, 2 };
            var isSuspiciousFilter = true;
            var spec = new ActivityLogFilterSpecification(playerIdsFilter, isSuspiciousFilter);

            var suspiciousLog1 = new ActivityLog(1, ActivityActionType.MaxScorePlayer);
            suspiciousLog1.MarkAsSuspicious();
            var suspiciousLog2 = new ActivityLog(2, ActivityActionType.Login);
            suspiciousLog2.MarkAsSuspicious();
            var normalLog1 = new ActivityLog(1, ActivityActionType.CompleteLevel);
            var suspiciousLog3 = new ActivityLog(3, ActivityActionType.MaxScorePlayer);
            suspiciousLog3.MarkAsSuspicious();

            // Act
            var expression = spec.ToExpression();
            var func = expression.Compile();

            // Assert
            func(suspiciousLog1).Should().BeTrue();
            func(suspiciousLog2).Should().BeTrue();
            func(normalLog1).Should().BeFalse();
            func(suspiciousLog3).Should().BeFalse();
        }

        [Fact]
        public void ToExpression_WithNoFilters_ShouldReturnAllRecords()
        {
            // Arrange
            IEnumerable<int> playerIdsFilter = [];
            bool? isSuspiciousFilter = null;
            var spec = new ActivityLogFilterSpecification(playerIdsFilter, isSuspiciousFilter);

            var log1 = new ActivityLog(1, ActivityActionType.Login);
            var log2 = new ActivityLog(2, ActivityActionType.CompleteLevel);
            var suspiciousLog = new ActivityLog(3, ActivityActionType.MaxScorePlayer);
            suspiciousLog.MarkAsSuspicious();

            // Act
            var expression = spec.ToExpression();
            var func = expression.Compile();

            // Assert
            func(log1).Should().BeTrue();
            func(log2).Should().BeTrue();
            func(suspiciousLog).Should().BeTrue();
        }
    }
}
