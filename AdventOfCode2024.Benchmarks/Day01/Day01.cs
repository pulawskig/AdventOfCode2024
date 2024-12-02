using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day01
{
    private string input;
    private AdventOfCode2024.Day01.Part1 part1;
    private AdventOfCode2024.Day01.Part2 part2;
    private (List<int> Left, List<int> Right) parse1;
    private (List<int> Left, Dictionary<int, int> Right) parse2;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day01/input.txt");
        
        part1 = new AdventOfCode2024.Day01.Part1();
        parse1 = part1.Parse(input);
        
        part2 = new AdventOfCode2024.Day01.Part2();
        parse2 = part2.Parse(input);
    }

    [Benchmark]
    public (List<int> Left, List<int> Right) Part1_Parse() => part1.Parse(input);

    [Benchmark]
    public (List<int> Left, Dictionary<int, int> Right) Part2_Parse() => part2.Parse(input);

    [Benchmark]
    public int Part1_Solve() => part1.Solve(parse1);

    [Benchmark]
    public long Part2_Solve() => part2.Solve(parse2);
}