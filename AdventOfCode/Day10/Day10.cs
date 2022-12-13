using System.Text;

namespace AdventOfCode.Day10;

public class Day10 : Day
{
    public Day10()
    {
        ParsedInput = ParseInput(_input);
    }

    private Action[] ParseInput(string input)
        => input.SplitNewLine().Select(Action.Parse).ToArray();

    public Action[] ParsedInput { get; }

    public override ValueTask<string> Solve_1()
    {
        return new(PlayBoard(ParsedInput).
            Select((x, i) => (Tick: i + 1, Value: x)).
            Where(a => (a.Tick - 20) % 40 == 0).
            Select(a => (Tick: a.Tick, Strength: a.Value * a.Tick)).
            Sum(x => x.Strength).ToString());
    }

    private IEnumerable<int> PlayBoard(Action[] actions)
    {
        int registery = 1;

        foreach (var item in actions)
        {
            switch (item.Type)
            {
                case ActionType.Noop:
                    yield return registery;
                    break;
                case ActionType.AddX:
                    yield return registery;
                    yield return registery;
                    registery += item.Amount.Value;
                    break;
            }
        }
    }

    public override ValueTask<string> Solve_2()
        => new(CreateMinitor(PlayBoard(ParsedInput).Select((x, i) => (Tick: i, Value: x))));
    
    const char darkPixel = ' ';
    const char litPixel = '█';
    
    private string CreateMinitor(IEnumerable<(int Tick, int Value)> tickLoop)
    {
        var sb = new StringBuilder();
        foreach (var tickloop in tickLoop)
        {
            var xPosition = tickloop.Tick % 40;
            
            if (xPosition == 0)
            {
                sb.Append(Environment.NewLine);
            }

            sb.Append(Math.Abs(tickloop.Value - xPosition) < 2 ? litPixel : darkPixel);

        }

        return sb.ToString();
    }
}

public enum ActionType
{
    Noop,
    AddX,
}

public record Action(ActionType Type, int? Amount = null)
{
    public static Action Parse(string line)
    {
        var splitted = line.SplitSpace();
        return splitted[0] switch
        {
            "noop" => new Action(ActionType.Noop),
            "addx" => new Action(ActionType.AddX, splitted[1].AsInt()),
        };
    }
}