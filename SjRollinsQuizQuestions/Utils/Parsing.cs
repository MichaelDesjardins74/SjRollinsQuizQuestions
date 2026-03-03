namespace SjRollinsQuizQuestions.Utils;

/*
 * Parsing utility class
 *
 * Purpose:
 * Centralizes all text-to-data conversion logic used by multiple
 * Project Euler problems.
 *
 * Keeping parsing logic separate:
 * - Improves readability of problem classes
 * - Avoids duplication
 * - Makes the codebase easier to maintain or extend
 */
internal static class Parsing
{
    /*
     * Reads a text file from the Resources folder.
     *
     * We use AppContext.BaseDirectory because when the project runs,
     * the executable is launched from the bin/Debug (or bin/Release) folder.
     * The Resources folder is copied there at build time.
     *
     * This method:
     *  1. Builds the full path to the resource file
     *  2. Validates that the file exists
     *  3. Returns the entire file contents as a string
     */
    public static string ReadResourceText(string fileName)
    {
        // Base directory where the compiled executable is running from.
        string baseDir = AppContext.BaseDirectory;

        // Construct full path: <baseDir>/Resources/<fileName>
        string path = Path.Combine(baseDir, "Resources", fileName);

        // Defensive programming: fail fast if file is missing.
        if (!File.Exists(path))
            throw new FileNotFoundException($"Missing resource file: {path}");

        // Read the entire file into memory and return it.
        return File.ReadAllText(path);
    }

    /*
     * Parses a triangle-formatted text file into a jagged 2D array.
     *
     * Expected format:
     * Each row contains space-separated integers.
     *
     * Example:
     *  75
     *  95 64
     *  17 47 82
     *
     * Output:
     *  int[][] where triangle[row][column] accesses each value.
     */
    public static int[][] ParseTriangle(string text)
    {
        // Split the input text into lines.
        // Remove empty lines in case of trailing newline characters.
        var lines = text.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        );

        // Create a jagged array where each row can have a different length.
        int[][] triangle = new int[lines.Length][];

        for (int r = 0; r < lines.Length; r++)
        {
            // Split each line by whitespace (spaces or tabs),
            // parse each token into an integer,
            // and convert the sequence to an array.
            triangle[r] = lines[r]
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        return triangle;
    }

    /*
     * Parses a CSV-style matrix into a 2D rectangular array.
     *
     * Expected format:
     * Each row contains comma-separated integers.
     *
     * Example:
     *  131,673,234
     *  201,96,342
     *
     * Output:
     *  int[,] where matrix[row, column] accesses each value.
     */
    public static int[,] ParseCsvMatrix(string text)
    {
        // Split input text into lines (each represents one matrix row).
        var lines = text.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        );

        int rows = lines.Length;

        // Determine the number of columns by inspecting the first row.
        int cols = lines[0]
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Length;

        // Create a rectangular 2D array with fixed row/column size.
        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            var parts = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries);

            // Ensure all rows have the same number of columns.
            // This prevents malformed input from silently corrupting data.
            if (parts.Length != cols)
                throw new FormatException("Matrix rows are not all the same length.");

            for (int j = 0; j < cols; j++)
            {
                // Convert each CSV value to an integer and store it.
                matrix[i, j] = int.Parse(parts[j]);
            }
        }

        return matrix;
    }
}