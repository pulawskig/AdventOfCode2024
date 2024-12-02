namespace AdventOfCode2024.Tools;

public static class StringExtensions
{
    public static IEnumerable<string> SplitToLines(this string input)
    {
        using var reader = new StringReader(input);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }
}