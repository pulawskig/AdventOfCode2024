using AdventOfCode2024.Tools;
using Point = (int X, int Y);

namespace AdventOfCode2024;

public partial class Day06
{
    public enum Direction
    {
        Up, Right, Down, Left
    }
    
    private const string ExampleString = """
                                         ....#.....
                                         .........#
                                         ..........
                                         ..#.......
                                         .......#..
                                         ..........
                                         .#..^.....
                                         ........#.
                                         #.........
                                         ......#...
                                         
                                         """;

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 41);

        public long Solve((Point Start, bool[,] Board) input) => Visit(input).Count;
        
        public static HashSet<Point> Visit((Point Start, bool[,] Board) input)
        {
            var pos = input.Start;
            var direction = Direction.Up;
            var height = input.Board.GetLength(0);
            var width = input.Board.GetLength(1);
            
            var visited = new HashSet<(int, int)>();

            while (true)
            {
                visited.Add(pos);
                Point next = direction switch
                {
                    Direction.Up => (pos.X, pos.Y - 1),
                    Direction.Right => (pos.X + 1, pos.Y),
                    Direction.Down => (pos.X, pos.Y + 1),
                    Direction.Left => (pos.X - 1, pos.Y),
                };

                if (next.X < 0 || next.X >= width || next.Y < 0 || next.Y >= height)
                {
                    break;
                }

                if (input.Board[next.Y, next.X])
                {
                    direction = direction switch
                    {
                        Direction.Up => Direction.Right,
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up,
                    };
                }
                else
                {
                    pos = next;
                }
            }

            return visited;
        }

        public (Point Start, bool[,] Board) Parse(string input) => Day06.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, 6);
        
        public long Solve((Point Start, bool[,] Board) input)
        {
            var originalPath = Part1.Visit(input);
            var height = input.Board.GetLength(0);
            var width = input.Board.GetLength(1);

            return originalPath
                .AsParallel()
                .Where(x => x != input.Start)
                .Select(x => VisitPossible(input.Start, input.Board, width, height, x) ? 0 : 1)
                .Sum(x => x);
        }

        private bool VisitPossible(Point start, bool[,] board, int width, int height, Point newBlocked)
        {
            var pos = start;
            var direction = Direction.Up;
            var visited = new Dictionary<Point, Direction>();

            while (true)
            {
                visited.TryAdd(pos, direction);
                Point next = direction switch
                {
                    Direction.Up => (pos.X, pos.Y - 1),
                    Direction.Right => (pos.X + 1, pos.Y),
                    Direction.Down => (pos.X, pos.Y + 1),
                    Direction.Left => (pos.X - 1, pos.Y),
                };

                if (visited.TryGetValue(next, out var nextDirection) && nextDirection == direction)
                {
                    return false;
                }

                if (next.X < 0 || next.X >= width || next.Y < 0 || next.Y >= height)
                {
                    return true;
                }

                if (board[next.Y, next.X] || next == newBlocked)
                {
                    direction = direction switch
                    {
                        Direction.Up => Direction.Right,
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up,
                    };
                }
                else
                {
                    pos = next;
                }
            }
        }

        public (Point Start, bool[,] Board) Parse(string input) => Day06.Parse(input);
    }
    
    public static (Point Start, bool[,] Board) Parse(string input)
    {
        var lines = input.SplitToLines()
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();
        var result = new bool[lines.Length, lines[0].Length];
        var x = -1;
        var y = -1;
            
        for (var i = 0; i < lines.Length; i++)
        for (var j = 0; j < lines[i].Length; j++)
        {
            var point = lines[i][j];
            switch (point)
            {
                case '#':
                    result[i, j] = true;
                    break;
                case '^':
                    y = i;
                    x = j;
                    break;
            }
        }
            
        return ((x, y), result);
    }
}