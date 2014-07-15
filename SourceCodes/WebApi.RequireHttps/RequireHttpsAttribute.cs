using Aliencube.WebApi.RequireHttps.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aliencube.WebApi.RequireHttps
{
    /// <summary>
    /// This represents the customer action filter attribute entity to enable HTTPS connection.
    /// </summary>
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        private Type _requireHttpsConfigurationSettingsProviderType;
        private IRequireHttpsConfigurationSettingsProvider _settings;
        private IRequireHttpsHelper _helper;

        /// <summary>
        /// Initialises a new instance of the RequreHttpsAttribute class.
        /// </summary>
        public RequireHttpsAttribute()
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
                this._helper = new RequireHttpsHelper(this._settings);
            }
        }

        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <c>System.Web.Http.Filters.AuthorizationFilterAttribute</c>.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var isHttpsConnection = this._helper.IsHttpsConnection(actionContext);
            if (!isHttpsConnection)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                                         {
                                             ReasonPhrase = "HTTPS Required"
                                         };
                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}