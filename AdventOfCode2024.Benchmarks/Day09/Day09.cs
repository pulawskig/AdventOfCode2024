using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode2024.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(false)]
public class Day09
{
    private string input;
    private AdventOfCode2024.Day09.Part1 part1;
    private AdventOfCode2024.Day09.Part2 part2;
    private int[] parsed1;
    private List<AdventOfCode2024.Day09.Part2.Block> parsed2;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText("Day09/input.txt");

        part1 = new AdventOfCode2024.Day09.Part1();
        parsed1 = part1.Parse(input);
        
        part2 = new AdventOfCode2024.Day09.Part2();
        parsed2 = part2.Parse(input);
    }

    [Benchmark]
    public int[] Part1_Parse() => part1.Parse(input);
    
    [Benchmark]
    public long Part1_Solve() => part1.Solve(parsed1);
    
    [Benchmark]
    public List<AdventOfCode2024.Day09.Part2.Block> Part2_Parse() => part2.Parse(input);
    
    [Benchmark]
    public long Part2_Solve() => part2.Solve(parsed2);
}