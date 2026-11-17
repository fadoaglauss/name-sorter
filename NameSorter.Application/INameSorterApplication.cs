namespace NameSorter.Application
{
    /// <summary>
    /// Provides the entry point for running the name-sorting application.
    /// </summary>
    public interface INameSorterApplication
    {
        /// <summary>
        /// Runs the name-sorting application with the specified command-line arguments.
        /// </summary>
        /// <param name="args">The command-line arguments, including the path of the file to process.</param>
        /// <returns>Zero when the application completes successfully; otherwise, a non-zero exit code.</returns>
        int Run(string[] args);
    }
}