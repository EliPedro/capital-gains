using CapitalGain.Core.FileProcessor;

namespace CapitalGain.UnitTests.Core.FileProcessor
{
    public class ReaderTests
    {
        private const string testDirectory = "TestFiles";

        public ReaderTests()
        {
            if (!Directory.Exists(testDirectory))
            {
                Directory.CreateDirectory(testDirectory);
            }
        }

        [Fact(DisplayName = "Unsupported File Extension Should Return Error")]
        public async Task UnsupportedFileExtension()
        {
            var reader = new Reader();
            string filePath = Path.Combine(testDirectory, "test.csv");
            File.WriteAllText(filePath, "Test content");

            var result = await reader.ReadAsync(filePath);

            Assert.NotNull(result.Error);
            Assert.Equal("Unsupported file extension", result.Error.Title);
            Assert.Contains("Allowed extensions: .txt, .json", result.Error.Detail);
        }

        [Fact(DisplayName = "File Not Found Should Return Error")]
        public async Task FileNotFound()
        {
            var reader = new Reader();
            string filePath = Path.Combine(testDirectory, "nonexistent.txt");

            var result = await reader.ReadAsync(filePath);

            Assert.NotNull(result.Error);
            Assert.Equal("File not found", result.Error.Title);
            Assert.Contains("File not found: ", result.Error.Detail);
        }

        [Fact(DisplayName = "Empty File Should Return Error")]
        public async Task EmptyFile()
        {
            var reader = new Reader();
            string filePath = Path.Combine(testDirectory, "empty.txt");
            File.WriteAllText(filePath, string.Empty);

            var result = await reader.ReadAsync(filePath);

            Assert.NotNull(result.Error);
            Assert.Equal("Empty file", result.Error.Title);
            Assert.Contains("File is empty: ", result.Error.Detail);
        }

        [Fact(DisplayName = "Valid File Should Return Lines")]
        public async Task ValidFile()
        {
            var reader = new Reader();
            string filePath = Path.Combine(testDirectory, "valid.txt");
            File.WriteAllText(filePath, "Line 1\nLine 2\nLine 3");

            var result = await reader.ReadAsync(filePath);

            Assert.Null(result.Error);
            Assert.NotNull(result.Content);
            Assert.Equal("Line 1\nLine 2\nLine 3", result.Content);
        }

        public void Dispose()
        {
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
        }
    }
}