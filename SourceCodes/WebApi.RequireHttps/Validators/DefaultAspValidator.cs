using System.Net.Http;

namespace Aliencube.WebApi.RequireHttps.Validators
{
    /// <summary>
    /// This represents the ApplicationServiceProviderValidator entity for none.
    /// </summary>
    public class DefaultAspValidator : BaseAspValidator
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="request"><c>HttpRequestMessage</c> instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        public override bool Validate(HttpRequestMessage request)
        {
            return false;
        }
    }
}