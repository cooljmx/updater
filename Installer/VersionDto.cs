namespace Installer;

public record VersionDto(Version Version, FileInfoDto[] InstallerFiles);

public record FileInfoDto(string Url, string CheckSum);