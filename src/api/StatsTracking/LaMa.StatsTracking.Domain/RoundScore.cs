namespace LaMa.StatsTracking.Domain;

public class RoundScore
{
    public RoundScore(int roundNumber)
    {
        RoundNumber = roundNumber;
        DisconnectedPlayers = new List<SessionPlayer>();
        SessionPlayers = new List<SessionPlayer>();
    }
    public int RoundNumber { get; }
    public List<SessionPlayer> DisconnectedPlayers { get; internal set; }
    public List<SessionPlayer> SessionPlayers { get; internal set; } 

    public void UpdateSession(List<SessionPlayer> players)
    {
        if (!SessionPlayers.Any())
        {
            SessionPlayers = players.ToList();
            return;
        }

        var currentAndPreviousActivePlayerNames =
            players.Select(_ => _.Name).Concat(SessionPlayers.Select(_ => _.Name)).Distinct().ToList();

        foreach (var playerName in currentAndPreviousActivePlayerNames)
        {
            var previousRoundSession = SessionPlayers.FirstOrDefault(player => player.Name == playerName);
            var currentSession = players.FirstOrDefault(player => player.Name == playerName);
            var disconnectedPlayer = DisconnectedPlayers.FirstOrDefault(player => player.Name == playerName);

            if (previousRoundSession == null && currentSession != null)
            {
                // new player
                SessionPlayers.Add(currentSession);
            }

            if (previousRoundSession != null && disconnectedPlayer ==null && currentSession == null)
            { 
                // player left
                DisconnectedPlayers.Add(previousRoundSession); 
            }
            if (previousRoundSession != null && currentSession != null)
            {
                
                if (disconnectedPlayer == null)
                {
                    if (currentSession.IsNewerThan(previousRoundSession))
                    {
                        // player has not reconnected, we can replace current stats
                        SessionPlayers.Remove(previousRoundSession);
                        SessionPlayers.Add(currentSession);
                    }
                    else
                    {
                        // player has reconnected, stats hijacking in progress
                        DisconnectedPlayers.Add(previousRoundSession);
                    } 
                }
                else
                {
                    // do nothing, user reconnected this round. possible stats hijacking
                }
            }
        }

    }
}