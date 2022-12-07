namespace AdventOfCode.Day02;

public class Day02 : Day
{
    private static readonly IDictionary<Action, Action> Player2_LoseDictionary = new Dictionary<Action, Action>
    {
        [Action.Rock] = Action.Scissor,
        [Action.Scissor] = Action.Paper,
        [Action.Paper] = Action.Rock,
    };

    private static readonly IDictionary<Action, Action> Player2_WinDictionary = Player2_LoseDictionary.ToDictionary(x => x.Value, y => y.Key);


    public override ValueTask<string> Solve_1() => new(_input.SplitNewLine().Select(x =>
    {
        var values = x.Split(" ");
        var player1 = StringToAction(values[0]);
        var player2 = StringToAction(values[1]);
        return new Game(player1, player2);
    }).GroupBy(x => x).Select(games => CalculateGameScore(games.Key) * games.Count()).Sum().ToString());

    public override ValueTask<string> Solve_2() => new(_input.SplitNewLine().Select(x =>
    {
        var values = x.Split(" ");
        var player1 = StringToAction(values[0]);
        var gameType = StringToGameType(values[1]);
        var player2 = GetPlayer2Action(player1, gameType);
        return new Game(player1, player2);
    }).GroupBy(x => x).Select(games => CalculateGameScore(games.Key) * games.Count()).Sum().ToString());

    private static int CalculateGameScore(Game x)
        => x switch
        {
            _ when x.Player1 == x.Player2 => (int)x.Player2 + 3,
            _ when Player2_LoseDictionary[x.Player1] == x.Player2 => (int)x.Player2,
            _ => (int)x.Player2 + 6,
        };

    private static Action GetPlayer2Action(Action player1, GameType gameType)
        => gameType switch
        {
            GameType.Draw => player1,
            GameType.Lose => Player2_LoseDictionary[player1],
            GameType.Win => Player2_WinDictionary[player1],
        };


    private static Action StringToAction(string character) => character switch
    {
        "A" => Action.Rock,
        "B" => Action.Paper,
        "C" => Action.Scissor,

        "X" => Action.Rock,
        "Y" => Action.Paper,
        "Z" => Action.Scissor,
    };

    private static GameType StringToGameType(string character) => character switch
    {
        "X" => GameType.Lose,
        "Y" => GameType.Draw,
        "Z" => GameType.Win,
    };
}

public enum Action
{
    Rock = 1,
    Paper = 2,
    Scissor = 3,
}

public enum GameType
{
    Draw,
    Win,
    Lose,
}

public record Game(Action Player1, Action Player2);