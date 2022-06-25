namespace LaMa.StatsTracking.Domain.Calculators
{
    public interface IPlayerScoreCalculator
    {
        int GetNumberOfKills(IPlayer player);
        int GetNumberOfDeaths(IPlayer player);
        decimal GetFragRate(IPlayer player);
        int GetTotalScore(IPlayer player);
        int CalculateHonor(IPlayer player);
    }

    public class PlayerScoreCalculator : IPlayerScoreCalculator
    {
        public int GetNumberOfKills(IPlayer player)
        {
            return player.KillPoints / 10;
        }

        public int GetNumberOfDeaths(IPlayer player)
        {
            return Math.Abs(player.KiaPoints / 10);
        }
        public decimal GetFragRate(IPlayer player)
        {
            var deaths = GetNumberOfDeaths(player);
            var kills = GetNumberOfKills(player);
            return deaths == 0 ? kills : Math.Round(((decimal)kills / deaths), 2);
        }

        public int GetTotalScore(IPlayer player)
        {
            var killScore = GetNumberOfDeaths(player) > GetNumberOfKills(player) ? 0 : player.KillPoints + player.KiaPoints;
            var regularScore = player.Goal + player.Roe + killScore;
            if (regularScore <= 0 && player.Leader < 0)
            {
                return regularScore;
            }

            if (regularScore <= -player.Leader && player.Leader < 0)
            {
                return 0;
            }

            return regularScore + player.Leader;
        }

        public int CalculateHonor(IPlayer player)
        {
            var expPointsPer10Lvls = new int[
                500,//'0-9'        
               1000,//'10-19'    
               2000,//'20-29'   
               4000,//'30-39'    
               6000,//'40-49'    
               9000,//'50-59'    
               13000,//'60-69'
               23000,//'70-79'
               43000,//'80-89'
               83000,//'90-99'
               133000];//'100-109'

            var honor = 0;
            var leftOverScore = GetTotalScore(player);

            foreach (var points in expPointsPer10Lvls)
            {
                var rangedScore = points * 10;
                if (leftOverScore - rangedScore < 0)
                {
                    for (var index = 0; index < 10; index++)
                    {
                        if (leftOverScore - points < 0)
                        {
                            return honor;
                        }
                        leftOverScore -= points;
                        honor++;
                    }
                }
                else
                {
                    honor += 10;
                    leftOverScore -= rangedScore;
                }
            }
            return honor;
        }
    }
}