using Daa;
using Solution;

const int time = 13;
const int easyProblems = 4;
const int mediumProblems = 2;
const int hardProblems = 17;

var contest = new GreedyRecursiveContest();
var solvedProblems = contest.GetBestProblemDistribution(time, easyProblems, mediumProblems, hardProblems);
var validationResult = Tester.Tester.ValidateSolution(time, easyProblems, mediumProblems, hardProblems, solvedProblems);

Console.WriteLine(contest.GetType());
var formattedResponse = SolvedProblemsFormatter.FormatSolvedProblemsSortedByStartTime(solvedProblems, time, easyProblems, mediumProblems, hardProblems);
Console.WriteLine(formattedResponse);

if (!validationResult.IsValid)
{
    Console.WriteLine($"Invalid solution: {validationResult.ErrorMessage}\n");
}

Tester.Tester.Test(contest, 1000);