using LaMa.StatsTracking.Domain;

namespace LaMa.StatsTracking.Data.DTO;

public class GameMatchDTO
{
    public GameMatchDTO()
    {
        RoundScores = new List<RoundScoreDTO>();
    }

    public string Id { get; set; }
    public string Ip { get; set; }
    public string MapName { get; set; }
    public string Game => "AAO2.5";
    public int CurrentRound { get; set; }
    public int TotalRounds { get; set; }
    public string TimeLeft { get; set; }
    public bool IsFinished { get; set; }

    public List<RoundScoreDTO> RoundScores{ get; set; }

}

public class RoundScoreDTO
{
    public int RoundNumber { get; set; }
    public List<SessionPlayerDTO> DisconnectedPlayers { get;  set; }
    public List<SessionPlayerDTO> SessionPlayers { get; set; }
}