using System.Text;

namespace AdventOfCode2024;

public partial class Day09
{
    private const string ExampleString = """
                                         2333133121414131402
                                         
                                         """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 1928);

        public long Solve(int[] input)
        {
            var span = input.AsSpan();

            while (true)
            {
                var idxEmpty = span.IndexOf(-1);
                var idxFilled = span.LastIndexOfAnyExcept(-1);

                if (idxEmpty > idxFilled) break;

                span[idxEmpty] = span[idxFilled];
                span[idxFilled] = -1;
            }

            var sum = 0L;
            for (var i = 0; i < span.Length && span[i] >= 0; i++)
            {
                sum += i * span[i];
            }

            return sum;
        }

        public int[] Parse(string input)
        {
            var span = input.Trim().AsSpan();
            var result = new List<int>();
            var id = 0;

            for (var i = 0; i < span.Length; i++)
            {
                var current = span[i] - '0';
                var insert = i % 2 == 0 ? id++ : -1;
                
                for (var j = 0; j < current; j++)
                    result.Add(insert);
            }

            return result.ToArray();
        }
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 2858);

        public long Solve(List<Block> input)
        {
            var emptyBlocks = input.Where(x => x.Id < 0).ToArray();
            var filledBlocks = input.Where(x => x.Id >= 0).Reverse().ToArray();
            
            foreach (var block in filledBlocks)
            {
                var empty = emptyBlocks
                    .Where(e => e.Index < block.Index)
                    .FirstOrDefault(e => e.Length >= block.Length);
                
                if (empty is null) continue;

                if (empty.Length == block.Length)
                {
                    (empty.Index, block.Index) = (block.Index, empty.Index);
                }
                else
                {
                    
                    empty.Length -= block.Length;

                    foreach (var otherBlocks in input.Where(x => x.Index >= empty.Index))
                    {
                        otherBlocks.Index++;
                    }
                    
                    var newEmpty = new Block
                    {
                        Id = -1,
                        Length = block.Length,
                        Index = block.Index,
                    };

                    block.Index = empty.Index - 1;
                    input.Add(newEmpty);
                    
                }
            }

            return input
                .OrderBy(x => x.Index)
                .SelectMany(x => Enumerable.Repeat(x.Id, x.Length))
                .Index()
                .Where(x => x.Item >= 0)
                .Sum(x => (long)x.Index * x.Item);

        }

        public List<Block> Parse(string input)
        {
            var span = input.Trim().AsSpan();
            var result = new List<Block>();
            var id = 0;
        
            for (var i = 0; i < span.Length; i++)
            {
                var current = span[i] - '0';
                var insert = i % 2 == 0 ? id++ : -1;

                result.Add(new Block
                {
                    Id = insert,
                    Length = current,
                    Index = i
                });
            }
        
            return result;
        }

        public class Block
        {
            public int Id { get; init; }
            public int Length { get; set; }
            public int Index { get; set; }
        }
    }
}