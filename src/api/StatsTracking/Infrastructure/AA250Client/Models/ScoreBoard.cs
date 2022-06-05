namespace AAO25.Client.Models;

public class ScoreBoard
{  

    public List<Player> OnlinePlayers { get; private set; }

    public ScoreBoard()
    {
        OnlinePlayers = new List<Player>(); 
    }

    public void RegisterOnlinePlayer(Player player)
    {
        OnlinePlayers.Add(player); 
    } 
}