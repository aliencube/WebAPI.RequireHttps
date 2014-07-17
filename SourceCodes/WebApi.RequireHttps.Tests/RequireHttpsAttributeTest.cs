using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;

namespace Aliencube.WebApi.RequireHttps.Tests
{
    [TestFixture]
    public class RequireHttpsAttributeTest
    {
        #region SetUp / TearDown

        private HttpRequestMessage _request;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._request != null)
                this._request.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase("http", "/repos/user/repo/git/refs/heads/master", "http", HttpStatusCode.Forbidden)]
        [TestCase("http", "/repos/user/repo/git/refs/heads/master", "https", null)]
        [TestCase("https", "/repos/user/repo/git/refs/heads/master", null, null)]
        public void OnAuthorization_GivenActionContext_ReturnResult(string protocol, string path, string headerProtocol, HttpStatusCode? expected)
        {
            var url = String.Format("{0}://localhost{1}", protocol, path);
            this._request = new HttpRequestMessage(new HttpMethod("GET"), new Uri(url));
            this._request.Headers.Add("X-Forwarded-Proto", headerProtocol);

            var actionContext = ContextUtil.GetActionContext(this._request);

            var requireHttps = new RequireHttpsAttribute()
                               {
                                   RequireHttpsConfigurationSettingsProviderType = typeof(RequireHttpsConfigurationSettingsProvider)
                               };
            requireHttps.OnAuthorization(actionContext);

            if (expected == null)
            {
                actionContext.Response.Should().BeNull();
            }
            else
            {
                actionContext.Response.StatusCode.Should().Be(expected);
            }
        }

        #endregion Tests
    }
}