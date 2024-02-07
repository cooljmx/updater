namespace Installer.Downloading;

public interface ICheckSumCalculator
{
    Task<string> CalculateAsync(string filePath);
}