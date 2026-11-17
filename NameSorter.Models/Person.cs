
namespace NameSorter.Models
{
    /// <summary>
    /// Represents a person identified by given name(s) and a last name.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets the person's last name.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Gets the person's given name(s).
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="fullName">The person's full name, containing one to three given names followed by a last name.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="fullName"/> does not contain two to four name parts.</exception>
        public Person(string fullName)
        {
            string[] parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts == null || parts.Length < 2 || parts.Length > 4)
            {
                throw new ArgumentException("A name must contain 1 to 3 given names and a last name.");
            }

            LastName = parts.Last();
            GivenName = string.Join(" ", parts[..^1]);
        }

        /// <summary>
        /// Returns the person's given name(s) followed by their last name.
        /// </summary>
        /// <returns>The person's full name.</returns>
        public override string ToString()
        {
            return $"{GivenName} {LastName}";
        }
    }
}
