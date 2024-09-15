using Solution;

namespace Tester;

public static class Tester
{
    private const int MaxTime = 22;
    private static readonly Random Random = new(2002);
    private static readonly BacktrackContest ExpectedContest = new();

    public static void Test(IContest contest, int count = 100)
    {
        Console.WriteLine("Starting tests...");

        var testCases = GenerateTestCases(count);

        foreach (var testCase in testCases)
        {
            var input = testCase.Input;
            var result = contest.GetBestProblemDistribution(input.Time, input.EasyProblems, input.MediumProblems,
                input.HardProblems);
            var validationResult = ValidateSolution(testCase.Input, result);

            if (!validationResult.IsValid)
            {
                throw new Exception($"Invalid result solution: {validationResult.ErrorMessage}. TestCase: {testCase}");
            }

            if (result.Count != testCase.ExpectedSolution.Count)
            {
                throw new Exception(
                    $"{testCase.ExpectedSolution.Count} solved problems were expected, but {result.Count} were gotten.\nTestCase: {testCase}");
            }
        }

        Console.WriteLine("All tests passed!!!");
    }

    private static IEnumerable<TestCase> GenerateTestCases(int count)
    {
        var inputs = GenerateInputs(count);
        var id = 1;
        foreach (var input in inputs)
        {
            var expectedResult = ExpectedContest.GetBestProblemDistribution(input.Time, input.EasyProblems,
                input.MediumProblems, input.HardProblems);

            var validationExpected = ValidateSolution(input, expectedResult);

            if (!validationExpected.IsValid)
            {
                throw new Exception($"Invalid expected solution: {validationExpected.ErrorMessage}. Input: {input}");
            }

            yield return new TestCase(id, input, expectedResult);
            id++;
        }
    }

    private static IEnumerable<Input> GenerateInputs(int count)
    {
        var first = count / 4;
        var second = count / 4;
        var third = count - first - second;

        for (var i = 0; i < first; i++)
        {
            var l = Random.Next(MaxTime);
            var a = Random.Next(20);
            var b = Random.Next(20);
            var c = Random.Next(20);
            yield return new Input(l, a, b, c);
        }

        for (var i = 0; i < second; i++)
        {
            var l = Random.Next(MaxTime);
            var a = Random.Next(20);
            var b = Random.Next(3);
            var c = Random.Next(20);
            yield return new Input(l, a, b, c);
        }

        for (var i = 0; i < third; i++)
        {
            var l = Random.Next(MaxTime);
            var a = Random.Next(10);
            var b = Random.Next(5);
            var c = Random.Next(20);
            yield return new Input(l, a, b, c);
        }
    }

    private static (bool IsValid, string? ErrorMessage) ValidateSolution(Input input,
        List<SolvedProblem> solvedProblems)
    {
        return ValidateSolution(input.Time, input.EasyProblems, input.MediumProblems, input.HardProblems,
            solvedProblems);
    }

    public static (bool IsValid, string? ErrorMessage) ValidateSolution(int time, int easyProblems, int mediumProblems,
        int hardProblems, List<SolvedProblem> solvedProblems)
    {
        if (solvedProblems.Count(x => x.Difficulty == Problem.Easy) > easyProblems)
            return (false, "More easy problems used than available");

        if (solvedProblems.Count(x => x.Difficulty == Problem.Medium) > mediumProblems)
            return (false, "More medium problems used than available");

        if (solvedProblems.Count(x => x.Difficulty == Problem.Hard) > hardProblems)
            return (false, "More hard problems used than available");

        var grouped = solvedProblems.GroupBy(x => x.Participant);


        foreach (var x in grouped)
        {
            var i = 0;
            foreach (var y in x)
            {
                var j = 0;
                foreach (var z in x)
                {
                    if (i == j) continue;
                    if ((y.StartTime <= z.EndTime && y.EndTime >= z.StartTime) ||
                        (z.StartTime <= y.EndTime && z.EndTime >= y.StartTime))
                    {
                        return (false, "One person solved multiple problems at a time");
                    }

                    j++;
                }

                i++;
            }
        }


        if (solvedProblems.Any(x => x.EndTime > time))
            return (false, "Some problems ended after the expected time");

        return solvedProblems.Select(x => x.EndTime).ToHashSet().Count != solvedProblems.Count
            ? (false, "There are two problems that end at the same time")
            : (true, null);
    }
}