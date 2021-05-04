using System;
using System.Text.Json;

namespace WindowsTermialTray.Config.Provider
{
    public class JsonProvider : IProvider
    {
        private readonly string _jsonString;

        public JsonProvider(string jsonString)
        {
            _jsonString = jsonString;
        }

        public Config Load()
        {
            try
            {
                return JsonSerializer.Deserialize<Config>(_jsonString);
            }
            catch (JsonException e)
            {
                throw new FormatException($"invalid json", e);
            }
        }
    }
}
