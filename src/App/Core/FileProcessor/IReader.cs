namespace CapitalGain.Core.FileProcessor
{
    public interface IReader
    {
        Task<ReaderResult> ReadAsync(string filePath);
    }
}