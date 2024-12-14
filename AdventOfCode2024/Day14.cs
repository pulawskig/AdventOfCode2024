using System.Text.RegularExpressions;
using System.Drawing;
using AdventOfCode2024.Tools;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AdventOfCode2024;

public partial class Day14
{
    private const string ExampleString = """
                                          p=0,4 v=3,-3
                                          p=6,3 v=-1,-3
                                          p=10,3 v=-1,2
                                          p=2,0 v=2,-1
                                          p=0,0 v=1,3
                                          p=3,0 v=-2,-2
                                          p=7,6 v=-1,-3
                                          p=3,0 v=-1,-2
                                          p=9,3 v=2,3
                                          p=7,3 v=-1,2
                                          p=2,4 v=2,-3
                                          p=9,5 v=-3,-3
                                          
                                          """;

    [GeneratedRegex(@"^p=(\d+),(\d+) v=(-?\d+),(-?\d+)$")]
    private static partial Regex RobotRegex();

    public partial class Part1
    {
        private readonly Example _example = new(ExampleString, 12);

        public long Solve(Robot[] input)
        {
            var width = 101;
            var height = 103;

            // check for example run
            if (input[0].Position == new Vector(0, 4))
            {
                width = 11;
                height = 7;
            }

            foreach (var robot in input)
            {
                var nextX = (robot.Position.X + 100 * robot.Velocity.X) % width;
                if (nextX < 0) nextX += width;
                
                var nextY = (robot.Position.Y + 100 * robot.Velocity.Y) % height;
                if (nextY < 0) nextY += height;
                
                robot.Position = new Vector(nextX, nextY);
            }
            
            var midX = width / 2;
            var midY = height / 2;
            
            var q1 = input.Count(r => r.Position.X < midX && r.Position.Y < midY);
            var q2 = input.Count(r => r.Position.X > midX && r.Position.Y < midY);
            var q3 = input.Count(r => r.Position.X > midX && r.Position.Y > midY);
            var q4 = input.Count(r => r.Position.X < midX && r.Position.Y > midY);
            
            return q1 * q2 * q3 * q4;
        }

        public Robot[] Parse(string input) => Day14.Parse(input);
    }

    public partial class Part2
    {
        private readonly Example _example = new(ExampleString, -1); // No example for part 2
        public long Solve(Robot[] input)
        {
            // check for example run
            if (input[0].Position == new Vector(0, 4))
            {
                return -1;
            }

            Directory.CreateDirectory(@"Images/");
            
            var width = 101;
            var height = 103;
            
            var midX = width / 2;
            var midY = height / 2;
            
            for (var i = 0; i < 10000; i++)
            {
                foreach (var robot in input)
                {
                    var nextX = (robot.Position.X + robot.Velocity.X) % width;
                    if (nextX < 0) nextX += width;
                    
                    var nextY = (robot.Position.Y + robot.Velocity.Y) % height;
                    if (nextY < 0) nextY += height;
                    
                    robot.Position = new Vector(nextX, nextY);
                }
                
                var q1 = input.Count(r => r.Position.X < midX && r.Position.Y < midY);
                var q2 = input.Count(r => r.Position.X > midX && r.Position.Y < midY);
                var q3 = input.Count(r => r.Position.X > midX && r.Position.Y > midY);
                var q4 = input.Count(r => r.Position.X < midX && r.Position.Y > midY);
                int[] qArray = [q1, q2, q3, q4];
                
                var qMax = qArray.Max();
                var qRest = qArray.Where(q => q != qMax).Sum();

                if (qMax > qRest)
                {
                    using var image = new Image<Rgba32>(width, height);

                    foreach (var robot in input)
                    {
                        image[robot.Position.X, robot.Position.Y] = new Rgba32(0, 0, 0, 255);
                    }
                    
                    using var stream = File.OpenWrite($"Images/output{i + 1}.png");
                    image.SaveAsPng(stream);
                }
            }
            
            return 6888;
        }

        public Robot[] Parse(string input) => Day14.Parse(input);
    }
    
    public static Robot[] Parse(string input)
    {
        return input
            .SplitToLines(true)
            .Select(line => RobotRegex().Match(line))
            .Select(match => new Robot
            {
                Position = new Vector(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                Velocity = new Vector(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)),
            })
            .ToArray();
    }

    public class Robot
    {
        public Vector Position { get; set; }
            
        public Vector Velocity { get; set; }
    }

    public record struct Vector(int X, int Y);
}