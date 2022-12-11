namespace AdventOfCode.Day09;

public class Day09 : Day
{
    public Day09()
    {
        ParsedInput = ParseInput(_input);
    }
    
    public Action[] ParsedInput { get; }

    private Action[] ParseInput(string input)
        => input.SplitNewLine().Select(Action.Parse).ToArray();

    public override ValueTask<string> Solve_1()
        => new(PlayBoard(Board.Init(1), ParsedInput).seenPositions.Count.ToString());


    private (Board board, HashSet<Point> seenPositions) PlayBoard(Board board, Action[] actions)
    {
        HashSet<Point> seenPositions = new()
        {
            new Point(0, 0),
        };
        foreach (var action in actions)
        {
            for (var commandAmount = 0; commandAmount < action.Amount; commandAmount++)
            {
                // Move H
                board.PosH = action.Direction switch
                {
                    Direction.Down => board.PosH with { Y = board.PosH.Y - 1, },
                    Direction.Up => board.PosH with { Y = board.PosH.Y + 1, },
                    Direction.Left => board.PosH with { X = board.PosH.X - 1, },
                    Direction.Right => board.PosH with { X = board.PosH.X + 1, },
                };

                // Move T
                for (var index = 0; index < board.Knots.Count; index++)
                {
                    // First item should follow head
                    var previousKnot = index == 0 ? board.PosH : board.Knots[index - 1];
                    var currentKnot = board.Knots[index];

                    var newCurrentKnot = currentKnot;
                    var difference = Difference(previousKnot, currentKnot);

                    if (Math.Abs(difference.Y) <= 1 && Math.Abs(difference.X) <= 1)
                    {
                        continue;
                    }

                    if (difference.X == 0 && Math.Abs(difference.Y) > 1)
                    {
                        newCurrentKnot = currentKnot with { Y = currentKnot.Y + Math.Sign(difference.Y), };
                    }
                    else if (difference.Y == 0 && Math.Abs(difference.X) > 1)
                    {
                        newCurrentKnot = currentKnot with { X = currentKnot.X + Math.Sign(difference.X), };
                    }
                    else
                    {
                        newCurrentKnot = currentKnot with { X = currentKnot.X + Math.Sign(difference.X), Y = currentKnot.Y + Math.Sign(difference.Y), };
                    }

                    board.Knots[index] = newCurrentKnot;
                }

                seenPositions.Add(board.Knots.Last());
            }
        }

        return (board, seenPositions);
    }

    public override ValueTask<string> Solve_2()
        => new(PlayBoard(Board.Init(9), ParsedInput).seenPositions.Count.ToString());


    public static Point Difference(Point PosH, Point PoshT)
        => new(PosH.X - PoshT.X, PosH.Y - PoshT.Y);
}

public class Board
{
    public Board(Point posH, List<Point> knots)
    {
        PosH = posH;
        Knots = knots;
    }

    public Point PosH { get; set; }
    public List<Point> Knots { get; }
    public static Board Init(int length) => new(new Point(0, 0), Enumerable.Range(0, length).Select(_ => new Point(0, 0)).ToList());
}

public record struct Point(int X, int Y);

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public record Action(Direction Direction, long Amount)
{
    public static Action Parse(string input)
    {
        var split = input.SplitSpace();
        return split[0] switch
        {
            "U" => new Action(Direction.Up, split[1].AsLong()),
            "D" => new Action(Direction.Down, split[1].AsLong()),
            "L" => new Action(Direction.Left, split[1].AsLong()),
            "R" => new Action(Direction.Right, split[1].AsLong()),
        };
    }
}