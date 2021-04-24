using System.Text.Json;

namespace WindowsTermialTray.Config.Provider
{
    class JsonProvider : IProvider
    {
        private readonly string _jsonString;

        public JsonProvider(string jsonString)
        {
            _jsonString = jsonString;
        }

        public Config Deserialize()
        {
            return JsonSerializer.Deserialize<Config>(_jsonString);
        }
    }
}
