using mazharenko.AoCAgent.Generator;
using NUnit.Framework;

namespace AdventOfCode2024.Tests;

[TestFixture]
[GenerateInputTests(nameof(GetCases))]
public partial class InputTests
{
    private static IEnumerable<PartInputCaseData> GetCases()
    {
        yield return new(1, 1, "2285373");
        yield return new(1, 2, "21142653");
        yield return new(2, 1, "549");
        yield return new(2, 2, "589");
        yield return new(3, 1, "178794710");
        yield return new(3, 2, "76729637");
        yield return new(4, 1, "2639");
        yield return new(4, 2, "2005");
        yield return new(5, 1, "4281");
        yield return new(5, 2, "5466");
        yield return new(6, 1, "4580");
        yield return new(6, 2, "1480");
        yield return new(7, 1, "2941973819040");
        yield return new(7, 2, "249943041417600");
        yield return new(8, 1, "313");
        yield return new(8, 2, "1064");
        yield return new(9, 1, "6307275788409");
        yield return new(9, 2, "6327174563252");
        yield return new(10, 1, "717");
        yield return new(10, 2, "1686");
        yield return new(11, 1, "207683");
        yield return new(11, 2, "244782991106220");
        yield return new(14, 1, "216772608");
    }
}