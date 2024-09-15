namespace Solution;

public class GreedyContest2 : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();
        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];
        int[] participantTimes = [0, 0, 0];
        var endTimes = new bool[time + 1];
        endTimes[0] = true;
        endTimes[1] = true;
        
        while (participantTimes.Any(x => x != -1))
        {
            for (var i = 0; i < participantTimes.Length; i++)
            {
                if (participantTimes[i] == -1) continue;
                
                var (difficulty, endTime) = FindBetterDifficulty(i, problemsLeft, endTimes, participantTimes);

                if (endTime == -1)
                {
                    participantTimes[i] = -1;
                }
                else
                {
                    solvedProblems.Add(new SolvedProblem(difficulty, i, endTime));
                    endTimes[endTime] = true;
                    participantTimes[i] = endTime;
                    Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
                }
            }
        }

        return solvedProblems;
    }

    private (int Difficulty, int EndTime) FindBetterDifficulty(int participant, int[] problemsLeft, bool[] endTimes,
        int[] participantTimes)
    {
        for (var i = participantTimes[participant] + 1; i < endTimes.Length; i++)
        {
            if (endTimes[i]) continue;

            var difficulty = i - participantTimes[participant];

            if (Problem.Difficulties.Contains(difficulty) && Utils.LeftDifficulty(difficulty, problemsLeft))
                return (difficulty, i);

            difficulty = FindClosestDifficulty(difficulty, problemsLeft);
            if (difficulty == -1) continue;

            var endTime = int.Max(i, participantTimes[participant] + difficulty);
            endTime = int.Min(endTime, endTimes.Length - 1);

            if (endTimes[endTime]) continue;

            return (difficulty, endTime);
        }

        return (-1, -1);
    }

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

        _ => -1
    };
}