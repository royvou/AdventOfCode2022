using System.Threading.Tasks.Sources;

namespace AdventOfCode.Day08;

public class Day08 : Day
{
    public Day08()
    {
        Map = ParseMap(_input);
    }

    private double[][] Map { get; }

    public override ValueTask<string> Solve_1() => new(GetPoints(Map, IsVisible).Count().ToString());

    private IEnumerable<(int X, int Y)> GetPoints(double[][] map, Func<double[][], int, int, bool> isVisible)
    {
        for (int x = 0; x < map.Length; x++)
        {
            var line = map[x];
            for (int y = 0; y < line.Length; y++)
            {
                if (isVisible(map, x, y))
                {
                    yield return (x, y);
                }
            }
        }
    }

    private bool IsVisible(double[][] map, int x, int y)
    {
        if (IsEdgeOfMap(map, x, y))
        {
            return true;
        }

        // Left
        return Enumerable.Range(0, x).All(curX => map[y][curX] < map[y][x]) ||
               // Top
               Enumerable.Range(0, y).All(curY => map[curY][x] < map[y][x]) ||
               // Right
               Enumerable.Range(x + 1, map[0].Length - x - 1).All(curX => map[y][curX] < map[y][x]) ||
               // Bottom
               Enumerable.Range(y + 1, map.Length - y - 1).All(currY => map[currY][x] < map[y][x]);
    }

    private static bool IsEdgeOfMap(double[][] map, int x, int y)
        => x == 0 || y == 0 || x == (map.Length - 1) || y == (map[0].Length - 1);

    private double[][] ParseMap(string input)
        => input.SplitNewLine().Select(x => x.ToCharArray().Select(char.GetNumericValue).ToArray()).ToArray();

    public override ValueTask<string> Solve_2()
    {
        return new(GetPointScore(Map, GetScore).MaxBy(x => x.Score).Score.ToString());
    }


    private static IEnumerable<(int X, int Y, int Score)> GetPointScore(double[][] map, Func<double[][], int, int, int> getScore)
    {
        // We can ignore the edges so start at +1
        for (int x = 1; x < map.Length - 2; x++)
        {
            var line = map[x];
            for (int y = 1; y < line.Length - 2; y++)
            {
                yield return (x, y, getScore(map, x, y));
            }
        }
    }

    private int GetScore(double[][] map, int x, int y)
    {
        // array[y][x]
        return (Enumerable.Range(1, x).TakeWhile(curX => map[y][x - curX] < map[y][x] && !IsEdgeOfMap(map, x - curX, y)).Count() + 1) *
               // Top
               (Enumerable.Range(1, y).TakeWhile(curY => map[y - curY][x] < map[y][x] && !IsEdgeOfMap(map, x, y - curY)).Count() + 1) *
               // Right
               (Enumerable.Range(1, map.Length - x - 1).TakeWhile(curX => map[y][x + curX] < map[y][x] && !IsEdgeOfMap(map, x + curX, y)).Count() + 1) *
               // Bottom
               (Enumerable.Range(1, map.Length - y - 1).TakeWhile(curY => map[y + curY][x] < map[y][x] && !IsEdgeOfMap(map, x, y + curY)).Count() + 1);
    }
}