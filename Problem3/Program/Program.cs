using Solution;
using Utils = Daa.Utils;

const int time = 13;
const int easyProblems = 4;
const int mediumProblems = 2;
const int hardProblems = 17;

var contest = new GreedyContest();
var solvedProblems = contest.GetBestProblemDistribution(time, easyProblems, mediumProblems, hardProblems);

var formattedResponse = Utils.FormatSolvedProblems(solvedProblems, time, easyProblems, mediumProblems, hardProblems);
Console.WriteLine(formattedResponse);

var validationResult = Tester.Tester.ValidateSolution(time, easyProblems, mediumProblems, hardProblems, solvedProblems);

if (!validationResult.IsValid)
{
    Console.WriteLine($"Invalid solution: {validationResult.ErrorMessage}\n");
}

// // Uncomment this to run n test cases
// const int n = 500;
// Tester.Tester.Test(contest, n);