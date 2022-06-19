using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Domain.Builders;

public class LiveGameSessionBuilder
{
    private readonly IpAddress _serverIp;
    private int _currentRound;
    private int _totalRounds;
    private string _mapName;
    private string _timeLeft; 
    private List<SessionPlayer> _players;

    public LiveGameSessionBuilder(IpAddress serverIp)
    {
        _serverIp = serverIp;
    }

    public static LiveGameSessionBuilder ForServer(IpAddress serverIp)
    {
        return new LiveGameSessionBuilder(serverIp);
    }

    public LiveGameSessionBuilder OnRound(int currentRound, int maxRound, string timeLeft)
    {
        _currentRound = currentRound;
        _totalRounds = maxRound;
        _timeLeft = timeLeft;
        return this;
    }
    public LiveGameSessionBuilder OnMap(string mapName)
    {
        _mapName = mapName;
        return this;
    }

    public LiveGameSessionBuilder AddPlayers(IEnumerable<SessionPlayer> players)
    {
        _players = players.ToList();
        return this;
    }

    public LiveGameSession Build()
    {
        var session = new GameSessionDetail(_currentRound, _totalRounds, _mapName, _timeLeft);
        return new LiveGameSession(_serverIp, session, _players);
    }
}