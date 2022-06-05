namespace AAO25.Client.Protocols.GameSpy2;

public interface IGameSpy2QueryBuilder
{
    IGameSpy2QueryBuilder WithServerInformation();

    IGameSpy2QueryBuilder WithPlayerInformation();

    IGameSpy2QueryBuilder WithTeamInformation();

    byte[] BuildQuery();
}

public class GameSpy2QueryBuilder : IGameSpy2QueryBuilder
{
    private readonly byte[] _queryBase = { 0xFE, 0xFD, 0x00, 0x04, 0x05, 0x06, 0x07, 0x00, 0x00, 0x00 };

    private GameSpy2QueryBuilder()
    {
    }

    public IGameSpy2QueryBuilder WithServerInformation()
    {
        _queryBase[7] = 0xFF;
        return this;
    }

    public IGameSpy2QueryBuilder WithPlayerInformation()
    {
        _queryBase[8] = 0xFF;
        return this;
    }

    public IGameSpy2QueryBuilder WithTeamInformation()
    {
        _queryBase[9] = 0xFF;
        return this;
    }

    public byte[] BuildQuery()
    {
        return _queryBase;
    }

    public static IGameSpy2QueryBuilder GetQuery()
    {
        return new GameSpy2QueryBuilder();
    }
}