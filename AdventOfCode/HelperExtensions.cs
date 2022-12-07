namespace AdventOfCode;

public static class HelperExtensions
{
    private static readonly string NewLine = Environment.NewLine;
    private static readonly string NewLineVariant = "\n";
    private static readonly string Space = " ";
    public static string[] SplitDoubleNewline(this string input) => input.Split(new[] { NewLine + NewLine, NewLineVariant + NewLineVariant }, StringSplitOptions.None);

    public static string[] SplitNewLine(this string input) => input.Split(new[] { NewLine, NewLineVariant }, StringSplitOptions.None);

    public static string[] SplitSpace(this string input) => input.Split(Space);
    public static string[] SplitSpaceStripped(this string input) => input.Split(Space, StringSplitOptions.RemoveEmptyEntries);

    public static long AsLong(this string input) => long.Parse(input);
    public static int AsInt(this string input) => int.Parse(input);
}