namespace AdventOfCode;

public class Day06 : Day
{
    public override ValueTask<string> Solve_1()
    {
        var inputArray = _input.ToArray();
        var result = Enumerable.Range(0, inputArray.Length - 3).First(x => inputArray[x..(x + 4)].GroupBy(x => x).Count() == 4) + 4;
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
        => throw new NotImplementedException();
}