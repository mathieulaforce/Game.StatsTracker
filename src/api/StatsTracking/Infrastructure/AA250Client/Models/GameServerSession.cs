namespace AAO25.Client.Models;

public class GameServerSession
{
    public GameServerSession(string hostname, string ipAddress)
    {
        Hostname = hostname;
        IpAddress = ipAddress; 
    }

    public string Hostname { get; }
    public string IpAddress { get; }
    
}