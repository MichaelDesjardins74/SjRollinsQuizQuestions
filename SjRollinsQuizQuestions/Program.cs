// Import the namespace that contains all of our Euler problem classes
// (Euler002, Euler018, etc.)
using SjRollinsQuizQuestions.Problems;

namespace SjRollinsQuizQuestions;

internal static class Program
{
    /*
     * Main() is the entry point of the application.
     * When the program starts, execution begins here.
     * 
     * This method acts as a menu-driven runner that allows the user
     * to choose which Project Euler solution to execute.
     */
    private static void Main()
    {
        // Display program title and available options to the user.
        Console.WriteLine("SJ Rollins - Quiz Questions (Project Euler)");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("2  = Euler #2  (Even Fibonacci numbers)");
        Console.WriteLine("18 = Euler #18 (Maximum path sum I)");
        Console.WriteLine("67 = Euler #67 (Maximum path sum II) [needs triangle file]");
        Console.WriteLine("81 = Euler #81 (Path sum: two ways)   [needs matrix file]");
        Console.WriteLine("82 = Euler #82 (Path sum: three ways) [needs matrix file]");
        Console.WriteLine("A  = Run all");
        Console.WriteLine("Q  = Quit");
        Console.WriteLine();

        // Infinite loop keeps the program running until the user chooses to quit.
        while (true)
        {
            // Prompt the user for input.
            Console.Write("Selection: ");

            // Read user input.
            // - If Console.ReadLine() returns null, replace with empty string.
            // - Trim removes accidental whitespace.
            // - ToUpperInvariant allows case-insensitive input (e.g., "q" or "Q").
            var input = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            // If the user enters "Q", exit the program immediately.
            if (input == "Q") return;

            try
            {
                // Use a switch statement to determine which problem to run
                // based on the user's selection.
                switch (input)
                {
                    case "2":
                        // Call the helper Run() method to execute Euler #2.
                        // We pass:
                        //   - A label for display
                        //   - A reference to the Solve() method
                        Run("Euler #2", Euler002.Solve);
                        break;

                    case "18":
                        Run("Euler #18", Euler018.Solve);
                        break;

                    case "67":
                        Run("Euler #67", Euler067.Solve);
                        break;

                    case "81":
                        Run("Euler #81", Euler081.Solve);
                        break;

                    case "82":
                        Run("Euler #82", Euler082.Solve);
                        break;

                    case "A":
                        // Run all problems sequentially.
                        Run("Euler #2", Euler002.Solve);
                        Run("Euler #18", Euler018.Solve);
                        Run("Euler #67", Euler067.Solve);
                        Run("Euler #81", Euler081.Solve);
                        Run("Euler #82", Euler082.Solve);
                        break;

                    default:
                        // If the input does not match any known option,
                        // notify the user and continue the loop.
                        Console.WriteLine("Unknown option. Choose 2, 18, 67, 81, 82, A, or Q.");
                        break;
                }
            }
            catch (Exception ex)
            {
                /*
                 * Catch any runtime exceptions that occur while executing a problem.
                 * This prevents the entire application from crashing.
                 *
                 * Example:
                 * - Missing resource file
                 * - Parsing error
                 */
                Console.WriteLine("ERROR: " + ex.Message);
                Console.WriteLine("Tip: For 67/81/82, ensure the resource text files exist in Resources/.");
            }

            // Print a blank line before returning to the menu for readability.
            Console.WriteLine();
        }
    }

    /*
     * Helper method that runs a selected Euler problem.
     *
     * Parameters:
     *   label - A descriptive name of the problem (for display purposes)
     *   solve - A delegate pointing to a Solve() method that returns a long
     *
     * Using Func<long> allows us to pass a method reference
     * instead of calling the method immediately.
     */
    private static void Run(string label, Func<long> solve)
    {
        // Print problem label before running it.
        Console.WriteLine($"--- {label} ---");

        // Execute the Solve() method and capture its return value.
        long answer = solve();

        // Display the computed answer.
        Console.WriteLine("Answer: " + answer);
    }
}
