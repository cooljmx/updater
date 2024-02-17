$fileName = "C:\Users\coolj\AppData\Local\CustomUpdaterCompany\CustomUpdater\Launcher.exe"
$fileVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($fileName).FileVersion
$versionParts = $fileVersion -split '\.'

$majorNumber = [int]$versionParts[0]
$minorNumber = [int]$versionParts[1]
$releaseNumber = [int]$versionParts[2]
$buildNumber = [int]$versionParts[3]
$nextBuildNumber = $buildNumber + 1

if ($majorNumber -eq 0){
	$majorNumber = 1
}

$nextVersion = "$majorNumber.$minorNumber.$releaseNumber.$nextBuildNumber"

$id = [System.Guid]::NewGuid()

dotnet publish ..\Application\Launcher\Launcher.csproj -c Release -o ..\data\$id --self-contained -p:PublishSingleFile=true /p:Version=$nextVersion /p:DebugSymbols=false /p:DebugType=None

dotnet publish ..\Application\Launcher.Metadata.Indexer\Launcher.Metadata.Indexer.csproj -c Release -o ..\Application\Launcher.Metadata.Indexer\bin\publish --self-contained -p:PublishSingleFile=true /p:DebugSymbols=false /p:DebugType=None

..\Application\Launcher.Metadata.Indexer\bin\publish\Launcher.Metadata.Indexer.exe ..\data\$id

dotnet publish ..\Application\Launcher\Launcher.csproj -c Release -o ..\data --self-contained -p:PublishSingleFile=true /p:Version=0.0.0.0 /p:DebugSymbols=false /p:DebugType=None

dotnet publish ..\Application\WebInstaller\WebInstaller.csproj -c Release -o ..\data

$versionJson = @"
{"Version":"$nextVersion","Id":"$id"}
"@

$versionJson | Out-File -FilePath ..\data\version.json -Encoding ascii