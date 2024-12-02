using System.Buffers;
using AdventOfCode2024.Tools;

namespace AdventOfCode2024;

public partial class Day02
{
    public static readonly SearchValues<char> NumberSearchValues =
        SearchValues.Create(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
    
    public partial class Part1
    {
        private readonly Example _example = new(
            """
            7 6 4 2 1
            1 2 7 8 9
            9 7 6 2 1
            1 3 2 4 5
            8 6 4 4 1
            1 3 6 7 9
            """, 2);
        
        public int Solve(List<List<int>> input)
        {
            return input.Count(IsLineSafe);
        }

        public List<List<int>> Parse(string input) => Day02.Parse(input);

        private bool IsLineSafe(List<int> line)
        {
            var ascending = line[0] <= line[^1];
            
            for (var i = 1; i < line.Count; i++)
            {
                var diff = line[i] - line[i - 1];
                if ((ascending && diff is >= 1 and <= 3) || (!ascending && diff is <= -1 and >= -3))
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }

    public partial class Part2
    {
        private readonly Example _example = new(
            """
            7 6 4 2 1
            1 2 7 8 9
            9 7 6 2 1
            1 3 2 4 5
            8 6 4 4 1
            1 3 6 7 9
            """, 4);
        
        public int Solve(List<List<int>> input)
        {
            return input.Count(IsLineSafe);
        }

        public List<List<int>> Parse(string input) => Day02.Parse(input);
        
        private bool IsLineSafe(List<int> line)
        {
            var tolerate = true;
            var ascending = line[0] <= line[^1];
            var lastValue = line[0];
            var success = true;
            
            for (var i = 1; i < line.Count; i++)
            {
                var diff = line[i] - lastValue;
                if ((ascending && diff is >= 1 and <= 3) || (!ascending && diff is <= -1 and >= -3))
                {
                    lastValue = line[i];
                    continue;
                }

                if (tolerate)
                {
                    tolerate = false;
                    continue;
                }

                success = false;
                break;
            }

            if (success)
            {
                return true;
            }

            tolerate = true;
            lastValue = line[^1];
            ascending = !ascending;
            for (var i = line.Count - 2; i >= 0; i--)
            {
                var diff = line[i] - lastValue;
                if ((ascending && diff is >= 1 and <= 3) || (!ascending && diff is <= -1 and >= -3))
                {
                    lastValue = line[i];
                    continue;
                }

                if (tolerate)
                {
                    tolerate = false;
                    continue;
                }

                return false;
            }

            return true;
        }
    }
    
    public static List<List<int>> Parse(string input)
    {
        var numbers = new List<List<int>>();
        var lines = input.SplitToLines();
            
        foreach (var line in lines)
        {
            numbers.Add(FindNumbers(line.AsSpan()));
        }

        return numbers;
    }
    
    public static List<int> FindNumbers(ReadOnlySpan<char> input)
    {
        var numbers = new List<int>();
        
        while (true)
        {
            var startIndex = input.IndexOfAny(NumberSearchValues);
            if (startIndex == -1)
            {
                break;
            }

            input = input[startIndex..];
            var endIndex = input.IndexOfAnyExcept(NumberSearchValues);
            if (endIndex == -1)
            {
                numbers.AddFromSpan(input);
                break;
            }
                
            numbers.AddFromSpan(input[..endIndex]);
            input = input[endIndex..];
        }

        return numbers;
    }
}