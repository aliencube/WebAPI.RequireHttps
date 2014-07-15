using Aliencube.WebApi.RequireHttps;
using Aliencube.WebApi.RequireHttps.Interfaces;
using Aliencube.WebApi.RequireHttps.Validators;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace WebApi.RequireHttps.Tests
{
    [TestFixture]
    public class AspValidatorTest
    {
        #region SetUp / TearDown

        private HttpRequestMessage _request;
        private IApplicationServiceProviderValidator _validator;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._request != null)
                this._request.Dispose();

            if (this._validator != null)
                this._validator.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase(ApplicationServiceProviderType.None, null, null, true)]
        [TestCase(ApplicationServiceProviderType.None, "X-CustomHeader", "ftp", true)]
        [TestCase(ApplicationServiceProviderType.None, "X-Forwarded-Proto", "https", true)]
        [TestCase(ApplicationServiceProviderType.None, "X-Forwarded-Proto", "http", true)]
        [TestCase(ApplicationServiceProviderType.AppHarbor, null, null, false)]
        [TestCase(ApplicationServiceProviderType.AppHarbor, "X-CustomHeader", "ftp", false)]
        [TestCase(ApplicationServiceProviderType.AppHarbor, "X-Forwarded-Proto", "https", true)]
        [TestCase(ApplicationServiceProviderType.AppHarbor, "X-Forwarded-Proto", "http", false)]
        public void ValidateNoneAspValidator_GivenRequest_ReturnResult(ApplicationServiceProviderType aspType, string headerKey, string headerValue, bool expected)
        {
            this._request = new HttpRequestMessage(new HttpMethod("GET"), new Uri("http://localhost"));
            if (!String.IsNullOrWhiteSpace(headerKey) && !String.IsNullOrWhiteSpace(headerValue))
            {
                this._request.Headers.Add(headerKey, headerValue);
            }

            this._validator = BaseAspValidator.CreateInstance(aspType);
            this._validator.Validate(this._request).Should().Be(expected);
        }

        #endregion Tests
    }
}