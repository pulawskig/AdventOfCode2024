using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Tools;

public static class ListExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddFromSpan(this List<int> numbers, ReadOnlySpan<char> numberSpan)
    {
        if (int.TryParse(numberSpan, out var number))
        {
            numbers.Add(number);
        }
    }
}