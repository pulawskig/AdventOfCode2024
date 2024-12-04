using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public partial class Day04
{
    public static readonly SearchValues<char> EolSearchValues =
        SearchValues.Create(['\n', '\r']);
    
    public partial class Part1
    {
        private readonly Example _example = new(
            """
            MMMSXXMASM
            MSAMXMSMSA
            AMXSXMAAMM
            MSAMASMSMX
            XMASAMXAMM
            XXAMMXXAMA
            SMSMSASXSS
            SAXAMASAAA
            MAMMMXMMMM
            MXMXAXMASX
            
            """, 18);
        
        public long Solve(List<string> input)
        {
            var span1 = "XMAS".AsSpan();
            var span2 = "SAMX".AsSpan();
            var count = 0L;

            foreach (var line in input)
            {
                var lineSpan = line.AsSpan();
                count += lineSpan.Count(span1);
                count += lineSpan.Count(span2);
            }

            return count;
        }

        public List<string> Parse(string input)
        {
            List<string> result = new();
            
            var span = input.AsSpan();
            var eolLength = span.IndexOfAny(EolSearchValues);
            var lineLength = eolLength + 1;
            
            if (span[lineLength..].IndexOfAnyExcept(EolSearchValues) > 0)
            {
                lineLength++;
            }

            var limit = Math.Ceiling(1d * input.Length / lineLength);
            for (var i = 0; i < limit; i++)
            {
                var startIndex = i * lineLength;

                if (startIndex + eolLength > input.Length) break;
                
                var str = span[startIndex..(startIndex + eolLength)].ToString();
                result.Add(str);
            }
            
            var lineCount = result.Count;
            var sb = new StringBuilder();
            for (var i = 0; i < eolLength; i++)
            {
                for (var j = 0; j < lineCount; j++)
                {
                    var index = j * lineLength + i;
                    if (index > input.Length) break;
                    sb.Append(span[index]);
                }
                result.Add(sb.ToString());
                sb.Clear();
            }

            var sb2 = new StringBuilder();
            for (var i = 0; i < eolLength + lineCount - 1; i++)
            {
                var z1 = i < eolLength ? 0 : i - eolLength + 1;
                var z2 = i < lineCount ? 0 : i - lineCount + 1;
                
                for (var j = i - z2; j >= z1; j--)
                {
                    sb.Append(span[j * lineLength + i - j]);
                    sb2.Append(span[(lineCount - j - 1) * lineLength + i - j]);
                }
                
                result.Add(sb.ToString());
                result.Add(sb2.ToString());
                sb.Clear();
                sb2.Clear();
            }

            return result;
        }
    }
    
    public partial class Part2
    {
        private readonly Example _example = new(
            """
            MMMSXXMASM
            MSAMXMSMSA
            AMXSXMAAMM
            MSAMASMSMX
            XMASAMXAMM
            XXAMMXXAMA
            SMSMSASXSS
            SAXAMASAAA
            MAMMMXMMMM
            MXMXAXMASX

            """, 9);
        
        [GeneratedRegex(@"(M.S.A.M.S|S.S.A.M.M|S.M.A.S.M|M.M.A.S.S)")]
        private static partial Regex _regex();
        
        public long Solve(List<string> input)
        {
            return input.Count(_regex().IsMatch);
        }
        
        public List<string> Parse(string input)
        {
            List<string> result = new();
            
            var span = input.Trim().AsSpan();
            var eolLength = span.IndexOfAny(EolSearchValues);
            var lineLength = eolLength + 1;
            
            if (span[lineLength..].IndexOfAnyExcept(EolSearchValues) > 0)
            {
                lineLength++;
            }

            var sb = new StringBuilder();
            var end = Math.Ceiling(1d * input.Length / lineLength) - 1;
            
            for (var i = 1; i < eolLength - 1; i++)
            {
                for (var j = 1; j < end; j++)
                {
                    var index = (j - 1) * lineLength + i - 1;
                    sb.Append(span[index..(index + 3)]);

                    index += lineLength;
                    sb.Append(span[(index)..(index + 3)]);

                    index += lineLength;
                    sb.Append(span[index..(index + 3)]);
                    
                    result.Add(sb.ToString());
                    sb.Clear();
                }
            }

            return result;
        }
    }
}