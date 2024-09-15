namespace Solution;

public class GreedyRecursiveContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var solvedProblems = new List<SolvedProblem>();

        if (time < Problem.Easy) return solvedProblems;
        
        var endTimes = new bool[time + 1];
        endTimes[0] = true;
        endTimes[1] = true;
        
        int[] participantTimes = [0, 0, 0];
        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];
        var best = Array.Empty<SolvedProblem>();
        GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref best, endTimes);
        return best.ToList();
    }
    
    private void GetBestProblemDistribution(int time, int[] problemsLeft, IList<int> participantTimes,
        List<SolvedProblem> solvedProblems, ref SolvedProblem[] maxSolvedProblems, bool[] dictionary)
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
                if (participantTimes[firstToFinishParticipant] > startTime) continue;
                var endTime = difficulty + startTime;
                if (endTime > time) continue;
                
                if (dictionary[endTime])
                {
                    // Si ya contenía la llave
                    continue;
                }

                dictionary[endTime] = true;
                var item = new SolvedProblem(difficulty, firstToFinishParticipant, endTime);

                participantTimes[firstToFinishParticipant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
                solvedProblems.Add(item);
                
                GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref maxSolvedProblems, dictionary);
            }
        }
    }
}