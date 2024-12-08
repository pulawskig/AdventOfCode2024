using AdventOfCode2024.Tools;
using Point = (int X, int Y);

namespace AdventOfCode2024;

public partial class Day08
{
    private const string ExampleString = """
                                         ............
                                         ........0...
                                         .....0......
                                         .......0....
                                         ....0.......
                                         ......A.....
                                         ............
                                         ............
                                         ........A...
                                         .........A..
                                         ............
                                         ............
                                         
                                         """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 14);

        public long Solve(Scan input)
        {
            var found = new HashSet<Point>();
            foreach (var frequency in input.Frequencies)
            {
                for (var i = 0; i < frequency.Value.Count; i++)
                for (var j = i + 1; j < frequency.Value.Count; j++)
                {
                    var p1 = frequency.Value[i];
                    var p2 = frequency.Value[j];
                    
                    Point vector = (p2.X - p1.X, p2.Y - p1.Y);
                    Point anti1 = (p1.X - vector.X, p1.Y - vector.Y);
                    Point anti2 = (p1.X + 2 * vector.X, p1.Y + 2 * vector.Y);
                    
                    if (anti1.X >= 0 && anti1.X < input.Width && anti1.Y >= 0 && anti1.Y < input.Height)
                    {
                        found.Add(anti1);
                    }
                    
                    if (anti2.X >= 0 && anti2.X < input.Width && anti2.Y >= 0 && anti2.Y < input.Height)
                    {
                        found.Add(anti2);
                    }
                }
            }

            return found.Count;
        }

        public Scan Parse(string input) => Day08.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 34);

        public long Solve(Scan input)
        {
            var found = new HashSet<Point>();
            foreach (var frequency in input.Frequencies)
            {
                for (var i = 0; i < frequency.Value.Count; i++)
                for (var j = i + 1; j < frequency.Value.Count; j++)
                {
                    var p1 = frequency.Value[i];
                    var p2 = frequency.Value[j];
                    
                    Point vector = (p2.X - p1.X, p2.Y - p1.Y);

                    found.Add(p1);
                    
                    for (var k = 1;; k++)
                    {
                        Point anti = (p1.X - k * vector.X, p1.Y - k * vector.Y);
                        if (anti.X < 0 || anti.X >= input.Width || anti.Y < 0 || anti.Y >= input.Height)
                        {
                            break;
                        }
                        
                        found.Add(anti);
                    }
                    
                    for (var k = 1;; k++)
                    {
                        Point anti = (p1.X + k * vector.X, p1.Y + k * vector.Y);
                        if (anti.X < 0 || anti.X >= input.Width || anti.Y < 0 || anti.Y >= input.Height)
                        {
                            break;
                        }
                        
                        found.Add(anti);
                    }
                }
            }

            return found.Count;
        }

        public Scan Parse(string input) => Day08.Parse(input);
    }
    
    public static Scan Parse(string input)
    {
        var result = new Dictionary<char, List<Point>>();
        var lines = input.SplitToLines(true).ToArray();
            
        for (var i = 0; i < lines.Length; i++)
        {
            var span = lines[i].AsSpan();
            var idxSum = -1;
            while (true)
            {
                var idx = span.IndexOfAnyExcept('.');
                if (idx < 0) break;
                    
                var ch = span[idx];
                    
                idxSum += idx + 1;
                if (result.TryGetValue(ch, out var list))
                {
                    list.Add((idxSum, i));
                }
                else
                {
                    result.Add(ch, [(idxSum, i)]);
                }
                    
                span = span[(idx + 1)..];
            }
        }

        return new Scan(lines[0].Length, lines.Length, result);
    }

    public record struct Scan(int Width, int Height, Dictionary<char, List<Point>> Frequencies);
}