namespace Solution;

public abstract class Problem(int difficulty)
{
    public const int Easy = 2;
    public const int Medium = 3;
    public const int Hard = 4;
    public static readonly int[] Difficulties = [Easy, Medium, Hard];

    public int Difficulty { get; } = difficulty;
}

public class SolvedProblem(int difficulty, int participant, int endTime = -1) : Problem(difficulty)
{
    public int Participant { get; } = participant;
    public int EndTime { get; } = endTime;

    public int StartTime => EndTime - Difficulty;

    public override string ToString()
    {
        return $"StartTime: {StartTime}, ParticipantId: {Participant}, Difficulty: {Difficulty}, EndTime: {EndTime}";
    }
}