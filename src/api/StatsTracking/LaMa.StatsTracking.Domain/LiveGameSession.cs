using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Domain;

public class LiveGameSession
{
    public IpAddress ServerIp { get; }
    public GameSessionDetail Details { get; }
    public List<SessionPlayer> Players { get; }

    public LiveGameSession(IpAddress serverIp, GameSessionDetail gameSessionDetail, List<SessionPlayer> players)
    {
        ServerIp = serverIp;
        Details = gameSessionDetail;
        Players = players;
    }
     

}