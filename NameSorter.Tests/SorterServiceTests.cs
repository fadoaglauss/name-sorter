using NameSorter.Models;
using NameSorter.Services;

namespace NameSorter.Tests
{
    public class SorterServiceTests
    {
        private readonly SorterService _sorter = new();

        [Fact]
        public void Sort_UnsortedNames_ShouldOrderByLast()
        {
            // Arrange
            var names = new[]
            {
                new Person("Janet Parsons"),
                new Person("Marin Alvarez"),
                new Person("Leo Gardner")
            };

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(
                ["Alvarez", "Gardner", "Parsons"],
                result.Select(n => n.LastName));
        }

        [Fact]
        public void Sort_SameLast_ShouldOrdersByGiven()
        {
            // Arrange
            var names = new[]
            {
                new Person("Zoe Smith"),
                new Person("Alice Smith"),
                new Person("Bob Smith")
            };

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(
                ["Alice", "Bob", "Zoe"],
                result.Select(n => n.GivenName));
        }

        [Fact]
        public void Sort_MultipleGiven_ShouldOrdersCorrectly()
        {
            // Arrange
            var names = new[]
            {
                new Person("John Michael Smith"),
                new Person("John Adam Smith"),
                new Person("Alice Smith")
            };

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(
                [
                    "Alice Smith",
                    "John Adam Smith",
                    "John Michael Smith"
                ],
                result.Select(n => n.ToString()));
        }

        [Fact]
        public void Sort_EmptyCollection_ShouldReturnEmptyCollection()
        {
            // Act
            var result = _sorter.Run([]);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Sort_NullCollection_ShouldThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sorter.Run(null!));
        }

        [Fact]
        public void Sort_DuplicateNames_ShouldPreservesDuplicates()
        {
            // Arrange
            var names = new[]
            {
                new Person("John Smith"),
                new Person("Alice Brown"),
                new Person("John Smith")
            };

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(3, result.ToList().Count);
            Assert.Equal(
                2,
                result.Count(n => n.ToString() == "John Smith"));
        }

        [Fact]
        public void Sort_AssessmentExample_ShouldReturnExpectedOrder()
        {
            // Arrange
            var names = new[]
            {
                "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley",
                "Leo Gardner",
                "Hunter Uriah Mathew Clarke",
                "Mikayla Lopez",
                "Frankie Conner Ritter"
            }.Select(n => new Person(n));

            string[] expected =
            [
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Hunter Uriah Mathew Clarke",
                "Leo Gardner",
                "Vaughn Lewis",
                "London Lindsey",
                "Mikayla Lopez",
                "Janet Parsons",
                "Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            ];

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(
                expected,
                result.Select(n => n.ToString()));
        }

        [Fact]
        public void Sort_ThousandNames_ReturnsSortedCollection()
        {
            // Arrange
            var names = Enumerable.Range(0, 1000)
                .Reverse()
                .Select(i => new Person($"John Last{i:D4}"))
                .ToList();

            // Act
            var result = _sorter.Run(names);

            // Assert
            Assert.Equal(1000, result.ToList().Count);

            Assert.Equal(
                Enumerable.Range(0, 1000)
                    .Select(i => $"Last{i:D4}"),
                result.Select(n => n.LastName));
        }
    }
}
