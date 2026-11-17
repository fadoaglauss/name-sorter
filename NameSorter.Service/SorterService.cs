using NameSorter.Models;

namespace NameSorter.Services
{
    public class SorterService : ISorterService
    {
        /// <inheritdoc/>
        public IEnumerable<Person> Run(IEnumerable<Person> names)
        {
            ArgumentNullException.ThrowIfNull(names);

            return names
                 .OrderBy(name => name.LastName, StringComparer.Ordinal)
                 .ThenBy(name => name.GivenName, StringComparer.Ordinal);
        }
    }
}