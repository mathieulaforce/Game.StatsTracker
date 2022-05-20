using System.Web;
using AAO25.Client.Models;

namespace AAO25.Client;

public interface IAAO25Client
{
   Task<IReadOnlyList<AAOServer>> GetServers();
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
                    Name = HttpUtility.HtmlDecode(entry[6]),
                    Mapname = HttpUtility.HtmlDecode(entry[7]),
                    NumberOfPlayers = int.Parse(entry[8]),
                    MaxPlayers = int.Parse(entry[9]),
                    Version = entry[10],
                    Ping = int.Parse(entry[14]),
                    LastOnlineSince =onlineSince
                };
            });
        return servers.ToList().AsReadOnly();
    }
}