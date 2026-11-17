using NameSorter.Models;

namespace NameSorter.Services
{
    /// <summary>
    /// Provides functionality for reading and writing name files.
    /// </summary>
    public interface IIOService
    {
        /// <summary>
        /// Reads non-empty names from a file.
        /// </summary>
        /// <param name="path">The path of the file to read.</param>
        /// <returns>The non-empty names read from the file.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
        IEnumerable<Person> Read(string path);

        /// <summary>
        /// Writes names to a file.
        /// </summary>
        /// <param name="path">The path of the file to write.</param>
        /// <param name="names">The names to write.</param>
        void Write(string path, IEnumerable<Person> names);
    }
}