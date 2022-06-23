using System.Net;
using System.Text.RegularExpressions;
using AAO25.Client.Models;

namespace AAO25.Client.Protocols.GameSpy2;

public static class GameSpy2Mappers
{
    public static GameSession MapToGameSession(this GameSpy2QueryResponse response, IPEndPoint endPoint)
    {
        var gameServer = GetGameServerSession(response.Value, endPoint);
        var matchInformation = GetMatchInformation(response.Value);
        var scoreBoard = GetScoreBoard(response.Value);

        return new GameSession(gameServer, matchInformation, scoreBoard);
    }

    private static GameServerSession GetGameServerSession(string response, IPEndPoint endPoint)
    {
       var passwordProtected =  GetKeyValueByRegexOrDefault(response, "password\\0(.*?)\\0") ?? "N/A";
       var cheats =  GetKeyValueByRegexOrDefault(response, "cheats\\0(.*?)\\0") ?? "N/A";
       var miles =  GetKeyValueByRegexOrDefault(response, "miles\\0(.*?)\\0") ?? "N/A";
       var adminName =  GetKeyValueByRegexOrDefault(response, "AdminName\\0(.*?)\\0") ?? "N/A";
       var version =  GetKeyValueByRegexOrDefault(response, "gamemode\\0(.*?)\\0") ?? "N/A";
       return new GameServerSession(GetHostname(response), endPoint.ToString(),passwordProtected =="1" ,version ,cheats =="1" ,miles =="1" ,adminName );
    }

    private static ScoreBoard GetScoreBoard(string response)
    {
        var scoreBoard = new ScoreBoard();
        var regex = "enemy_\\0.*\\0";
        var match = Regex.Match(response, regex);
        if (match.Success)
        {
            var groups = match.Groups;
            var playerData = groups[0].ToString().Remove(0, "enemy_00".Length)
                .Split("\0", StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < playerData.Length; i++)
            {
                var leader = int.Parse(playerData[i]);
                var goal = int.Parse(playerData[++i]);
                var honor = int.Parse(playerData[++i]);
                var name = playerData[++i];
                var ping = int.Parse(playerData[++i]);
                var roe = int.Parse(playerData[++i]);
                var kia = int.Parse(playerData[++i]);
                var enemy = int.Parse(playerData[++i]);
                var player = new Player(name, leader, goal, ping, roe, kia, enemy, honor);
                scoreBoard.RegisterOnlinePlayer(player);
            }
        }

        return scoreBoard;
    }

    private static MatchInformation GetMatchInformation(string response)
    {
        var currentRoundString = GetKeyValueByRegexOrDefault(response, "current_round\\0(.*?)\\0");
        var currentAndTotalRound = string.IsNullOrWhiteSpace(currentRoundString)
            ? new[] { "0", "0" }
            : currentRoundString.Split("/");
        var currentRound = int.Parse(currentAndTotalRound[0]);
        var totalRounds = int.Parse(currentAndTotalRound[1]);

        var mapName = GetKeyValueByRegexOrDefault(response, "mapname\\0(.*?)\\0") ?? "N/A";
        var missionTime = GetKeyValueByRegexOrDefault(response, "mission_time\\0(.*?)\\0") ?? "N/A";

        return new MatchInformation(mapName, missionTime, currentRound, totalRounds);
    }

    private static string GetHostname(string response)
    {
        var hostnameWithWeirdChars = GetKeyValueByRegexOrDefault(response, "\ahostname\\0(.*?)\\0");
        if (hostnameWithWeirdChars == null)
        {
            return "N/A";
        }
        //used for colors in the hostname, TODO easy way to remove unsupported chars?
        var weirdCharLength = 4;

        return hostnameWithWeirdChars.Substring(weirdCharLength).Trim();
    }

    private static string? GetKeyValueByRegexOrDefault(string input, string regexExpression)
    {
        var match = Regex.Match(input, regexExpression);
        if (match.Success)
        {
            var groups = match.Groups;
            return groups[1].ToString();
        }

        return null;
    } 
}