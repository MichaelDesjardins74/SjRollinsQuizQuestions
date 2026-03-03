namespace SjRollinsQuizQuestions.Problems;

internal static class Euler018
{
    /*
     * Euler #18: Maximum path sum I
     *
     * Goal:
     * Find the maximum total from top to bottom in a triangle,
     * moving only to adjacent numbers on the row below.
     *
     * Why not brute force?
     * - Each row creates two choices.
     * - Total paths = 2^(rows - 1), which grows exponentially.
     *
     * Instead, use dynamic programming (bottom-up):
     * - Start from the last row.
     * - For each cell above, choose the larger of its two children.
     * - Store the best possible sum at each position.
     *
     * Time Complexity: O(n^2)
     * Space Complexity: O(n)
     */
    public static long Solve()
    {
        // Hard-coded triangle from the problem statement.
        string triangleText =
@"75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

        // Convert the multi-line string into a 2D jagged array of integers.
        var rows = triangleText
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray())
            .ToArray();

        // dp represents the best possible sum from a given position
        // down to the bottom of the triangle.
        // Start by copying the last row, since the best sum from the bottom
        // is simply the value itself.
        int[] dp = rows[^1].ToArray();

        // Move upward through the triangle (second-to-last row up to top).
        for (int r = rows.Length - 2; r >= 0; r--)
        {
            for (int c = 0; c < rows[r].Length; c++)
            {
                /*
                 * For each cell:
                 * - It has two children in the row below: dp[c] and dp[c + 1]
                 * - Choose the larger of those two paths.
                 * - Add the current cell's value.
                 *
                 * This effectively collapses the triangle upward,
                 * replacing child rows with optimal partial sums.
                 */
                dp[c] = rows[r][c] + Math.Max(dp[c], dp[c + 1]);
            }
        }

        // After processing all rows, dp[0] contains the maximum total.
        return dp[0];
    }
}