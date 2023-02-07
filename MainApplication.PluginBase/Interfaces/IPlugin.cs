using MainApplication.PluginBase.Models;

namespace MainApplication.PluginBase.Interfaces
{
    public interface IPlugin
    {
        string PluginName { get; }
        string PluginDescription { get; }


        bool CanDoMath { get; }

        bool Initialize(InitParams initParams);

    }
}
