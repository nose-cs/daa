namespace Solution;

public class GreedyContest : IContest
{
    private readonly int[] _participantTimes = [0, 0, 0];
    
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();
        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];

        while (Utils.LeftProblems(problemsLeft))
        {
            var assignProblem = false;

            for (var i = 0; i < _participantTimes.Length; i++)
            {
                var assignedProblem = AssignProblem(problemsLeft, i, time, solvedProblems);
                assignProblem = assignProblem || assignedProblem;
            }

            if (!assignProblem) break;
        }

        return solvedProblems;
    }

    private bool AssignProblem(int[] problemsLeft, int participant, int time, List<SolvedProblem> solvedProblems)
    {
        if (FindNextPerfectEndTime() > time) 
            return false;
        
        try
        {
           var (difficulty, endTime) = FindBetterDifficulty(participant, problemsLeft);
           if (endTime > time) return false;
           solvedProblems.Add(new SolvedProblem(difficulty, participant, endTime));
           _participantTimes[participant] = endTime;
           Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
           return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
    
    private (int Difficulty, int EndTime) FindBetterDifficulty(int participant, int[] problemsLeft)
    {
        var nextPerfectEndTime = FindNextPerfectEndTime();
        var startTime = _participantTimes[participant];
        var difficulty = nextPerfectEndTime - startTime;

        if (Problem.Difficulties.Contains(difficulty) && Utils.LeftDifficulty(difficulty, problemsLeft))
            return (difficulty, nextPerfectEndTime);

        difficulty = FindClosestDifficulty(difficulty, problemsLeft);
        var endTime = int.Max(nextPerfectEndTime, startTime + difficulty);

        return (difficulty, endTime);
    }
    
    private int FindNextPerfectEndTime() => _participantTimes.Max() + 1;
    
    private static int FindClosestDifficulty(int difficulty, IList<int> problemsLeft) => difficulty switch
    {
        >= Problem.Hard when Utils.LeftDifficulty(Problem.Hard, problemsLeft) => Problem.Hard,
        >= Problem.Hard when Utils.LeftDifficulty(Problem.Medium, problemsLeft) => Problem.Medium,
        >= Problem.Hard when Utils.LeftDifficulty(Problem.Easy, problemsLeft) => Problem.Easy,
        
        <= Problem.Easy when Utils.LeftDifficulty(Problem.Easy, problemsLeft) => Problem.Easy,
        <= Problem.Easy when Utils.LeftDifficulty(Problem.Medium, problemsLeft) => Problem.Medium,
        <= Problem.Easy when Utils.LeftDifficulty(Problem.Hard, problemsLeft) => Problem.Hard,
        
        Problem.Medium when Utils.LeftDifficulty(Problem.Medium, problemsLeft) => Problem.Medium,
        Problem.Medium when Utils.LeftDifficulty(Problem.Easy, problemsLeft) => Problem.Easy,
        Problem.Medium when Utils.LeftDifficulty(Problem.Hard, problemsLeft) => Problem.Hard,
        
        _ => throw new ArgumentException()
    };
}