using SjRollinsQuizQuestions.Utils;

namespace SjRollinsQuizQuestions.Problems;

internal static class Euler081
{
    /*
     * Problem #81 (Path sum: two ways)
     *
     * Goal:
     * Find the minimal path sum from the top-left corner to the bottom-right
     * corner of an 80x80 matrix, moving only RIGHT and DOWN.
     *
     * Approach:
     * Use dynamic programming:
     *
     * Let dp[i,j] represent the minimum cost to reach cell (i,j).
     *
     * Recurrence:
     * dp[i,j] = matrix[i,j] + min(dp[i-1,j], dp[i,j-1])
     *
     * Base cases:
     * - First row: can only come from the left
     * - First column: can only come from above
     *
     * Time Complexity: O(n*m)
     * Space Complexity: O(n*m)
     */
    public static long Solve()
    {
        string text = Parsing.ReadResourceText("0081_matrix.txt");
        int[,] a = Parsing.ParseCsvMatrix(text);

        int n = a.GetLength(0);
        int m = a.GetLength(1);

        // dp[i,j] stores the minimum path sum to reach position (i,j).
        long[,] dp = new long[n, m];

        // Iterate row by row, left to right.
        // By the time we compute dp[i,j], the needed subproblems
        // (dp[i-1,j] and dp[i,j-1]) have already been computed.
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (i == 0 && j == 0)
                {
                    // Starting position.
                    dp[i, j] = a[i, j];
                }
                else if (i == 0)
                {
                    // First row: can only move right,
                    // so we must come from the left.
                    dp[i, j] = dp[i, j - 1] + a[i, j];
                }
                else if (j == 0)
                {
                    // First column: can only move down,
                    // so we must come from above.
                    dp[i, j] = dp[i - 1, j] + a[i, j];
                }
                else
                {
                    // General case:
                    // Choose the cheaper of coming from above or from the left.
                    dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + a[i, j];
                }
            }
        }

        // The bottom-right cell contains the minimal path sum.
        return dp[n - 1, m - 1];
    }
}