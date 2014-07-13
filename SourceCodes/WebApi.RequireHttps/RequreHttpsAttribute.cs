using Aliencube.WebApi.RequireHttps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aliencube.WebApi.RequireHttps
{
    /// <summary>
    /// This represents the customer action filter attribute entity to enable HTTPS connection.
    /// </summary>
    public class RequreHttpsAttribute : AuthorizationFilterAttribute
    {
        private Type _requireHttpsConfigurationSettingsProviderType;
        private IRequireHttpsConfigurationSettingsProvider _settings;

        /// <summary>
        /// Initialises a new instance of the RequreHttpsAttribute class.
        /// </summary>
        public RequreHttpsAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the type of the configuration settings provider.
        /// </summary>
        public Type RequireHttpsConfigurationSettingsProviderType
        {
            get
            {
                return this._requireHttpsConfigurationSettingsProviderType;
            }
            set
            {
                this._requireHttpsConfigurationSettingsProviderType = value;
                this._settings = Activator.CreateInstance(this._requireHttpsConfigurationSettingsProviderType) as IRequireHttpsConfigurationSettingsProvider;
            }
        }

        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <c>System.Web.Http.Filters.AuthorizationFilterAttribute</c>.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!this._settings.UseHttps)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            if (actionContext.Request.IsLocal())
            {
                base.OnAuthorization(actionContext);
                return;
            }

            if (actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            //  This is for AppHarbor specified implementation.
            //  https://gist.github.com/runesoerensen/915869,
            //  https://gist.github.com/geersch/7710361
            IEnumerable<string> values;
            var scheme = actionContext.Request
                                      .Headers
                                      .TryGetValues("X-Forwarded-Proto", out values)
                             ? values.FirstOrDefault()
                             : null;
            if (!String.IsNullOrWhiteSpace(scheme) && scheme == Uri.UriSchemeHttps)
            {
                base.OnAuthorization(actionContext);
                return;
            }

            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                                     {
                                         ReasonPhrase = "HTTPS Required"
                                     };
        }
    }
}