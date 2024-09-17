namespace Solution;

public class GreedyContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();

        if (time < Problem.Easy) return solvedProblems;

        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];
        int[] participantTimes = [0, 0, 0];

        var firstToFinishParticipant = 0;

        while (AssignProblem(firstToFinishParticipant, time, problemsLeft, participantTimes, solvedProblems))
        {
            firstToFinishParticipant = GetMinIndex(participantTimes);
        }

        return solvedProblems;
    }
    
    /// <summary>
    /// Attempts to assign a problem to the participant and returns a boolean indicating success.
    /// </summary>
    private bool AssignProblem( int participant, int time, int[] problemsLeft, int[] participantTimes, List<SolvedProblem> solvedProblems)
    {
        for (var startTime = participantTimes[participant]; startTime < time; startTime++)
        {
            foreach (var difficulty in Problem.Difficulties)
            {
                if (!Utils.LeftDifficulty(difficulty, problemsLeft)) continue;

                var endTime = difficulty + startTime;

                if (endTime > time || participantTimes.Contains(endTime)) continue;

                solvedProblems.Add(new SolvedProblem(difficulty, participant, endTime));
                participantTimes[participant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
                return true;
            }
        }

        return false;
    }
    
    private static int GetMinIndex(IReadOnlyList<int> array)
    {
        var min = int.MaxValue;
        var index = -1;
        for (var i = 0; i < array.Count; i++)
        {
            if (array[i] >= min) continue;
            min = array[i];
            index = i;
        }

        return index;
    }
}