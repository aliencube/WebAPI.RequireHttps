using System;
using System.Net.Http;

namespace Aliencube.WebApi.RequireHttps.Interfaces
{
    public interface IApplicationServiceProviderValidator : IDisposable
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="request"><c>HttpRequestMessage</c> instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        bool Validate(HttpRequestMessage request);
    }
}