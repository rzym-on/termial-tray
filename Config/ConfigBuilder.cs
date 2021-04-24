using System.Collections.Generic;
using WindowsTermialTray.Config.Provider;

namespace WindowsTermialTray.Config
{
    // var config = new ConfigBuilder().AddJsonFile("./config.json").AddJsonFile(appPath).Build()
    public class ConfigBuilder
    {
        private class ProviderList
        {
            public string filePath;
            public IProvider provider;
        }

        private readonly List<ProviderList> _providers;

        private ConfigBuilder(List<ProviderList> providers)
        {
            new ConfigBuilder(_providers = providers);
        }

        public static ConfigBuilder Create()
        {
            return new ConfigBuilder(default);
        }

        public ConfigBuilder AddJsonFile(string jsonPath)
        {
            _providers.Add(new ProviderList { filePath = jsonPath, provider = new JsonProvider() });
            return new ConfigBuilder(_providers);
        }

        // TODO: call provider.Deserialize() in foreach
        public Config Build()
        {
            return new Config();
        }
    }
}
