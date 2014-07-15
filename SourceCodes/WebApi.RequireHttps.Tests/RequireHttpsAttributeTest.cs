using System.Net;
using System.Net.Sockets;
using Aliencube.WebApi.RequireHttps;
using Aliencube.WebApi.RequireHttps.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WebApi.RequireHttps.Tests
{
    [TestFixture]
    public class RequireHttpsAttributeTest
    {
        #region SetUp / TearDown

        private HttpRequestMessage _request;
        private IRequireHttpsHelper _helper;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._request != null)
                this._request.Dispose();

            if (this._helper != null)
                this._helper.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase("http", "/repos/user/repo/git/refs/heads/master", "http", HttpStatusCode.Forbidden)]
        [TestCase("http", "/repos/user/repo/git/refs/heads/master", "https", null)]
        [TestCase("https", "/repos/user/repo/git/refs/heads/master", null, null)]
        public void OnActionExecuting_GivenActionContext_ReturnResult(string protocol, string path, string headerProtocol, HttpStatusCode? expected)
        {
            var url = String.Format("{0}://localhost{1}", protocol, path);
            this._request = new HttpRequestMessage(new HttpMethod("GET"), new Uri(url));
            this._request.Headers.Add("X-Forwarded-Proto", headerProtocol);

            var actionContext = ContextUtil.GetActionContext(this._request);

            var requireHttps = new RequireHttpsAttribute()
                               {
                                   RequireHttpsConfigurationSettingsProviderType = typeof (RequireHttpsConfigurationSettingsProvider)
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