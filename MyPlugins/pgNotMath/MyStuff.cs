using MainApplication.PluginBase.Interfaces;
using MainApplication.PluginBase.Models;

namespace pgNotMath
{
    public class MyStuff : IPlugin
    {
        public string PluginName => "NotAMath";
        public string PluginDescription => "I cannot do Math";

        public bool CanDoMath => false; // I can't and do not lie

        public bool Initialize(InitParams initParams)
        {
            // do my stuff
            return true; // I initialized correctly and can continue
        }
    }
}
