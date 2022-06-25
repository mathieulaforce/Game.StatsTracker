using LaMa.StatsTracking.Domain.Calculators;

namespace LaMa.StatsTracking.Domain;

public class TrackedPlayer : IPlayer
{
    private readonly IPlayerScoreCalculator _calculator;
    private int _lastDeathNumberBeforeKill;

    private int _lastKillNumberBeforeDeath;

    public TrackedPlayer(string name, int honor, IPlayerScoreCalculator calculator)
    {
        _calculator = calculator;
        Name = name;
        Honor = honor;
    }

    public int Kills { get; private set; }
    public int Kia { get; private set; }
    public decimal FragRate { get; private set; }
    public int TotalPoints { get; private set; }
    public int LargestKillStreak { get; private set; }
    public int LargestDeathStreak { get; private set; }
    public int TimesGoldPlayer { get; private set; }
    public int TimesSilverPlayer { get; private set; }
    public int TimesBronzePlayer { get; private set; }
    public int TimesDeadliestPlayer { get; private set; }
    public int LargestTotalScore { get; private set; }
    public int LowestRoeScore { get; private set; }
    public int LargestGoalScore { get; private set; }
    public int LargestLeaderScore { get; private set; }
    public int RoundsPlayed { get; private set; }
    public int Reconnects { get; private set; }
    public string Name { get; }
    public int Leader { get; private set; }
    public int Goal { get; private set; }
    public int Ping { get; }
    public int Roe { get; private set; }
    public int Honor { get; }
    public int KiaPoints { get; private set; }
    public int KillPoints { get; private set; } 
    public int Level { get; private set; }

    public void AddRoundPlayed(int goalScore, int leaderScore, int roeScore, int killPoints, int kiaPoints)
    {
        Goal = goalScore;
        Leader = leaderScore;
        Roe = roeScore;

        var previousKills = Kills;
        var previousDeaths = Kia;

        Kills = _calculator.GetNumberOfKills(this);
        Kia = _calculator.GetNumberOfDeaths(this);

        var streak = _calculator.CalculateKillStreak(this, _lastDeathNumberBeforeKill, _lastKillNumberBeforeDeath);
        FragRate = _calculator.GetFragRate(this);
        TotalPoints = _calculator.GetTotalScore(this);

        if (previousKills < Kills) _lastKillNumberBeforeDeath = Kills;

        if (previousDeaths < Kia) _lastDeathNumberBeforeKill = Kia;

        if (LargestDeathStreak < streak.DeathStreak) LargestDeathStreak = streak.DeathStreak;

        if (LargestKillStreak < streak.KillStreak) LargestKillStreak = streak.KillStreak;

        KillPoints = killPoints;
        KiaPoints = kiaPoints;
        RoundsPlayed++;
    }

    public void AddReconnect()
    {
        _lastKillNumberBeforeDeath = Kills;
        _lastDeathNumberBeforeKill = Kia;
        Reconnects++;
    }

    public void AddBasicScore(TrackedPlayer trackedPlayer)
    {
        if (trackedPlayer.Name != Name) throw new Exception("not the same player");

        Goal += trackedPlayer.Goal;
        Leader += trackedPlayer.Leader;
        Roe += trackedPlayer.Roe;
        KillPoints += trackedPlayer.KillPoints;
        KiaPoints += trackedPlayer.KiaPoints;
        Kills = _calculator.GetNumberOfKills(this);
        Kia = _calculator.GetNumberOfDeaths(this);
    }

    public void CalculateAllScores(PlayerScoreCalculator calculator)
    {
        TotalPoints = calculator.GetTotalScore(this);
        LargestGoalScore = Goal;
        LargestLeaderScore = Leader;
        LowestRoeScore = Roe;
        LargestTotalScore = TotalPoints;
    }

    public void GiveGoldMedal()
    {
        TimesGoldPlayer++;
    }

