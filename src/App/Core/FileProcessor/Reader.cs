namespace CapitalGain.Core.FileProcessor
{
    public class Reader : IReader
    {
        private readonly string[] allowedFileExtensions = { ".txt", ".json" };

        public async Task<ReaderResult> ReadAsync(string filePath)
        {
            var result = new ReaderResult();
            try
            {
                if (!allowedFileExtensions.Contains(Path.GetExtension(filePath)))
                {
                    return new ReaderResult
                    {
                        Error = new ValidantionProblemDetails
                        {
                            Title = "Unsupported file extension",
                            Detail = $"Allowed extensions: {string.Join(", ", allowedFileExtensions)}"
                        }
                    };
                }
                if (!File.Exists(filePath))
                {
                    return new ReaderResult
                    {
                        Error = new ValidantionProblemDetails
                        {
                            Title = "File not found",
                            Detail = $"File not found: {filePath}"
                        }
                    };
                }
                var fileContent = string.Empty;

                using (StreamReader reader = new(filePath))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    return new ReaderResult
                    {
                        Error = new ValidantionProblemDetails
                        {
                            Title = "Empty file",
                            Detail = $"File is empty: {filePath}"
                        }
                    };
                }

                return new ReaderResult
                {
                    Content = fileContent
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult
                {
                    Error = new ValidantionProblemDetails
                    {
                        Title = "Unexpected error",
                        Detail = $"Error reading the file: {ex.InnerException?.Message ?? ex.Message}"
                    }
                };
            }
        }
    }
}