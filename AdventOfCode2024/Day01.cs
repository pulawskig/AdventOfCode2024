namespace AdventOfCode2024;

public partial class Day01
{
    internal partial class Part1
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

            var lines = input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                left.Add(numbers[0]);
                right.Add(numbers[1]);
            }
            
            return (left, right);
        }
    }
    internal partial class Part2
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

            var lines = input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                left.Add(numbers[0]);

                if (right.ContainsKey(numbers[1]))
                {
                    right[numbers[1]]++;
                }
                else
                {
                    right.Add(numbers[1], 1);
                }
            }

            foreach (var number in left.Distinct())
            {
                right.TryAdd(number, 0);
            }
            
            return (left, right);
        }
    }
}