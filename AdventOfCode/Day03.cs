namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new(_input.SplitNewLine().Select(ParseBag).Select(GetChar).Select(GetScore).Sum().ToString());

    private static Bag ParseBag(string input)
    {
        var inputCharArray = input.ToCharArray();
        return new Bag(inputCharArray[0..(inputCharArray.Length / 2)], inputCharArray[(inputCharArray.Length / 2)..]);
    }

    private double GetScore(char arg)
        => char.IsUpper(arg)
            ? arg - 64 + 26
            : arg - 96;

    private char GetChar(Bag bag)
    {
        var compartment1 = new HashSet<char>(bag.Compartment1);
        compartment1.IntersectWith(bag.Compartment2);
        return compartment1.First();
    }

    public override ValueTask<string> Solve_2() => new(_input.SplitNewLine().Chunk(3).Select(x =>
    {
        var line1 = x[0];
        var hashSet = new HashSet<char>(line1.ToCharArray());
        for (int i = 1; i < x.Length; i++)
        {
            hashSet.IntersectWith(x[i].ToCharArray());
        }

        return hashSet.First();
    }).Select(GetScore).Sum().ToString());
}

public record Bag(char[] Compartment1, char[] Compartment2);