using Moq;
using NameSorter.Application;
using NameSorter.Models;
using NameSorter.Services;

namespace NameSorter.Tests
{
    public class NameSorterApplicationTests
    {
        private readonly Mock<IIOService> _ioServiceMock;
        private readonly Mock<ISorterService> _sorterMock;
        private readonly NameSorterApplication _application;

        public NameSorterApplicationTests()
        {
            _ioServiceMock = new Mock<IIOService>();
            _sorterMock = new Mock<ISorterService>();

            _application = new NameSorterApplication(
                _ioServiceMock.Object,
                _sorterMock.Object);
        }

        [Fact]
        public void Run_ValidFile_ShouldSortAndWriteNames()
        {
            // Arrange
            var names = new List<Person>
            {
                new("Janet Parsons"),
                new("Marin Alvarez")
            };

            var sortedNames = new List<Person>
            {
                new("Marin Alvarez"),
                new("Janet Parsons")
            };

            _ioServiceMock
                .Setup(x => x.Read("input.txt"))
                .Returns(names);

            _sorterMock
                .Setup(x => x.Run(names))
                .Returns(sortedNames);

            // Act
            int result = _application.Run(["input.txt"]);

            // Assert
            Assert.Equal(0, result);

            _ioServiceMock.Verify(
                x => x.Read("input.txt"),
                Times.Once);

            _sorterMock.Verify(x => x.Run(names), Times.Once);

            _ioServiceMock.Verify(
                x => x.Write("input.txt", sortedNames),
                Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Run_InvalidArguments_ShouldReturnsFailure(string? filePath)
        {
            // Arrange
            string[] args = filePath is null
                ? []
                : [filePath];

            // Act
            int result = _application.Run(args);

            // Assert
            Assert.Equal(1, result);

            _ioServiceMock.VerifyNoOtherCalls();
            _sorterMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Run_TooManyArguments_ShouldReturnsFailure()
        {
            // Act
            int result = _application.Run(["file1.txt", "file2.txt"]);

            // Assert
            Assert.Equal(1, result);

            _ioServiceMock.VerifyNoOtherCalls();
            _sorterMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Run_FileNotFound_ShouldReturnsFailure()
        {
            // Arrange
            _ioServiceMock
                .Setup(x => x.Read("missing.txt"))
                .Throws(new FileNotFoundException());

            // Act
            int result = _application.Run(["missing.txt"]);

            // Assert
            Assert.Equal(1, result);

            _sorterMock.VerifyNoOtherCalls();

            _ioServiceMock.Verify(
                x => x.Write(
                    It.IsAny<string>(),
                    It.IsAny<IEnumerable<Person>>()),
                Times.Never);
        }

        [Fact]
        public void Run_InvalidName_ShouldReturnsFailure()
        {
            // Arrange
            _ioServiceMock
                .Setup(x => x.Read("input.txt"))
                .Throws(new ArgumentException("Invalid name"));

            // Act
            int result = _application.Run(["input.txt"]);

            // Assert
            Assert.Equal(1, result);

            _sorterMock.VerifyNoOtherCalls();

            _ioServiceMock.Verify(
                x => x.Write(
                    It.IsAny<string>(),
                    It.IsAny<IEnumerable<Person>>()),
                Times.Never);
        }

        [Fact]
        public void Run_WriteFails_ShouldReturnsFailure()
        {
            // Arrange
            var names = new List<Person>
        {
            new("Alice Smith")
        };

            _ioServiceMock
                .Setup(x => x.Read("input.txt"))
                .Returns(names);

            _sorterMock
                .Setup(x => x.Run(names))
                .Returns(names);

            _ioServiceMock
                .Setup(x => x.Write(
                    "input.txt",
                    It.IsAny<IEnumerable<Person>>()))
                .Throws(new IOException("Cannot write file"));

            // Act
            int result = _application.Run(["input.txt"]);

            // Assert
            Assert.Equal(1, result);

            _ioServiceMock.Verify(
                x => x.Write(
                    "input.txt",
                    names),
                Times.Once);
        }

        [Fact]
        public void Run_EmptyFile_ShouldWritesEmptyCollection()
        {
            // Arrange
            var names = new List<Person>();

            _ioServiceMock
                .Setup(x => x.Read("empty.txt"))
                .Returns(names);

            _sorterMock
                .Setup(x => x.Run(names))
                .Returns(names);

            // Act
            int result = _application.Run(["empty.txt"]);

            // Assert
            Assert.Equal(0, result);

            _ioServiceMock.Verify(
                x => x.Write(
                    "empty.txt",
                    It.Is<IEnumerable<Person>>(
                        collection => !collection.Any())),
                Times.Once);
        }
    }
}