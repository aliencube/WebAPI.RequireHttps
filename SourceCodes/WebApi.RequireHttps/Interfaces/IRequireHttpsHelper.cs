using System;
using System.Web.Http.Controllers;

namespace Aliencube.WebApi.RequireHttps.Interfaces
{
    /// <summary>
    /// This provides interfaces to the RequireHttpsHelper class.
    /// </summary>
    public interface IRequireHttpsHelper : IDisposable
    {
        /// <summary>
        /// Gets the require https configuration settings.
        /// </summary>
        IRequireHttpsConfigurationSettingsProvider Settings { get; }

        /// <summary>
        /// Checks whether the current connection is over HTTPS or not.
        /// </summary>
        /// <param name="actionContext">The action context instance.</param>
        /// <returns>Returns <c>True</c>, if the current connection is over HTTPS; otherwise returns <c>False</c>.</returns>
        bool IsHttpsConnection(HttpActionContext actionContext);
    }
}