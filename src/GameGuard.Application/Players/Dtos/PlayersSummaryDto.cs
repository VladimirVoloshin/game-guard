namespace GameGuard.Application.Players.Dtos
{
    public record PlayersSummaryDto(int Total, int Active, int Suspicious, int Banned);
}
