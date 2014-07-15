using Aliencube.WebApi.RequireHttps.Interfaces;
using System.Net.Http;

namespace Aliencube.WebApi.RequireHttps.Validators
{
    /// <summary>
    /// This represents the validator entity for application service providers. This must be inherited.
    /// </summary>
    public abstract class BaseAspValidator : IApplicationServiceProviderValidator
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="request"><c>HttpRequestMessage</c> instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        public abstract bool Validate(HttpRequestMessage request);

        /// <summary>
        /// Creates the ApplicationServiceProviderValidator instance based on the ApplicationServiceProviderType value.
        /// </summary>
        /// <param name="aspType">ApplicationServiceProviderType value.</param>
        /// <returns>Returns the ApplicationServiceProviderValidator instance.</returns>
        public static BaseAspValidator CreateInstance(ApplicationServiceProviderType aspType)
        {
            switch (aspType)
            {
                case ApplicationServiceProviderType.Default:
                    return new DefaultAspValidator();
                    break;

                case ApplicationServiceProviderType.AppHarbor:
                    return new AppHarborAspValidator();
                    break;

                default:
                    return new UnknownAspValidator();
                    break;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}