namespace AAO25.Client.Models;

public class GameSession
{
    public GameSession(GameServerSession server, MatchInformation matchInformation, ScoreBoard scoreBoard)
    {
        Id = Guid.NewGuid().ToString();
        Server = server;
        MatchInformation = matchInformation;
        ScoreBoard = scoreBoard;
    }

    public string Id { get; set; }
    public GameServerSession Server{ get; }
    public MatchInformation MatchInformation { get; }
    public ScoreBoard ScoreBoard { get; }
     
    public bool CanBeTracked()
    {
        return ScoreBoard.OnlinePlayers.Count >= 2 && MatchInformation.CurrentRound != 0;
    }
}