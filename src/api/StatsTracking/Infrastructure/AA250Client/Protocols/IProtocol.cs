using System.Net;
using AAO25.Client.Models;

namespace AAO25.Client.Protocols;

public interface IProtocol : IDisposable
{
    string Name { get; }

    void Connect(IPAddress ip, int port);

    GameSession GetLiveSession();
}