namespace Updater.Downloading;

public interface ICheckSumCalculator
{
    Task<string> CalculateAsync(string filePath);
}