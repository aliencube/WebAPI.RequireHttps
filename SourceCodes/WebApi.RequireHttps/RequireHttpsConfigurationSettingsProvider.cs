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
        public bool BypassHttps
        {
            get { return this._settings.BypassHttps; }
        }

        /// <summary>
        /// Gets the application service providers.
        /// </summary>
        public IEnumerable<ApplicationServiceProviderType> ApplicationServiceProviders
        {
            get
            {
                var value = this._settings.ApplicationServiceProviders;
                if (String.IsNullOrWhiteSpace(value))
                {
                    return new List<ApplicationServiceProviderType> { ApplicationServiceProviderType.Default };
                }

                ApplicationServiceProviderType result;
                var providers = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(p => Enum.TryParse(p, true, out result) ? result : ApplicationServiceProviderType.Default);

                return providers;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}