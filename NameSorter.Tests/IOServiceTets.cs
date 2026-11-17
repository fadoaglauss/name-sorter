using NameSorter.Models;
using NameSorter.Services;

namespace NameSorter.Tests
{
    public class IOServiceTests
    {
        private readonly IOService _IOService = new();

        [Fact]
        public void Read_ExistingFile_ShouldReturnNames()
        {
            // Arrange
            string path = Path.GetTempFileName();

            try
            {
                File.WriteAllLines(path,
                [
                    "Janet Parsons",
                    "Marin Alvarez"
                ]);

                // Act
                var result = _IOService.Read(path).ToList();

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Equal("Janet Parsons", result[0].ToString());
                Assert.Equal("Marin Alvarez", result[1].ToString());
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Read_NonExistingFile_ShouldThrowsFileNotFoundException()
        {
            // Arrange
            string path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt");

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _IOService.Read(path));
        }

        [Fact]
        public void Read_EmptyFile_ShouldReturnEmptyCollection()
        {
            // Arrange
            string path = Path.GetTempFileName();

            try
            {
                // Act
                var result = _IOService.Read(path);

                // Assert
                Assert.Empty(result);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Write_ValidNames_ShouldCreateFile()
        {
            // Arrange
            string path = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid()}.txt");

            var names = new[]
            {
                new Person("Marin Alvarez"),
                new Person("Janet Parsons")
            };

            try
            {
                // Act
                _IOService.Write(path, names);

                // Assert
                Assert.True(File.Exists(path));

                Assert.Equal(
                    [ "Marin Alvarez", "Janet Parsons" ],
                    File.ReadAllLines(path));
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Write_ExistingFile_OverwritesContent()
        {
            // Arrange
            string path = Path.GetTempFileName();

            try
            {
                File.WriteAllText(path, "Old Content");

                var names = new[]
                {
                    new Person("Alice Smith")
                };

                // Act
                _IOService.Write(path, names);

                // Assert
                Assert.Equal(
                    [ "Alice Smith" ],
                    File.ReadAllLines(path));
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
