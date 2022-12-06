namespace AdventOfCode;

public abstract class Day : BaseDay
{
    protected readonly string _input;

    protected Day()
    {
        _input = File.ReadAllText(InputFilePath);
    }
}