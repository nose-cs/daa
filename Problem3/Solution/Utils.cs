namespace Solution;

public static class Utils
{
    /// <summary>
    /// Updates the number of problems left for a specific difficulty level.
    /// If the count cannot be subtracted from the available problems, an exception is thrown.
    /// </summary>
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

    /// <summary>
    /// Checks if the count of problems can be subtracted from the remaining problems at a specific difficulty.
    /// </summary>
    /// <returns>True if subtraction is possible, otherwise false.</returns>
    public static bool LeftDifficulty(int difficulty, IList<int> problemsLeft, int count = 1) => difficulty switch
    {
        // Check if the count can be subtracted from the remaining easy problems
        Problem.Easy => problemsLeft[0] - count >= 0,
        // Check if the count can be subtracted from the remaining medium problems
        Problem.Medium => problemsLeft[1] - count >= 0,
        // Check if the count can be subtracted from the remaining hard problems
        Problem.Hard => problemsLeft[2] - count >= 0,
        // Throw an exception if an invalid difficulty is provided
        _ => throw new ArgumentException()
    };
}