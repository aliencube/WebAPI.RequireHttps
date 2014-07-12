using Aliencube.WebApi.RequireHttps.Interfaces;
using Aliencube.WebApi.RequireHttps.Properties;

namespace Aliencube.WebApi.RequireHttps
{
    /// <summary>
    /// This represents the entity providing configuration settings.
    /// </summary>
    public class RequireHttpsConfigurationSettingsProvider : IRequireHttpsConfigurationSettingsProvider
    {
        private readonly Settings _settings;

        /// <summary>
        /// Initialises a new instance of the CacheConfigurationSettingsProvider class.
        /// </summary>
        public RequireHttpsConfigurationSettingsProvider()
        {
            this._settings = Settings.Default;
        }

        /// <summary>
        /// Gets the value that specifies whether to use HTTPS connection or not.
        /// </summary>
        public bool UseHttps
        {
            get { return this._settings.UseHttps; }
        }
    }
}