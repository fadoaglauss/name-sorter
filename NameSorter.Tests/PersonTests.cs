using NameSorter.Models;

namespace NameSorter.Tests
{
    public class PersonTests
    {
        [Theory]
        [InlineData("Janet Parsons", "Janet", "Parsons")]
        [InlineData("Adonis Julius Archer", "Adonis Julius", "Archer")]
        [InlineData("Hunter Uriah Mathew Clarke", "Hunter Uriah Mathew", "Clarke")]
        public void Constructor_ValidName_ShouldParseCorrectly(
            string fullName,
            string expectedGivenNames,
            string expectedLastName)
        {
            // Act
            var name = new Person(fullName);

            // Assert
            Assert.Equal(expectedGivenNames, name.GivenName);
            Assert.Equal(expectedLastName, name.LastName);
        }

        [Theory]
        [InlineData("Janet")]
        [InlineData("One Two Three Four Five")]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ShouldThrowsArgumentException(
            string fullName)
        {
            Assert.Throws<ArgumentException>(() => new Person(fullName));
        }

        [Fact]
        public void ToString_ValidName_ShouldReturnFullName()
        {
            // Arrange
            var name = new Person("Adonis Julius Archer");

            // Act
            string result = name.ToString();

            // Assert
            Assert.Equal("Adonis Julius Archer", result);
        }
    }
}