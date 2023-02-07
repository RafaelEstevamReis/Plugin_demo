using MainApplication.PluginBase.Models;

namespace pgNotAPlugin
{
    public class MyNotPlugin
    {
        // a class that does Initialize, but does not implement IPlugin, so it will not be called
        public bool Initialize(InitParams initParams)
        {
            // do my stuff
            return true; // I initialized correctly and can continue
        }
    }
}
