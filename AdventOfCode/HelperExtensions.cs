namespace AdventOfCode;

public static class HelperExtensions
{
    private static readonly string NewLine = Environment.NewLine;
    private static readonly string DoubleNewLine = NewLine + NewLine;
    private static readonly string Space = " ";
    public static string[] SplitDoubleNewline(this string input) => input.Split(DoubleNewLine);

    public static string[] SplitNewLine(this string input) => input.Split(NewLine);

    public static string[] SplitSpace(this string input) => input.Split(Space);
    public static string[] SplitSpaceStripped(this string input) => input.Split(Space, StringSplitOptions.RemoveEmptyEntries);

    public static long AsLong(this string input) => long.Parse(input);
    public static int AsInt(this string input) => int.Parse(input);

}