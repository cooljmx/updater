namespace Launcher.Common.Cryptography;

public interface ICheckSumCalculator
{
    Task<string> CalculateAsync(string filePath);
}