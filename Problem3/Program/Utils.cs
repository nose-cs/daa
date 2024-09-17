using Solution;

namespace Daa;

public static class Utils
{
    private const string WhiteSpaces = "   ";

    public static string FormatSolvedProblemsSortedByStartTime(IEnumerable<SolvedProblem> solvedProblems, int time,
        int easyProblems, int mediumProblems, int hardProblems)
    {
        return FormatSolvedProblems(solvedProblems.OrderBy(x => x.StartTime).ToList(), time, easyProblems,
            mediumProblems, hardProblems);
    }

    public static string FormatSolvedProblemsSortedByEndTime(IEnumerable<SolvedProblem> solvedProblems, int time,
        int easyProblems, int mediumProblems, int hardProblems)
    {
        return FormatSolvedProblems(solvedProblems.OrderBy(x => x.EndTime).ToList(), time, easyProblems,
            mediumProblems, hardProblems);
    }

    public static string FormatSolvedProblems(List<SolvedProblem> solvedProblems, int time, int easyProblems,
        int mediumProblems, int hardProblems)
    {
        return solvedProblems.Aggregate(
            $"Parameters: a = {easyProblems}, b = {mediumProblems}, c = {hardProblems}, l = {time}\nSolved problems: {solvedProblems.Count}",
            (s, problem) => $"{s}\n{WhiteSpaces}{problem}");
    }
}