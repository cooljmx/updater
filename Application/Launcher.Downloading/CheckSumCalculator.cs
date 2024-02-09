using System.Security.Cryptography;
using System.Text;

namespace Launcher.Downloading;

internal class CheckSumCalculator : ICheckSumCalculator
{
    public async Task<string> CalculateAsync(string filePath)
    {
        using var sha256 = SHA256.Create();
        await using var fileStream = File.OpenRead(filePath);
        var bytes = await sha256.ComputeHashAsync(fileStream);

        var stringBuilder = new StringBuilder(bytes.Length * 2);
        foreach (var @byte in bytes)
            stringBuilder.Append($"{@byte:x2}");

        return stringBuilder.ToString();
    }
}