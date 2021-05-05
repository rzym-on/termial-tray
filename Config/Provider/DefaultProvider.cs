namespace WindowsTermialTray.Config.Provider
{
    class DefaultProvider : IProvider
    {
        public Config Load()
        {
            return new DefaultConfig();
        }
    }
}
