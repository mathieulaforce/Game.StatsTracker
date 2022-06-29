namespace LaMa.StatsTracking.Data.DTO;

public class PlayerDTO
{
    public PlayerDTO()
    {
        
    }

    public string Id => Name;
    public int Kills { get; set; }
    public int Kia { get; set; }
    public decimal FragRate { get; set; }
    public int TotalPoints { get; set; }
    public int LargestKillStreak { get; set; }
    public int LargestDeathStreak { get; set; }
    public int TimesGoldPlayer { get; set; }
    public int TimesSilverPlayer { get; set; }
    public int TimesBronzePlayer { get; set; }
    public int TimesDeadliestPlayer { get; set; }
    public int LargestTotalScore { get; set; }
    public int LowestRoeScore { get; set; }
    public int LargestGoalScore { get; set; }
    public int LargestLeaderScore { get; set; }
    public int RoundsPlayed { get; set; }
    public int Reconnects { get; set; }
    public string Name { get; set; }
    public int Leader { get; set; }
    public int Goal { get; set; }
    public int Ping { get; set; }
    public int Roe { get; set; }
    public int Honor { get; set; }
    public int KiaPoints { get; set; }
    public int KillPoints { get; set; }
}