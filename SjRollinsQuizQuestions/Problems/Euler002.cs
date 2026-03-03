namespace SjRollinsQuizQuestions.Problems;

internal static class Euler002
{
    /*
     * Euler #2: Even Fibonacci numbers
     *
     * Goal:
     * Compute the sum of all even Fibonacci numbers not exceeding 4,000,000.
     *
     * Approach:
     * - Generate Fibonacci numbers iteratively (avoids recursion overhead).
     * - Add each even-valued term to a running total.
     * - Stop once the sequence exceeds the limit.
     *
     * Time Complexity: O(k), where k is the number of Fibonacci terms ≤ 4,000,000.
     */
    public static long Solve()
    {
        const int limit = 4_000_000;

        long sum = 0;

        // Start with the first two Fibonacci numbers.
        int a = 1;
        int b = 2;

        // Generate Fibonacci numbers until exceeding the limit.
        while (b <= limit)
        {
            if (b % 2 == 0)
                sum += b;

            // Advance the sequence: (a, b) → (b, a + b)
            int next = a + b;
            a = b;
            b = next;
        }

        return sum;
    }
}