﻿using System.Web.Http.Controllers;

namespace Aliencube.WebApi.RequireHttps.Validators
{
    /// <summary>
    /// This represents the ApplicationServiceProviderValidator entity for default.
    /// </summary>
    public class NoneAspValidator : BaseAspValidator
    {
        /// <summary>
        /// Validates the action context for application service provider.
        /// </summary>
        /// <param name="actionContext">The action context instance.</param>
        /// <returns>Returns <c>True</c>, if validated; otherwise returns <c>False</c>.</returns>
        public override bool Validate(HttpActionContext actionContext)
        {
            return true;
        }
    }
}