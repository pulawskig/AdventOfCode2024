using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day02
{
    private string input;
    private AdventOfCode2024.Day02.Part1 part1;
    private AdventOfCode2024.Day02.Part2 part2;
    private List<List<int>> parse;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day02/input.txt");
        
        part1 = new AdventOfCode2024.Day02.Part1();
        part2 = new AdventOfCode2024.Day02.Part2();
        parse = part1.Parse(input);
    }

    [Benchmark]
    public List<List<int>> Part1_Parse() => part1.Parse(input);

    [Benchmark]
    public List<List<int>> Part2_Parse() => part2.Parse(input);

    [Benchmark]
    public int Part1_Solve() => part1.Solve(parse);

    [Benchmark]
    public long Part2_Solve() => part2.Solve(parse);
}