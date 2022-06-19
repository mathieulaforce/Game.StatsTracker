namespace LaMa.StatsTracking.Domain;

public class SessionPlayer
{
    public SessionPlayer(string name, int leader, int goal, int ping, int roe, int kiaPoints, int killPoints, int honor)
    {
        Name = name;
        Leader = leader;
        Goal = goal;
        Ping = ping;
        Roe = roe;
        KiaPoints = kiaPoints;
        KillPoints = killPoints;
        Honor = honor;  
    }

    public string Name { get; }
    public int Leader { get; }
    public int Goal { get; }
    public int Ping { get; }
    public int Roe { get; }
    public int KiaPoints { get; }
    public int KillPoints { get; }
    public int Honor { get; }


    //public int GetNumberOfKills()
    //{
    //    return KillPoints / 10;
    //}

    //public int GetNumberOfDeaths()
    //{
    //    return Math.Abs(KiaPoints / 10);
    //}
    //public decimal GetFragRate()
    //{
    //    var deaths = GetNumberOfDeaths();
    //    var kills = GetNumberOfKills();
    //    return deaths == 0 ? kills : Math.Round(((decimal)kills / deaths), 2);
    //}

    //public int GetTotalScore()
    //{
    //    var killScore = GetNumberOfDeaths() > GetNumberOfKills() ? 0 : KillPoints + KiaPoints;
    //    var regularScore = Goal + Roe + killScore;
    //    if (regularScore <= 0 && Leader < 0)
    //    {
    //        return regularScore;
    //    }

    //    if (regularScore <= -Leader && Leader < 0)
    //    {
    //        return 0;
    //    }

    //    return regularScore + Leader;
    //}

    //public int CalculateHonor()
    //{ 
    //    var expPointsPer10Lvls = new int[ 
    //        500,//'0-9'        
    //       1000,//'10-19'    
    //       2000,//'20-29'   
    //       4000,//'30-39'    
    //       6000,//'40-49'    
    //       9000,//'50-59'    
    //       13000,//'60-69'
    //       23000,//'70-79'
    //       43000,//'80-89'
    //       83000,//'90-99'
    //       133000];//'100-109'

    //    var honor = 0;
    //    var leftOverScore = GetTotalScore();

    //    foreach (var points in expPointsPer10Lvls)
    //    {
    //        var rangedScore = points * 10;
    //        if (leftOverScore - rangedScore < 0)
    //        {
    //            for (var index = 0; index < 10; index++)
    //            {
    //                if (leftOverScore - points < 0)
    //                {
    //                    return honor;
    //                } 
    //                leftOverScore -= points;
    //                honor++;
    //            }
    //        }
    //        else
    //        {
    //            honor += 10;
    //            leftOverScore -= rangedScore;
    //        }
    //    }
    //    return honor;
    //}

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