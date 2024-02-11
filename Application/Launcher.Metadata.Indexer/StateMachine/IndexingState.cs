namespace Launcher.Metadata.Indexer.StateMachine;

internal enum IndexingState
{
    FileCollectionBuilding,
    CheckSumCalculating,
    MetadataSaving,
    Shutdown,
}