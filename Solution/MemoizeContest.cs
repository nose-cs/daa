namespace Solution;

public class MemoizeContest : IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems)
    {
        var dictionary = new Dictionary<int, int[,]>();
        dictionary[0] = new int[3, 3];
        dictionary[1] = new int[3, 3];
        
        var solvedProblems = new List<SolvedProblem>();
        int[] participantTimes = [0, 0, 0];
        int[] problemsLeft = [easyProblems, mediumProblems, hardProblems];
        var best = Array.Empty<SolvedProblem>();
        GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref best, dictionary);
        return best.ToList();
    }
    
    private void GetBestProblemDistribution(int time, int[] problemsLeft, IList<int> participantTimes,
        List<SolvedProblem> solvedProblems, ref SolvedProblem[] maxSolvedProblems, Dictionary<int, int[,]> dictionary)
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

                if (!dictionary.ContainsKey(startTime))
                {
                    dictionary[startTime] = CopyMatrix(dictionary[firstToFinishTime]);
                }
                
                var m = CopyMatrix(dictionary[startTime]);
                m[firstToFinishParticipant, difficulty - 2] += 1;
                
                if (dictionary.TryGetValue(endTime, out var value) && SumMatrix(value) <= SumMatrix(m))
                {
                    continue;
                }

                dictionary[endTime] = m;
                participantTimes[firstToFinishParticipant] = endTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, -1);
                var item = new SolvedProblem(difficulty, firstToFinishParticipant, endTime);
                solvedProblems.Add(item);
                
                GetBestProblemDistribution(time, problemsLeft, participantTimes, solvedProblems, ref maxSolvedProblems, dictionary);
                
                participantTimes[firstToFinishParticipant] = startTime;
                Utils.UpdateProblemsLeft(difficulty, problemsLeft, +1);
                solvedProblems.Remove(item);
            }
        }
    }

    private int SumMatrix(int[,] matrix)
    {
        return matrix.Cast<int>().Sum();
    }
    
    private int[,] CopyMatrix(int[,] matrix)
    {
        var m = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                m[i, j] = matrix[i, j];
            }
        }
        return m;
    }
}