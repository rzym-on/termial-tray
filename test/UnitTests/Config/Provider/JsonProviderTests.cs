using System;
using WindowsTermialTray.Config.Provider;
using WindowsTermialTray.Keys;
using Xunit;

namespace UnitTests
{
    public class JsonProviderTests
    {
        [Fact]
        public void LoadFromValidJson()
        {
            var json = @"
{
    ""Apps"": [
        {
            ""ProcessName"": ""Windows Terminal"",
            ""ExeFilePath"": ""wt.exe"",
            ""ModifierKeys"": 1,
            ""Keys"": 192
        }
    ]
}";
            var jsonProvider = new JsonProvider(json);
            var config = jsonProvider.Load();

            Assert.Equal("Windows Terminal", config.Apps[0].ProcessName);
            Assert.Equal("wt.exe", config.Apps[0].ExeFilePath);
            Assert.Equal(ModifierKeys.Alt, config.Apps[0].ModifierKeys);
            Assert.Equal(System.Windows.Forms.Keys.Oemtilde, config.Apps[0].Keys);
        }

        [Fact]
        public void LoadFromInvalidJson()
        {
            var json = @"""test""";
            var jsonProvider = new JsonProvider(json);
            var exception = Assert.Throws<FormatException>(
                () => jsonProvider.Load());

            Assert.NotNull(exception.Message);
        }

        [Fact]
        public void LoadFromInvalidJson_MissingFields()
        {
            var json = @"
{
    ""Apps"": [
        {
            ""ProcessName"": ""Windows Terminal""
        }
    ]
}";
            var jsonProvider = new JsonProvider(json);
            var exception = Assert.Throws<FormatException>(
                () => jsonProvider.Load());

            Assert.NotNull(exception.Message);
        }
    }
}
