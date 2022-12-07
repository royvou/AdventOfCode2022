namespace AdventOfCode;

public abstract class Day : BaseDay
{
    protected readonly string _input;
    private readonly string _rawInput;

    protected Day()
    {
        _rawInput = File.ReadAllText(InputFilePath);
        _input = _rawInput.TrimEnd();
    }

    public override string InputFilePath => Path.Combine(InputFileDirPath, $"input.{InputFileExtension.TrimStart('.')}");


    protected override string InputFileDirPath => $"Day{CalculateIndex():D2}";
}