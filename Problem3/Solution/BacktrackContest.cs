namespace Solution;

public class BacktrackContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems,
        int hardProblems)
    {
        int[] participantTimes = [0, 0, 0];
        int[] remainingProblems = [easyProblems, mediumProblems, hardProblems];
        return GetBestProblemDistribution(time, remainingProblems, participantTimes, [], []);
    }

    public int GetMaxSolvedProblemsCount(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        return GetBestProblemDistribution(time, easyProblems, mediumProblems, hardProblems).Count;
    }

    private List<SolvedProblem> GetBestProblemDistribution(int time, int[] remainingProblems, int[] participantTimes,
        List<SolvedProblem> currentSolvedProblems, List<SolvedProblem> bestSolvedProblems)
    {
        if (currentSolvedProblems.Count > bestSolvedProblems.Count)
        {
            bestSolvedProblems = [..currentSolvedProblems];
        }

        var (firstToFinishTime, firstToFinishParticipant) = participantTimes.MinWithIndex();

        var availableEndTimes = time - firstToFinishTime;
            
        if (currentSolvedProblems.Count + availableEndTimes < bestSolvedProblems.Count)
            return bestSolvedProblems;
        
        for (var startTime = firstToFinishTime; startTime < time; startTime++)
        {
            foreach (var difficulty in Problem.Difficulties)
            {
                if (!Utils.LeftDifficulty(difficulty, remainingProblems)) continue;
                var endTime = difficulty + startTime;
                if (endTime > time || participantTimes.Any(x => x == endTime)) continue;

                participantTimes[firstToFinishParticipant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, remainingProblems, -1);
                var item = new SolvedProblem(difficulty, firstToFinishParticipant, endTime);
                currentSolvedProblems.Add(item);

                bestSolvedProblems = GetBestProblemDistribution(time, remainingProblems, participantTimes,
                    currentSolvedProblems, bestSolvedProblems);

                participantTimes[firstToFinishParticipant] = firstToFinishTime;
                Utils.UpdateProblemsLeft(difficulty, remainingProblems, +1);
                currentSolvedProblems.Remove(item);
            }
        }

        return bestSolvedProblems;
    }
}