namespace AdventOfCode.Day06;

public class Day06 : Day
{
    private readonly char[] _inputArray;

    public Day06()
    {
        _inputArray = _input.ToArray();
    }

    public override ValueTask<string> Solve_1()
        => new(GetMarkerEndIndex(_inputArray, 4).ToString());

    private static int GetMarkerEndIndex(char[] inputArray, int length)
    {
        return Enumerable.Range(0, inputArray.Length - length).First(x => inputArray[x..(x + length)].GroupBy(x => x).Count() == length) + length;
    }

    public override ValueTask<string> Solve_2()
        => new(GetMarkerEndIndex(_inputArray, 14).ToString());
}