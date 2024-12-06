using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day06
{
    private string input;
    private AdventOfCode2024.Day06.Part1 part1;
    private AdventOfCode2024.Day06.Part2 part2;
    private ((int X, int Y) Start, bool[,] Board) parsed;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day06/input.txt");

        part1 = new AdventOfCode2024.Day06.Part1();
        part2 = new AdventOfCode2024.Day06.Part2();

        parsed = AdventOfCode2024.Day06.Parse(input);
    }

    [Benchmark]
    public ((int X, int Y) Start, bool[,] Board) Parse() => AdventOfCode2024.Day06.Parse(input);
    
    [Benchmark]
    public long Part1() => part1.Solve(parsed);
    
    [Benchmark]
    public long Part2() => part2.Solve(parsed);
}