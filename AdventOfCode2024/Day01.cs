using System.Buffers;
using AdventOfCode2024.Tools;

namespace AdventOfCode2024;

public partial class Day01
{
    public static readonly SearchValues<char> NumberSearchValues =
        SearchValues.Create(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
    
    public partial class Part1
    {
        private readonly Example _example = new(
            """
            3   4
            4   3
            2   5
            1   3
            3   9
            3   3
            """, 11);
        
        public int Solve((List<int> Left, List<int> Right) input)
        {
            return input.Left
                .OrderBy(x => x)
                .Zip(
                    input.Right.OrderBy(x => x),
                    (x, y) => Math.Abs(x - y))
                .Sum();
        }

        public (List<int> Left, List<int> Right) Parse(string input)
        {
            var left = new List<int>();
            var right = new List<int>();

            var span = input.AsSpan();

            var numbers = FindNumbers(span);
            for (var i = 0; i < numbers.Count; i++)
            {
                if (i % 2 == 0)
                {
                    left.Add(numbers[i]);
                }
                else
                {
                    right.Add(numbers[i]);
                }
            }
            
            return (left, right);
        }
    }
    public partial class Part2
    {
        private readonly Example _example = new(
            """
            3   4
            4   3
            2   5
            1   3
            3   9
            3   3
            """, 31);
        
        public long Solve((List<int> Left, Dictionary<int, int> Right) input)
        {
            return input.Left
                .Select(x => x * input.Right[x])
                .Sum();
        }
        
        public (List<int> Left, Dictionary<int, int> Right) Parse(string input)
        {
            var left = new List<int>();
            var right = new Dictionary<int, int>();
            
            var span = input.AsSpan();
            var numbers = FindNumbers(span);

            for (var i = 0; i < numbers.Count; i++)
            {
                if (i % 2 == 0)
                {
                    left.Add(numbers[i]);
                }
                else
                {
                    right.TryAdd(numbers[i], 0);
                    right[numbers[i]]++;
                }
            }

            foreach (var number in left.Distinct())
            {
                right.TryAdd(number, 0);
            }
            
            return (left, right);
        }
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