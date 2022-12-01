namespace AdventOfCode;

public static class HelperExtensions
{
    private static readonly string NewLine = Environment.NewLine;
    private static readonly string DoubleNewLine = NewLine + NewLine;
    
    public static string[] SplitDoubleNewline(this string input) => input.Split(DoubleNewLine);
    
    public static string[] SplitNewLine(this string input) => input.Split(NewLine);


    public static long AsLong(this string input) => long.Parse(input);
}