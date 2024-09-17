namespace Solution;

public interface IContest
{
    public List<SolvedProblem> GetBestProblemDistribution(int time, int easyProblems, int mediumProblems, int hardProblems);
}