﻿using System;
using System.Linq;
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
                var config = JsonSerializer.Deserialize<Config>(_jsonString);
                var hasNull = config.Apps.Any(a => a.GetType().GetProperties().Any(p => p.GetValue(a) == default));
                if (hasNull)
                {
                    throw new FormatException("json includes null value or some fields are missing");
                }

                return config;
            }
            catch (JsonException e)
            {
                throw new FormatException($"invalid json", e);
            }
        }
    }
}
