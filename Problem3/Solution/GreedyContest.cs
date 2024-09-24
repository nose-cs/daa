namespace Solution;

public class GreedyContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems,
        int hardProblems)
    {
        if (time < Problem.Easy) return [];

        int[] remainingProblems = [easyProblems, mediumProblems, hardProblems];
        int[] participantTimes = [0, 0, 0];
        var solvedProblems = new List<SolvedProblem>();

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
    private static bool AssignProblem(int participant, int time, int[] remainingProblems, int[] participantTimes,
        List<SolvedProblem> solvedProblems)
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

    public int GetMaxSolvedProblemsCount(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        return time - GetMinHoles(time, easyProblems, mediumProblems, hardProblems);
    }

    private static int GetMinHoles(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        if (time < 2) return time;

        if (2 * easyProblems + 3 * mediumProblems + 4 * hardProblems <= 3 * (time - 1))
            return Math.Max(1, time - easyProblems - mediumProblems - hardProblems);

        if (easyProblems == 0)
            return mediumProblems > 0
                ? 2 + DivisionCeiling(Math.Max(0, time - 2 - mediumProblems), 3)
                : DivisionCeiling(time - 3, 3);

        if (mediumProblems == 0)
        {
            mediumProblems++;
            easyProblems--;
        }

        return 1 + DivisionCeiling(Math.Max(0,
            time - 1 - mediumProblems - 2 * Math.Min(hardProblems, easyProblems) -
            (easyProblems - Math.Min(hardProblems, easyProblems))), 3);
    }

    private static int DivisionCeiling(int x, int y)
    {
        return (int)Math.Ceiling((double)x / y);
    }
}