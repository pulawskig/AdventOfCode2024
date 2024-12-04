using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day04
{
    private string input;
    private AdventOfCode2024.Day04.Part1 part1;
    private AdventOfCode2024.Day04.Part2 part2;
    private List<string> parsed1;
    private List<string> parsed2;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day04/input.txt");

        part1 = new AdventOfCode2024.Day04.Part1();
        part2 = new AdventOfCode2024.Day04.Part2();

        parsed1 = part1.Parse(input);
        parsed2 = part2.Parse(input);
    }

    [Benchmark]
    public List<string> Part1_Parse() => part1.Parse(input);

    [Benchmark]
    public long Part1_Solve() => part1.Solve(parsed1);

    [Benchmark]
    public List<string> Part2_Parse() => part2.Parse(input);

    [Benchmark]
    public long Part2_Solve() => part2.Solve(parsed2);
}