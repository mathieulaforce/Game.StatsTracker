namespace AAO25.Client.Models;

public class GameServerSession
{
    public GameServerSession(string hostname, string ipAddress, bool passwordProtected, string version, bool cheats, bool miles, string adminName)
    {
        Hostname = hostname;
        IpAddress = ipAddress;
        PasswordProtected = passwordProtected;
        Version = version;
        Cheats = cheats;
        Miles = miles;
        AdminName = adminName;
    }

    public string Hostname { get; }
    public string IpAddress { get; }
    public bool PasswordProtected{ get; }
    public string Version { get;  }
    public bool Cheats { get;  }
    public bool Miles { get;  }
    public string AdminName { get;  }

}