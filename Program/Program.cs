using Daa;
using Solution;

const int time = 12;
const int easyProblems = 6;
const int mediumProblems = 1;
const int hardProblems = 12;

var contest = new GreedyContest2();
var solvedProblems = contest.GetBestProblemDistribution(time, easyProblems, mediumProblems, hardProblems);
var validationResult = Tester.Tester.ValidateSolution(time, easyProblems, mediumProblems, hardProblems, solvedProblems);

Console.WriteLine(contest.GetType());
var formattedResponse = SolvedProblemsFormatter.FormatSolvedProblemsSortedByStartTime(solvedProblems, time, easyProblems, mediumProblems, hardProblems);
Console.WriteLine(formattedResponse);

if (!validationResult.IsValid)
{
    Console.WriteLine($"Invalid solution: {validationResult.ErrorMessage}\n");
}

Tester.Tester.Test(contest);