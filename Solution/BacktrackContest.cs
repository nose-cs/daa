namespace Solution;

public class BacktrackContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems,
        int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();
        int[] participantTimes = [0, 0, 0];
        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];
        var best = Array.Empty<SolvedProblem>();
        GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref best);
        return best.ToList();
    }

    private void GetBestProblemDistribution(int time, int[] problemsLeft, IList<int> participantTimes,
        List<SolvedProblem> solvedProblems, ref SolvedProblem[] maxSolvedProblems)
    {
        if (solvedProblems.Count > maxSolvedProblems.Length)
        {
            var x = new SolvedProblem[solvedProblems.Count];
            solvedProblems.CopyTo(x);
            maxSolvedProblems = x;
        }

        var firstToFinishTime = participantTimes.Min();
        var firstToFinishParticipant = participantTimes.IndexOf(firstToFinishTime);

        for (var startTime = firstToFinishTime; startTime < time; startTime++)
        {
            foreach (var difficulty in Problem.Difficulties)
            {
                if (!Utils.LeftDifficulty(difficulty, problemsLeft)) continue;
                var endTime = difficulty + startTime;
                if (endTime > time) continue;
                if (participantTimes.Any(x => x == endTime)) continue;

                participantTimes[firstToFinishParticipant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
                var item = new SolvedProblem(difficulty, firstToFinishParticipant, endTime);
                solvedProblems.Add(item);
                
                GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref maxSolvedProblems);
                
                participantTimes[firstToFinishParticipant] = startTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, +1);
                solvedProblems.Remove(item);
            }
        }
    }
}