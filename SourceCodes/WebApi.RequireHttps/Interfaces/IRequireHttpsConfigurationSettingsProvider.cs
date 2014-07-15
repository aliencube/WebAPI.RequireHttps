﻿using System.Collections.Generic;

namespace Aliencube.WebApi.RequireHttps.Interfaces
{
    /// <summary>
    /// This provides interfaces to the RequireHttpsConfigurationSettingsProvider class.
    /// </summary>
    public interface IRequireHttpsConfigurationSettingsProvider
    {
        /// <summary>
        /// Gets the value that specifies whether to use HTTPS connection or not.
        /// </summary>
        bool UseHttps { get; }

        /// <summary>
        /// Gets the application service providers.
        /// </summary>
        IEnumerable<ApplicationServiceProviderType> ApplicationServiceProviders { get; }
    }
}