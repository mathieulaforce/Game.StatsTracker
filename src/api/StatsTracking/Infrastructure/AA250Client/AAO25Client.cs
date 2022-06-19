using System.Net;
using System.Web;
using AAO25.Client.Models;
using AAO25.Client.Protocols.GameSpy2;

namespace AAO25.Client;

public interface IAAO25Client
{
   Task<IReadOnlyList<AAOServer>> GetServers();
   GameSession GetServerDetails(string ipAddressWithPort);
}

public class AAO25Client: IAAO25Client
{
    private readonly HttpClient _httpClient;

    public AAO25Client(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IReadOnlyList<AAOServer>> GetServers()
    {
        var response = await _httpClient.GetStringAsync("/srvlist.txt");
        var onlineSince = DateTimeOffset.UtcNow; 
        var servers = response.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var entry = line.Split(' ');
                return new AAOServer
                { 
                    Ip = entry[0],
                    CountryIso2 = entry[1], 
                    PasswordProtected =entry[5] != "0",
                    Name = HttpUtility.HtmlDecode(Uri.UnescapeDataString(entry[6])),
                    MapName = HttpUtility.HtmlDecode(Uri.UnescapeDataString(entry[7])),
                    NumberOfPlayers = int.Parse(entry[8]),
                    MaxPlayers = int.Parse(entry[9]),
                    Version = entry[10],
                    Ping = int.Parse(entry[14]),
                    LastOnlineSince =onlineSince,
                    IsOnline = true,
                };
            });
        return servers.ToList().AsReadOnly();
    }

    public GameSession GetServerDetails(string ipAddressWithPort)
    {
        using var protocol = new GameSpy2Protocol();
        var split = ipAddressWithPort.Split(':');
        var port = (int.Parse(split[1])) ;
        protocol.Connect(IPAddress.Parse(split[0]),port);
        var session = protocol.GetLiveSession();
        return session;
    }
}