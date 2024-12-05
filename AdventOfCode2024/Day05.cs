using AdventOfCode2024.Tools;

namespace AdventOfCode2024;

public partial class Day05
{
    private const string ExampleString = """
                                         47|53
                                         97|13
                                         97|61
                                         97|47
                                         75|29
                                         61|13
                                         75|53
                                         29|13
                                         97|29
                                         53|29
                                         61|53
                                         97|53
                                         61|29
                                         47|13
                                         75|47
                                         97|75
                                         47|61
                                         75|61
                                         47|29
                                         75|13
                                         53|13

                                         75,47,61,53,29
                                         97,61,53,29,13
                                         75,29,13
                                         75,97,47,61,53
                                         61,13,29
                                         97,13,75,29,47

                                         """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 143);

        public long Solve(Manual input)
        {
            var sum = 0L;
            foreach (var update in input.Updates)
            {
                if (IsCorrect(update, input.Rules))
                {
                    sum += update[update.Length / 2];
                }
            }

            return sum;
        }

        public Manual Parse(string input) => Day05.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 123);

        public long Solve(Manual input)
        {
            var sum = 0L;
            var comparer = new PageComparer(input.Rules);

            foreach (var update in input.Updates)
            {
                if (IsCorrect(update, input.Rules)) continue;

                var span = update.AsSpan();
                span.Sort(comparer);
                sum += span[span.Length / 2];
            }

            return sum;
        }

        public Manual Parse(string input) => Day05.Parse(input);

        private class PageComparer(bool[,] rules) : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                if (rules[a, b]) return -1;
                if (rules[b, a]) return 1;
                return 0;
            }
        }
    }

    public static Manual Parse(string input)
    {
        var rules = new bool[100, 100];
        var updates = new List<int[]>();
        
        foreach (var line in input.SplitToLines())
        {
            var span = line.AsSpan();
            if (span.IndexOf('|') >= 0)
            {
                var split = span.Split('|');
                split.MoveNext();
                var first = int.Parse(span[split.Current]);
                split.MoveNext();
                var second = int.Parse(span[split.Current]);

                rules[first, second] = true;
            }
            else if (span.IndexOf(',') >= 0)
            {
                var update = new List<int>();
                var split = span.Split(',');
                foreach (var range in split)
                {
                    update.Add(int.Parse(span[range]));
                }

                updates.Add(update.ToArray());
            }
        }

        return new Manual(rules, updates.ToArray());
    }

    public static bool IsCorrect(int[] update, bool[,] rules)
    {
        for (var i = 0; i < update.Length; i++)
        for (var j = i + 1; j < update.Length; j++)
        {
            if (!rules[update[i], update[j]])
            {
                return false;
            }
        }

        return true;
    }

    public record struct Manual(bool[,] Rules, int[][] Updates);
}