using System.Net;
using System.Net.Sockets;
using AAO25.Client.Models;

namespace AAO25.Client.Protocols.GameSpy2;

public class GameSpy2Protocol : IProtocol
{
    private readonly UdpClient _client;
    private IPEndPoint? _remoteHostEndPoint;

    public GameSpy2Protocol()
    {
        _client = new UdpClient();
    }

    public string Name => "GameSpy2";

    public void Connect(IPAddress ipAddress, int port)
    {
        _remoteHostEndPoint = new IPEndPoint(ipAddress, port);
        _client.Connect(_remoteHostEndPoint);
    }

    public GameSession GetLiveSession()
    {
        if (_remoteHostEndPoint == null)
        {
            throw new NullReferenceException($"the {nameof(IPEndPoint)} cannot be null");
        }

        SendGameServerInfoRequest();
        var response = GetGameServerInfoResponse();
        return response.MapToGameSession(_remoteHostEndPoint);
    }

    public void Dispose()
    {
        _client?.Dispose();
    }

    private void SendGameServerInfoRequest()
    {
        var query = GameSpy2QueryBuilder.GetQuery()
            .WithServerInformation()
            .WithPlayerInformation()
            .BuildQuery();

        _client.Send(query, query.Length);
    }

    private GameSpy2QueryResponse GetGameServerInfoResponse()
    {
         
        var result = _client.BeginReceive(null, null);
        result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(20)); 
        if (result.IsCompleted)
        {
            IPEndPoint? remoteEP = null;
            var receivedData = _client.EndReceive(result, ref remoteEP);
            return GameSpy2QueryResponse.BytesResponse(receivedData);
        }

        throw new TimeoutException("Server did not respond in time");
    }
}