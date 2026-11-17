using NameSorter.Models;

namespace NameSorter.Services
{
    /// <summary>
    /// Provides functionality for sorting names.
    /// </summary>
    public interface ISorterService
    {
        /// <summary>
        /// Sorts names by last name and then by first name using ordinal comparison.
        /// </summary>
        /// <param name="names">The names to sort.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="names"/> is null.</exception>"
        /// <returns>The names sorted by last name and then by first name.</returns>
        IEnumerable<Person> Run(IEnumerable<Person> names);
    }
}