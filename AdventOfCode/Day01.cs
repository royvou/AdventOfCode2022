namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new(_input.SplitDoubleNewline().Select(x => x.SplitNewLine().Select(x => x.AsLong()).Sum()).Max().ToString());

    public override ValueTask<string> Solve_2() => new(_input.SplitDoubleNewline().Select(x => x.SplitNewLine().Select(x => x.AsLong()).Sum()).OrderByDescending(x => x).Take(3).Sum().ToString());
}