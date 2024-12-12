using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace AdventOfCode2024;

public partial class Day11
{
    private const string ExampleString = """
                                          125 17
                                          
                                          """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 55312);

        public long Solve(List<long> input) => Day11.Solve(input, 25);
        
        public List<long> Parse(string input) => Day11.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 65601038650482);

        public long Solve(List<long> input) => Day11.Solve(input, 75);

        public List<long> Parse(string input) => Day11.Parse(input);
    }
    
    public static long Solve(List<long> input, int blinkCount)
    {
        var counter = input.ToDictionary(x => x, _ => 1L);
        var resolver = new Dictionary<long, long[]>
        {
            { 0, [1] },
        };

        for (var i = 0; i < blinkCount; i++)
        {
            var counterCopy = new Dictionary<long, long>(counter);
            foreach (var stone in counterCopy.Keys)
            {
                counter[stone] -= counterCopy[stone];
                if (resolver.TryGetValue(stone, out var resolved))
                {
                    ApplyCalculation(resolved, counterCopy[stone], counter);
                    continue;
                }

                var next = CalculateNext(stone);
                resolver.Add(stone, next);
                ApplyCalculation(next, counterCopy[stone], counter);
            }
        }

        return counter.Sum(x => x.Value);
    }

    public static List<long> Parse(string input)
    {
        return input
            .Trim()
            .Split(' ')
            .Select(long.Parse)
            .ToList();
    }
    
    private static long[] CalculateNext(long stone)
    {
        if (stone == 0) return [1];
        var len = (int)Math.Floor(Math.Log10(stone) + 1);
        if (len % 2 == 0)
        {
            var divisor = (long)Math.Pow(10, len / 2);
            return [stone / divisor, stone % divisor];
        }

        return [stone * 2024];
    }

    private static void ApplyCalculation(long[] calculations, long stoneCount, Dictionary<long, long> counter)
    {
        foreach (var value in calculations)
        {
            if (!counter.TryAdd(value, stoneCount))
            {
                counter[value] += stoneCount;
            }
        }
    }
}