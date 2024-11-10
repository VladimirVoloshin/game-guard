using FluentAssertions;
using GameGuard.Application.Players;
using GameGuard.Domain.Players;
using Moq;

namespace GameGuard.ApplicationTests.Players
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockRepository;
        private readonly PlayerService _sut;

        public PlayerServiceTests()
        {
            _mockRepository = new Mock<IPlayerRepository>();
            _sut = new PlayerService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetPlayersStatsAsync_WithAllStatusTypes_ShouldReturnCorrectCounts()
        {
            // Arrange
            var statusSummary = new List<PlayerStatusSummary>
            {
                new PlayerStatusSummary(PlayerStatusType.Active, 50),
                new PlayerStatusSummary(PlayerStatusType.Suspicious, 30),
                new PlayerStatusSummary(PlayerStatusType.Banned, 20)
            };
            _mockRepository
                .Setup(r => r.GetPlayersStatusesSummaryAsync())
                .ReturnsAsync(statusSummary);

            // Act
            var result = await _sut.GetPlayersSummaryAsync();

            // Assert
            result.Should().NotBeNull();
            result.Total.Should().Be(100);
            result.Active.Should().Be(50);
            result.Suspicious.Should().Be(30);
            result.Banned.Should().Be(20);
        }

        [Fact]
        public async Task GetPlayersStatsAsync_WithMissingStatus_ShouldReturnZeroForMissingStatus()
        {
            // Arrange
            var statusSummary = new List<PlayerStatusSummary>
            {
                new PlayerStatusSummary(PlayerStatusType.Active, 50),
                new PlayerStatusSummary(PlayerStatusType.Suspicious, 30)
            };
            _mockRepository
                .Setup(r => r.GetPlayersStatusesSummaryAsync())
                .ReturnsAsync(statusSummary);

            // Act
            var result = await _sut.GetPlayersSummaryAsync();

            // Assert
            result.Should().NotBeNull();
            result.Total.Should().Be(80);
            result.Active.Should().Be(50);
            result.Suspicious.Should().Be(30);
            result.Banned.Should().Be(0);
        }

        [Fact]
        public async Task GetPlayersStatsAsync_WithEmptyResult_ShouldReturnAllZeros()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetPlayersStatusesSummaryAsync())
                .ReturnsAsync(new List<PlayerStatusSummary>());

            // Act
            var result = await _sut.GetPlayersSummaryAsync();

            // Assert
            result.Should().NotBeNull();
            result.Total.Should().Be(0);
            result.Active.Should().Be(0);
            result.Suspicious.Should().Be(0);
            result.Banned.Should().Be(0);
        }
    }
}
