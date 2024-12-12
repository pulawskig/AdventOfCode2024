using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day10
{
    private string input;
    private AdventOfCode2024.Day10.Part1 part1;
    private AdventOfCode2024.Day10.Part2 part2;
    private AdventOfCode2024.Day10.Data parsed;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day10/input.txt");

        parsed = AdventOfCode2024.Day10.Parse(input);
        part1 = new AdventOfCode2024.Day10.Part1();
        part2 = new AdventOfCode2024.Day10.Part2();
    }

    [Benchmark]
    public AdventOfCode2024.Day10.Data Parse() => AdventOfCode2024.Day10.Parse(input);
    
    [Benchmark]
    public long Part1() => part1.Solve(parsed);
    
    [Benchmark]
    public long Part2() => part2.Solve(parsed);
}