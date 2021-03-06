﻿using Aliencube.WebApi.RequireHttps.Interfaces;
using Aliencube.WebApi.RequireHttps.Validators;
using System;
using System.Linq;
using System.Web.Http.Controllers;

namespace Aliencube.WebApi.RequireHttps
{
    /// <summary>
    /// This represents an entity to help <c>RequireHttpsAttribute</c> class.
    /// </summary>
    public class RequireHttpsHelper : IRequireHttpsHelper
    {
        public RequireHttpsHelper(IRequireHttpsConfigurationSettingsProvider settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.Settings = settings;
        }

        /// <summary>
        /// Gets the require https configuration settings.
        /// </summary>
        public IRequireHttpsConfigurationSettingsProvider Settings { get; private set; }

        /// <summary>
        /// Checks whether the current connection is over HTTPS or not.
        /// </summary>
        /// <param name="actionContext">The action context instance.</param>
        /// <returns>Returns <c>True</c>, if the current connection is over HTTPS; otherwise returns <c>False</c>.</returns>
        public bool IsHttpsConnection(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            if (this.Settings.BypassHttps)
            {
                return true;
            }

            if (actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps)
            {
                return true;
            }

            var providers = this.Settings.ApplicationServiceProviders;
            var result = providers.Select(BaseAspValidator.CreateInstance)
                                  .Select(validator => validator.Validate(actionContext.Request))
                                  .All(validated => validated);
            return result;
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