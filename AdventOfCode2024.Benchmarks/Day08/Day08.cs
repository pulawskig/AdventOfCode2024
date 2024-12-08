using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day08
{
    private string input;
    private AdventOfCode2024.Day08.Part1 part1;
    private AdventOfCode2024.Day08.Part2 part2;
    private AdventOfCode2024.Day08.Scan parsed;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day08/input.txt");

        part1 = new AdventOfCode2024.Day08.Part1();
        part2 = new AdventOfCode2024.Day08.Part2();

        parsed = AdventOfCode2024.Day08.Parse(input);
    }

    [Benchmark]
    public AdventOfCode2024.Day08.Scan Parse() => AdventOfCode2024.Day08.Parse(input);
    
    [Benchmark]
    public long Part1() => part1.Solve(parsed);
    
    [Benchmark]
    public long Part2() => part2.Solve(parsed);
}