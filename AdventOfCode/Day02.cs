namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private readonly HashSet<Game> Player1Wins = new HashSet<Game>()
    {
        new(Action.Rock, Action.Scissor),
        new(Action.Scissor, Action.Paper),
        new(Action.Paper, Action.Rock),
    };

    public override ValueTask<string> Solve_1() => new(_input.SplitNewLine().Select(x =>
    {
        var values = x.Split(" ");
        var player1 = StringToAction(values[0]);
        var player2 = StringToAction(values[1]);
        return new Game(player1, player2);
    }).Select(x =>
    {
        if (x.Player1 == x.Player2)
        {
            return (int)x.Player2 + 3;
        }
        else if (Player1Wins.Contains(x))
        {
            //P1 Win
            return (int)x.Player2;
        }
        else
        {
            //P2 win
            return (int)x.Player2 + 6;
        }
    }).Sum().ToString());

    public override ValueTask<string> Solve_2() => new(_input.SplitNewLine().Select(x =>
    {
        var values = x.Split(" ");
        var player1 = StringToAction(values[0]);
        var gameType = StringToGameType(values[1]);
        var player2 = GetPlayer2Action(player1, gameType);
        return new Game(player1, player2);
    }).Select(x =>
    {
        if (x.Player1 == x.Player2)
        {
            return (int)x.Player2 + 3;
        }
        else if (Player1Wins.Contains(x))
        {
            //P1 Win
            return (int)x.Player2;
        }
        else
        {
            //P2 win
            return (int)x.Player2 + 6;
        }
    }).Sum().ToString());

    private static readonly IDictionary<Action, Action> LoseDictionary = new Dictionary<Action, Action>()
    {
        [Action.Rock] = Action.Scissor,
        [Action.Scissor] = Action.Paper,
        [Action.Paper] = Action.Rock,
    };

    private static readonly  IDictionary<Action, Action> WinDictionary = LoseDictionary.ToDictionary(x => x.Value, y => y.Key);

    private Action GetPlayer2Action(Action player1, GameType gameType)
        => gameType switch
        {
            GameType.Draw => player1,
            GameType.Lose => LoseDictionary[player1],
            GameType.Win => WinDictionary[player1],
        };


    private Action StringToAction(string character) => character switch
    {
        "A" => Action.Rock,
        "B" => Action.Paper,
        "C" => Action.Scissor,

        "X" => Action.Rock,
        "Y" => Action.Paper,
        "Z" => Action.Scissor,
    };

    private GameType StringToGameType(string character) => character switch
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
    Lose
}

public record Game(Action Player1, Action Player2);