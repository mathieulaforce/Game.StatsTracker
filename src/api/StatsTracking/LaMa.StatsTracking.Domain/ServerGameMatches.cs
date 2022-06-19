using LaMa.StatsTracking.Domain.Exceptions;

namespace LaMa.StatsTracking.Domain;

public class ServerGameMatches
{

    private List<GameMatch> _gameMatches;

    public ServerGameMatches()
    {
        _gameMatches = new List<GameMatch>();
    }
    public ServerGameMatches(IEnumerable<GameMatch>? matches):this()
    { 
        if (matches != null)
        {
            _gameMatches.AddRange(matches);
        } 
    }

    public void RegisterGameSession(LiveGameSession liveGameSession)
    {
        if (!CanRegisterSessionToMatch(liveGameSession))
        {
            throw new InvalidGameSessionForGameMatchException();
        }

        var match = GetOrCreateMatchFromSession(liveGameSession);
        var roundInformation = new RoundInformation(liveGameSession.Details.CurrentRound,
            liveGameSession.Details.TotalRound, liveGameSession.Details.TimeLeft);
        match.RegisterNewRound(roundInformation, liveGameSession.Players);
    }

    public IReadOnlyCollection<GameMatch> GetGameMatches()
    {
        return _gameMatches.AsReadOnly();
    }

    private GameMatch GetOrCreateMatchFromSession(LiveGameSession liveGameSession)
    { 
        var gameMatch = _gameMatches.FirstOrDefault(match => match.IsValidSessionForMatch(liveGameSession));
        if (gameMatch == null)
        {
            return CreateAndRegisterNewMatch(liveGameSession);
        }
        return gameMatch;
    }

    private GameMatch CreateAndRegisterNewMatch(LiveGameSession liveGameSession)
    {
        var match = new GameMatch(liveGameSession.ServerIp, liveGameSession.Details.MapName);
        _gameMatches.Add(match);
        if (_gameMatches.Count == 2)
        {
            _gameMatches.First().MarkAsFinished();
        }
        return match;
    }

    private bool CanRegisterSessionToMatch(LiveGameSession liveGameSession)
    {
        return !_gameMatches.Any() || _gameMatches.All(match => match.ServerIp == liveGameSession.ServerIp);
    } 
}