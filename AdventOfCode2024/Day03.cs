using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public partial class Day03
{
    public partial class Part1
    {
        private readonly Example _example = new("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", 161);
        
        [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
        private static partial Regex _regex();
        
        public long Solve(string input)
        {
            var sum = 0L;
            
            foreach (Match match in _regex().Matches(input))
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);    
            }

            return sum;
        }
    }
    
    public partial class Part2
    {
        private readonly Example _example = new("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48);
        
        [GeneratedRegex(@"(mul\((\d+),(\d+)\)|do\(\)|don't\(\))")]
        private static partial Regex _regex();
        
        public long Solve(string input)
        {
            var sum = 0L;
            var active = true;
            var matches = _regex().Matches(input);

            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("mul"))
                {
                    if (!active) continue;
                    sum += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
                }
                else
                {
                    active = !match.Value.StartsWith("don't");
                }
            }

            return sum;
        }
    }
}