    public void GiveSilverMedal()
    {
        TimesSilverPlayer++;
    }

    public void GiveBronzeMedal()
    {
        TimesBronzePlayer++;
    }

    public void SetDeadliestPlayer()
    {
        TimesDeadliestPlayer++;
    }

    public void SetScore(int goal, int leader, int roe, int totalScore, int killPoints, int kiaPoints)
    {
        Goal = goal;
        Leader = leader;
        Roe = roe;
        TotalPoints = totalScore;
        KillPoints = killPoints;
        KiaPoints = kiaPoints;
        Kills = _calculator.GetNumberOfKills(this);
        Kia = _calculator.GetNumberOfDeaths(this);
        FragRate = _calculator.GetFragRate(this);
    }

    public void SetHonours(int timesGoldPlayer, int timesSilverPlayer, int timesBronzePlayer, int timesDeadliestPlayer)
    {
        TimesGoldPlayer = timesGoldPlayer;
        TimesSilverPlayer = timesSilverPlayer;
        TimesBronzePlayer = timesBronzePlayer;
        TimesDeadliestPlayer = timesDeadliestPlayer;
    }

    public void SetRecords(int largestGoalScore, int largestLeaderScore, int largestTotalScore,
        int largestKillStreak, int largestDeathStreak, int lowestRoeScore)
    {
        LargestGoalScore = largestGoalScore;
        LargestLeaderScore = largestLeaderScore;
        LargestTotalScore = largestTotalScore;
        LargestKillStreak = largestKillStreak;
        LargestDeathStreak = largestDeathStreak;
        LowestRoeScore = lowestRoeScore;
    }

    public void SetReconnectsAndRoundsPlayed(int reconnects, int roundsPlayed)
    {
        Reconnects = reconnects;
        RoundsPlayed =roundsPlayed;
    }
     
    public void CalculateLevel()
    {
        Level = _calculator.CalculateLevel(this);
    }

    public void AddMatchScore(TrackedPlayer trackedPlayer)
    {
        Leader += trackedPlayer.Leader;
        Goal += trackedPlayer.Goal;
        Leader += trackedPlayer.Leader;
        Roe += trackedPlayer.Roe;
        TotalPoints += trackedPlayer.TotalPoints;
        KillPoints += trackedPlayer.KillPoints;
        KiaPoints += trackedPlayer.KiaPoints;
        Kills = _calculator.GetNumberOfKills(this);
        Kia = _calculator.GetNumberOfDeaths(this);
        FragRate = _calculator.GetFragRate(this);

        Reconnects += trackedPlayer.Reconnects;
        RoundsPlayed += trackedPlayer.RoundsPlayed;

        TimesGoldPlayer += trackedPlayer.TimesGoldPlayer;
        TimesSilverPlayer += trackedPlayer.TimesSilverPlayer;
        TimesBronzePlayer += trackedPlayer.TimesBronzePlayer;
        TimesDeadliestPlayer += trackedPlayer.TimesDeadliestPlayer;

        if (LargestGoalScore < trackedPlayer.LargestGoalScore)
            LargestGoalScore = trackedPlayer.LargestGoalScore;

        if (LargestLeaderScore < trackedPlayer.LargestLeaderScore)
            LargestLeaderScore = trackedPlayer.LargestLeaderScore;

        if (LargestTotalScore < trackedPlayer.LargestTotalScore)
            LargestTotalScore = trackedPlayer.LargestTotalScore;

        if (LargestKillStreak < trackedPlayer.LargestKillStreak)
            LargestKillStreak = trackedPlayer.LargestKillStreak;

        if (LargestDeathStreak < trackedPlayer.LargestDeathStreak)
            LargestDeathStreak = trackedPlayer.LargestDeathStreak;

        if (LowestRoeScore > trackedPlayer.LowestRoeScore)
            LowestRoeScore = trackedPlayer.LowestRoeScore;

        CalculateLevel();
    }
}