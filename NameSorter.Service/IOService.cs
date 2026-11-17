using NameSorter.Models;

namespace NameSorter.Services
{    
    public class IOService : IIOService
    {
        /// <inheritdoc/>
        public IEnumerable<Person> Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Input file not found: {path}", path);
            }

            return File.ReadAllLines(path)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => new Person(line));
        }

        /// <inheritdoc/>
        public void Write(string path, IEnumerable<Person> names)
        {
            File.WriteAllLines(path, names.Select(name => name.ToString()));
        }
    }
}