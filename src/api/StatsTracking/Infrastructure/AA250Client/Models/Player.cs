namespace AAO25.Client.Models;

public class Player
{
    public Player(string name, int leader, int goal, int ping, int roe, int kia, int enemy, int honor)
    {
        Name = name;
        Leader = leader;
        Goal = goal;
        Ping = ping;
        Roe = roe;
        Kia = kia;
        Enemy = enemy;
        Honor = honor;
    }

    public string Name { get; }
    public int Leader { get; }
    public int Goal { get; }
    public int Ping { get; }
    public int Roe { get; }
    public int Kia { get; }
    public int Enemy { get; }
    public int Honor { get; }

    public Player AddScore(Player player)
    {
        if (player.Name != Name)
        {
            throw new Exception("adding score not allowed");
        }

        return new Player(Name, Leader + player.Leader, Goal + player.Goal, player.Ping, Roe + player.Roe, Kia + player.Kia, Enemy + player.Enemy, player.Honor);
    }
}