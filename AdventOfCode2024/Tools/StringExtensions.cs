namespace AdventOfCode2024.Tools;

public static class StringExtensions
{
    public static IEnumerable<string> SplitToLines(this string input, bool removeEmpty = false)
    {
        using var reader = new StringReader(input);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (removeEmpty && string.IsNullOrWhiteSpace(line)) continue;
            yield return line;
        }
    }
}