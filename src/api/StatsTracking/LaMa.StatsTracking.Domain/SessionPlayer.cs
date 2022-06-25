using LaMa.StatsTracking.Domain.Calculators;

namespace LaMa.StatsTracking.Domain;

public class SessionPlayer : IPlayer
{
    private readonly IPlayerScoreCalculator _calculator;

    public SessionPlayer(string name, int leader, int goal, int ping, int roe, int honor, IPlayerScoreCalculator calculator)
    {
        _calculator = calculator;
        Name = name;
        Leader = leader;
        Goal = goal;
        Ping = ping;
        Roe = roe; 
        Honor = honor; 
    }

    public string Name { get; }
    public int Leader { get; }
    public int Goal { get; }
    public int Ping { get; }
    public int Roe { get; }
 
    public int Honor { get; }

    public int Kills { get; private set; }
    public int Kia { get; private set; }
    public int KiaPoints { get; private set; }
    public int KillPoints { get; private set; }
    public decimal FragRate { get; private set; }
    public int TotalPoints{ get; private set; }

    public void SetAndCalculateKills(int killPoints, int kiaPoints)
    {
        KiaPoints = kiaPoints;
        KillPoints = killPoints;

        Kills = _calculator.GetNumberOfKills(this);
        Kia = _calculator.GetNumberOfDeaths(this);
        FragRate = _calculator.GetFragRate(this);

        TotalPoints = _calculator.GetTotalScore(this);
    }

    public bool IsNewerThan(SessionPlayer previousRoundSession)
    {
        if (previousRoundSession.Name != Name)
        {
            throw new Exception("not the same user");
        }

        return KiaPoints <= previousRoundSession.KiaPoints &&
               KillPoints >= previousRoundSession.KillPoints &&
               Roe <= previousRoundSession.Roe;
    }
}