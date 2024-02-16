namespace Launcher;

internal class BaseAddressProvider : IBaseAddressProvider
{
    public Uri Get()
    {
#if DEBUG
        return new Uri("http://localhost:50001/");
#else
        #error
#endif
    }
}