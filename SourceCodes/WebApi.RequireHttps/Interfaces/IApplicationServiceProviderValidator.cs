using System.Web.Http.Controllers;

namespace Aliencube.WebApi.RequireHttps.Interfaces
{
    public interface IApplicationServiceProviderValidator
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="actionContext">The action context instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        bool Validate(HttpActionContext actionContext);
    }
}