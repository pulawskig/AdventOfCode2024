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
    }
}