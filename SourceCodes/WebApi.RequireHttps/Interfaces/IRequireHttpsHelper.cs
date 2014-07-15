using System.Web.Http.Controllers;

namespace Aliencube.WebApi.RequireHttps.Interfaces
{
    /// <summary>
    /// This provides interfaces to the RequireHttpsHelper class.
    /// </summary>
    public interface IRequireHttpsHelper
    {
        /// <summary>
        /// Gets the require https configuration settings.
        /// </summary>
        IRequireHttpsConfigurationSettingsProvider Settings { get; }

        /// <summary>
        /// Validates whether the HTTPS connection is allowed or not.
        /// </summary>
        /// <param name="actionContext">The action context instance.</param>
        /// <returns>Returns <c>True</c>, if the HTTPS connection is allowed; otherwise returns <c>False</c>.</returns>
        bool ValidateHttpsConnection(HttpActionContext actionContext);
    }
}