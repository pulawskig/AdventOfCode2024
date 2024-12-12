using AdventOfCode2024.Tools;

namespace AdventOfCode2024;

public partial class Day10
{
    private const string ExampleString = """
                                          0123
                                          1234
                                          8765
                                          9876
                                          
                                          """;
    
    private const string ExampleString2 = """
                                          89010123
                                          78121874
                                          87430965
                                          96549874
                                          45678903
                                          32019012
                                          01329801
                                          10456732
                                           
                                          """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 1);
        private readonly Example _example2 = new(ExampleString2, 36);

        public long Solve(Data input)
        {
            return input.Trailheads
                .AsParallel()
                .Sum(x => DFS(x, input.Map));
        }

        private int DFS(Point start, int[,] map)
        {
            var height = map.GetLength(0);
            var width = map.GetLength(1);
            var score = 0;
            
            var visited = new bool[height, width];
            
            var queue = new Queue<Point>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();

                if (visited[p.Y, p.X]) continue;
                
                var v = map[p.Y, p.X];

                visited[p.Y, p.X] = true;

                var next = p with { X = p.X - 1 };
                if (next.X >= 0 && !visited[next.Y, next.X] && map[next.Y, next.X] - v == 1)
                    queue.Enqueue(next);
                
                next = p with { X = p.X + 1 };
                if (next.X < width && !visited[next.Y, next.X] && map[next.Y, next.X] - v == 1)
                    queue.Enqueue(next);
                
                next = p with { Y = p.Y - 1 };
                if (next.Y >= 0 && !visited[next.Y, next.X] && map[next.Y, next.X] - v == 1)
                    queue.Enqueue(next);
                
                next = p with { Y = p.Y + 1 };
                if (next.Y < height && !visited[next.Y, next.X] && map[next.Y, next.X] - v == 1)
                    queue.Enqueue(next);

                if (v == 9) score++;
            }

            return score;
        }

        public Data Parse(string input) => Day10.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString2, 81);

        public long Solve(Data input)
        {
            var height = input.Map.GetLength(0);
            var width = input.Map.GetLength(1);

            return input.Trailheads
                .AsParallel()
                .Sum(x => DFS(x, input.Map, new bool[height, width], width, height));
        }

        public Data Parse(string input) => Day10.Parse(input);

        private long DFS(Point current, int[,] map, bool[,] visited, int width, int height)
        {
            if (visited[current.Y, current.X]) return 0;

            var value = map[current.Y, current.X];
            if (value == 9) return 1;
            
            visited[current.Y, current.X] = true;

            var sum = 0L;

            var next = current with { X = current.X - 1 };
            if (next.X >= 0 && !visited[next.Y, next.X] && map[next.Y, next.X] - value == 1)
                sum += DFS(next, map, (bool[,])visited.Clone(), width, height);
            
            next = current with { X = current.X + 1 };
            if (next.X < width && !visited[next.Y, next.X] && map[next.Y, next.X] - value == 1)
                sum += DFS(next, map, (bool[,])visited.Clone(), width, height);
                
            next = current with { Y = current.Y - 1 };
            if (next.Y >= 0 && !visited[next.Y, next.X] && map[next.Y, next.X] - value == 1)
                sum += DFS(next, map, (bool[,])visited.Clone(), width, height);
                
            next = current with { Y = current.Y + 1 };
            if (next.Y < height && !visited[next.Y, next.X] && map[next.Y, next.X] - value == 1)
                sum += DFS(next, map, (bool[,])visited.Clone(), width, height);

            return sum;
        }
    }
    
    public static Data Parse(string input)
    {
        var lines = input.SplitToLines(true).ToArray();
        var map = new int[lines.Length, lines[0].Length];
        var trailheads = new List<Point>();

        for (var y = 0; y < lines.Length; y++)
        {
            var span = lines[y].AsSpan();
            for (var x = 0; x < lines[y].Length; x++)
            {
                var current = span[x] - '0';
                map[y, x] = current;

                if (current == 0)
                {
                    trailheads.Add(new Point(x, y));
                }
            }
        }

        return new Data(map, trailheads.ToArray());
    }
        
    public record struct Point(int X, int Y);

    public record struct Data(int[,] Map, Point[] Trailheads);
}