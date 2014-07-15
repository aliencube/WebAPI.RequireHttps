using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Aliencube.WebApi.RequireHttps.Validators
{
    /// <summary>
    /// This represents the ApplicationServiceProviderValidator entity for AppHarbor.
    /// </summary>
    public class AppHarborAspValidator : BaseAspValidator
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="request"><c>HttpRequestMessage</c> instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        public override bool Validate(HttpRequestMessage request)
        {
            //  This is for AppHarbor specified implementation.
            //  https://gist.github.com/runesoerensen/915869,
            //  https://gist.github.com/geersch/7710361
            IEnumerable<string> values;
            var scheme = request.Headers.TryGetValues("X-Forwarded-Proto", out values)
                             ? values.FirstOrDefault()
                             : null;

            var validated = !String.IsNullOrWhiteSpace(scheme) && scheme == Uri.UriSchemeHttps;
            return validated;
        }
    }
}