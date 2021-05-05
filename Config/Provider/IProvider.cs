namespace WindowsTermialTray.Config.Provider
{
    interface IProvider
    {
        /// <summary>
        /// Load configurations.
        /// </summary>
        /// <exception cref="System.FormatException">Thrown when invalid data provided.</exception>
        Config Load();
    }
}
