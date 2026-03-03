using SjRollinsQuizQuestions.Utils;

namespace SjRollinsQuizQuestions.Problems;

internal static class Euler082
{
    /*
     * Problem #82 (Path sum: three ways): :contentReference[oaicite:14]{index=14}
     * Find the minimal path sum from ANY cell in the left column to ANY cell in the right column,
     * moving only UP, DOWN, and RIGHT. Matrix in p082_matrix.txt. :contentReference[oaicite:15]{index=15}
     *
     * Key idea (common DP approach):
     * We process column-by-column from left to right.
     * Let dist[i] be the minimal cost to reach row i in the current column.
     *
     * For each new column j:
     *  1) Start with coming straight from the left:
     *     dist[i] = distPrev[i] + a[i,j]
     *  2) Relax downward (allow moving down within the column):
     *     dist[i] = min(dist[i], dist[i-1] + a[i,j]) for i=1..n-1
     *  3) Relax upward (allow moving up within the column):
     *     dist[i] = min(dist[i], dist[i+1] + a[i,j]) for i=n-2..0
     *
     * Because costs are non-negative and vertical moves are within the same column,
     * the two passes (down then up) correctly propagate best vertical paths.
     *
     * Complexity: O(n^2) for an n x n matrix (here 80x80 => trivial).
     */
    public static long Solve()
    {
        string text = Parsing.ReadResourceText("0082_matrix.txt");
        int[,] a = Parsing.ParseCsvMatrix(text);

        int n = a.GetLength(0);
        int m = a.GetLength(1);
        if (m != n) throw new InvalidOperationException("Expected a square matrix for Euler #82.");

        // distPrev[i] = minimal cost to reach row i in the previous column
        long[] distPrev = new long[n];

        // Initialize with the leftmost column: starting point can be ANY row in left column.
        for (int i = 0; i < n; i++)
            distPrev[i] = a[i, 0];

        // Process remaining columns left -> right.
        for (int col = 1; col < n; col++)
        {
            long[] dist = new long[n];

            // Step 1: come from the left (same row).
            for (int row = 0; row < n; row++)
                dist[row] = distPrev[row] + a[row, col];

            // Step 2: allow moving DOWN within the column.
            for (int row = 1; row < n; row++)
                dist[row] = Math.Min(dist[row], dist[row - 1] + a[row, col]);

            // Step 3: allow moving UP within the column.
            for (int row = n - 2; row >= 0; row--)
                dist[row] = Math.Min(dist[row], dist[row + 1] + a[row, col]);

            distPrev = dist;
        }

        // Finish in ANY row of the rightmost column -> answer is minimum dist there.
        return distPrev.Min();
    }
}
