namespace Solution;

public static class MatrixExtensions
{
    public static int Sum(this int[,] matrix)
    {
        return matrix.Cast<int>().Sum();
    }
    
    public static int[,] Copy(this int[,] matrix)
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
    
    public static void Print(this int[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }

            Console.WriteLine();
        }
    }
    
    public static bool Compare(this int[,] m1, int[,] m2)
    {
        if (m1.GetLength(0) != m2.GetLength(0) || m1.GetLength(1) != m2.GetLength(1))
            return false;
            
        for (var i = 0; i < m1.GetLength(0); i++)
        {
            for (var j = 0; j < m1.GetLength(1); j++)
            {
                if (m1[i, j] != m2[i, j])
                    return false;
            }
        }

        return true;
    }
}