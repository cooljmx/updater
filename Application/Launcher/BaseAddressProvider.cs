namespace Launcher;

internal class BaseAddressProvider : IBaseAddressProvider
{
    public Uri Get()
    {
        return new Uri("http://localhost:50001/");
    }
}