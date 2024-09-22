namespace Solution;

public class GreedyContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();

        if (time < Problem.Easy) return solvedProblems;

        int[] remainingProblems = [easyProblems, mediumProblems, hardProblems];
        int[] participantTimes = [0, 0, 0];

        var firstToFinishParticipant = 0;

        while (AssignProblem(firstToFinishParticipant, time, remainingProblems, participantTimes, solvedProblems))
        {
            firstToFinishParticipant = participantTimes.MinWithIndex().Index;
        }

        return solvedProblems;
    }
    
    /// <summary>
    /// Attempts to assign a problem to the participant and returns a boolean indicating success.
    /// </summary>
    private bool AssignProblem( int participant, int time, int[] remainingProblems, int[] participantTimes, List<SolvedProblem> solvedProblems)
    {
        for (var startTime = participantTimes[participant]; startTime < time; startTime++)
        {
            foreach (var difficulty in Problem.Difficulties)
            {
                if (!Utils.LeftDifficulty(difficulty, remainingProblems)) continue;

                var endTime = difficulty + startTime;

                if (endTime > time || participantTimes.Contains(endTime)) continue;

                solvedProblems.Add(new SolvedProblem(difficulty, participant, endTime));
                participantTimes[participant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, remainingProblems, -1);
                return true;
            }
        }

        return false;
    }
}