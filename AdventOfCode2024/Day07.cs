using AdventOfCode2024.Tools;
using Point = (int X, int Y);

namespace AdventOfCode2024;

public partial class Day07
{
    private const string ExampleString = """
                                         190: 10 19
                                         3267: 81 40 27
                                         83: 17 5
                                         156: 15 6
                                         7290: 6 8 6 15
                                         161011: 16 10 13
                                         192: 17 8 14
                                         21037: 9 7 18 13
                                         292: 11 6 16 20
                                         
                                         """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 3749);

        public long Solve((long Target, int[] Operands)[] input)
        {
            return input
                .AsParallel()
                .Where(x => IsSolvable(x.Target, 0, x.Operands))
                .Sum(x => x.Target);
        }

        private bool IsSolvable(long target, long current, int[] operands)
        {
            if (operands.Length == 0) return current == target;
            if (current > target) return false;
        
            var nextOperands = operands[1..];
            var currentOperand = operands[0];
            return IsSolvable(target, current + currentOperand, nextOperands) 
                   || IsSolvable(target, current == 0 ? currentOperand : current * currentOperand, nextOperands);
        }

        public (long Target, int[] Operands)[] Parse(string input) => Day07.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 11387);

        public long Solve((long Target, int[] Operands)[] input)
        {
            return input
                .AsParallel()
                .Where(x => IsSolvable(x.Target, 0, x.Operands))
                .Sum(x => x.Target);
        }

        public (long Target, int[] Operands)[] Parse(string input) => Day07.Parse(input);
        
        private bool IsSolvable(long target, long current, int[] operands)
        {
            if (operands.Length == 0) return current == target;
            if (current > target) return false;
        
            var nextOperands = operands[1..];
            var currentOperand = operands[0];
            return IsSolvable(target, current + currentOperand, nextOperands) 
                   || IsSolvable(target, current == 0 ? currentOperand : current * currentOperand, nextOperands)
                   || IsSolvable(target, current == 0 ? currentOperand : current * (long)Math.Pow(10, Math.Ceiling(Math.Log10(currentOperand + 1))) + currentOperand, nextOperands);
        }
    }
    
    public static (long Target, int[] Operands)[] Parse(string input)
    {
        var lines = input.SplitToLines(true).ToArray();
        var result = new (long Target, int[] Operands)[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var span = lines[i].AsSpan();
            var count = span.Count(' ');
            var split = span.Split(' ');
            var target = 0L;
            var operands = new int[count];
            var j = 0;
                
            foreach (var range in split)
            {
                if (j == 0)
                {
                    target = long.Parse(span[range.Start..(range.End.Value - 1)]);
                }
                else
                {
                    operands[j - 1] = int.Parse(span[range]);
                }

                j++;
            }
                
            result[i] = (target, operands);
        }
            
        return result;
    }
}