using SjRollinsQuizQuestions.Utils;

namespace SjRollinsQuizQuestions.Problems;

internal static class Euler067
{
    /*
     * Euler #67: Maximum path sum II
     * Same idea as #18, but triangle comes from a file (p067_triangle.txt) and has 100 rows.
     *
     * Approach: identical bottom-up DP.
     */
    public static long Solve()
    {
        string text = Parsing.ReadResourceText("0067_triangle.txt");
        int[][] tri = Parsing.ParseTriangle(text);

        int[] dp = tri[^1].ToArray();

        for (int r = tri.Length - 2; r >= 0; r--)
        {
            for (int c = 0; c < tri[r].Length; c++)
                dp[c] = tri[r][c] + Math.Max(dp[c], dp[c + 1]);
        }

        return dp[0];
    }
}
