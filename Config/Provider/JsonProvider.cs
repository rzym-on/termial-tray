using System;
using System.IO;
using System.Text.Json;

namespace WindowsTermialTray.Config.Provider
{
    class JsonProvider : IProvider
    {
        private readonly string _jsonPath;

        public JsonProvider(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public Config Load()
        {
            if (!File.Exists(_jsonPath))
            {
                return null;
            }

            var jsonString = File.ReadAllText(_jsonPath);

            try
            {
                return JsonSerializer.Deserialize<Config>(jsonString);
            }
            catch (JsonException e)
            {
                throw new FormatException($"invalid json: {_jsonPath}", e);
            }
        }
    }
}
