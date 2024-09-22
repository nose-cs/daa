namespace Solution;

public static class Utils
{
    private static readonly Dictionary<int, int> DifficultyMap = Problem.Difficulties
        .Select((difficulty, index) => new { difficulty, index })
        .ToDictionary(x => x.difficulty, x => x.index);

    /// <summary>
    /// Updates the number of problems left for a specific difficulty level.
    /// If the count cannot be subtracted from the available problems, an exception is thrown.
    /// </summary>
    public static void UpdateProblemsLeft(int difficulty, IList<int> problemsLeft, int count)
    {
        if (!DifficultyMap.TryGetValue(difficulty, out var index))
        {
            throw new ArgumentException("Invalid difficulty level.");
        }

        if (!LeftDifficulty(difficulty, problemsLeft, -count))
        {
            throw new Exception($"Unable to update problems left for difficulty {difficulty}.");
        }

        problemsLeft[index] += count;
    }

    /// <summary>
    /// Checks if the count of problems can be subtracted from the remaining problems at a specific difficulty.
    /// </summary>
    /// <returns>True if subtraction is possible, otherwise false.</returns>
    public static bool LeftDifficulty(int difficulty, IList<int> problemsLeft, int count = 1)
    {
        if (!DifficultyMap.TryGetValue(difficulty, out var index))
        {
            throw new ArgumentException("Invalid difficulty level.");
        }

        return problemsLeft[index] >= count;
    }
    
    public static (int Min, int Index) MinWithIndex(this IReadOnlyList<int> array)
    {
        var min = int.MaxValue;
        var index = -1;
        for (var i = 0; i < array.Count; i++)
        {
            if (array[i] >= min) continue;
            min = array[i];
            index = i;
        }

        return (min, index);
    }
}