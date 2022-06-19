using LaMa.Domain.SharedKernel;

namespace LaMa.StatsTracking.Domain.ValueObjects;

public class IpAddress : ValueObject<IpAddress>
{
    public IpAddress()
    {
        Ip = string.Empty;
    }
    public IpAddress(string ipWithPort)
    {
        var values = ipWithPort.Split(':');
        if (values.Length != 2)
        {
            throw new ArgumentException();
        }

        Ip = values[0];
        Port = int.Parse(values[1]);
    }

    public IpAddress(string ip, string port)
    {
        Ip = ip;
        Port = int.Parse(port);
    }
    public IpAddress(string ip, int port)
    {
        Ip = ip;
        Port = port;
    }

    public string Ip { get; init; }
    public int Port { get; init; }

    public override string ToString()
    {
        return $"{Ip}:{Port}";
    }

    public static bool operator ==(IpAddress a, string b)
    {
        var bAddress = new IpAddress(b);
        return a == bAddress;
    }

    public static bool operator !=(IpAddress a, string b)
    {
        return !(a == b);
    }

    public static bool operator ==(string b, IpAddress a)
    {
        return a == b;
    }

    public static bool operator !=(string b, IpAddress a)
    {
        return !(a == b);
    }
}