using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day03
{
    private string input;
    private AdventOfCode2024.Day03.Part1 part1;
    private AdventOfCode2024.Day03.Part2 part2;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day03/input.txt");

        part1 = new AdventOfCode2024.Day03.Part1();
        part2 = new AdventOfCode2024.Day03.Part2();
    }

    [Benchmark]
    public long Part1() => part1.Solve(input);

    [Benchmark]
    public long Part2() => part2.Solve(input);
}