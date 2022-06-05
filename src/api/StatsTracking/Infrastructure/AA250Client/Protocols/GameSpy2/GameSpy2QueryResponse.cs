using System.Text;

namespace AAO25.Client.Protocols.GameSpy2;

public class GameSpy2QueryResponse
{
    private GameSpy2QueryResponse()
    {
    }

    public string Value { get; init; } = string.Empty;

    public static GameSpy2QueryResponse BytesResponse(byte[] responseBytes)
    {
        return new GameSpy2QueryResponse
        {
            Value = Encoding.UTF8.GetString(responseBytes)
        };
    }
}