using System.Collections.Generic;
using WindowsTermialTray.Config.Provider;

namespace WindowsTermialTray.Config
{
    // var config = ConfigBuilder.Create().AddJsonFile("./config.json").AddJsonFile(appPath).Build()
    public class ConfigBuilder
    {
        private readonly List<IProvider> _providers;

        private ConfigBuilder(List<IProvider> providers)
        {
            _providers = providers;
        }

        public static ConfigBuilder Create()
        {
            return new ConfigBuilder(default);
        }

        public ConfigBuilder AddJsonFile(string jsonPath)
        {
            _providers.Add(new JsonProvider(jsonPath));
            return new ConfigBuilder(_providers);
        }

        public Config Build()
        {
            foreach (var provider in _providers)
            {
                var config = provider.Load();
                if (config != null)
                {
                    return config;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }
    }
}
