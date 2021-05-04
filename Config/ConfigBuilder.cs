using System.Collections.Generic;
using System.IO;
using WindowsTermialTray.Config.Provider;

namespace WindowsTermialTray.Config
{
    public class ConfigBuilder
    {
        private readonly List<IProvider> _providers;

        private ConfigBuilder(List<IProvider> providers)
        {
            _providers = providers;
        }

        public static ConfigBuilder Create()
        {
            return new ConfigBuilder(new List<IProvider> { });
        }

        public ConfigBuilder AddDefault()
        {
            _providers.Add(new DefaultProvider());
            return new ConfigBuilder(_providers);
        }

        public ConfigBuilder AddJsonFile(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                var jsonString = File.ReadAllText(jsonPath);
                _providers.Add(new JsonProvider(jsonString));
            }

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
