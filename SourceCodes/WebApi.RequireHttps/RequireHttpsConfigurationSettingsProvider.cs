using Aliencube.WebApi.RequireHttps.Interfaces;
using Aliencube.WebApi.RequireHttps.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Gets the application service providers.
        /// </summary>
        public IEnumerable<ApplicationServiceProviderType> ApplicationServiceProviders
        {
            get
            {
                ApplicationServiceProviderType result;
                var types = this._settings
                                .ApplicationServiceProviders
                                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => Enum.TryParse(p, true, out result) ? result : ApplicationServiceProviderType.None)
                                .Where(p => p != ApplicationServiceProviderType.None)
                                .ToList();

                return types.Any()
                           ? types
                           : new List<ApplicationServiceProviderType> { ApplicationServiceProviderType.None };
            }
        }
    }
}