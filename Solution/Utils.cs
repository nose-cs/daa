namespace Solution;

public static class Utils
{
    public static void UpdateProblemsLeft(int difficulty, IList<int> problemsLeft, int count)
    {
        switch (difficulty)
        {
            case Problem.Easy:
                if (!LeftDifficulty(Problem.Easy, problemsLeft, -count))
                    throw new Exception();
                problemsLeft[0] += count; 
                break;
            case Problem.Medium:
                if (!LeftDifficulty(Problem.Medium, problemsLeft, -count))
                    throw new Exception();
                problemsLeft[1] += count; 
                break;
            case Problem.Hard:
                if (!LeftDifficulty(Problem.Hard, problemsLeft, -count))
                    throw new Exception();
                problemsLeft[2] += count; 
                break;
            default:
                throw new ArgumentException();
        }
    }

    public static bool LeftDifficulty(int difficulty, IList<int> problemsLeft, int count = 1) => difficulty switch
    {
        Problem.Easy => problemsLeft[0] - count >= 0,
        Problem.Medium => problemsLeft[1] - count >= 0,
        Problem.Hard => problemsLeft[2] - count >= 0,
        _ => throw new ArgumentException()
    };

    public static bool LeftProblems(IList<int> problemsLeft) =>
        Problem.Difficulties.Aggregate(false, (acc, difficulty) => acc || LeftDifficulty(difficulty, problemsLeft));
}