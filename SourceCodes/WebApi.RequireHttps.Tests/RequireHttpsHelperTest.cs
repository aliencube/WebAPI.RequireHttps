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
    public class RequireHttpsHelperTest
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
        [TestCase(true, "http", "http", null, true)]
        [TestCase(true, "http", "http", "", true)]
        [TestCase(true, "http", "http", "Default", true)]
        [TestCase(true, "http", "http", "AppHarbor", true)]
        [TestCase(true, "http", "http", "Default,AppHarbor", true)]
        [TestCase(true, "http", "http", "Default,GoDaddy", true)]
        [TestCase(true, "https", "https", null, true)]
        [TestCase(true, "https", "https", "", true)]
        [TestCase(true, "https", "https", "Default", true)]
        [TestCase(true, "https", "https", "AppHarbor", true)]
        [TestCase(true, "https", "https", "Default,AppHarbor", true)]
        [TestCase(true, "https", "https", "Default,GoDaddy", true)]
        [TestCase(false, "http", "http", null, false)]
        [TestCase(false, "http", "http", "", false)]
        [TestCase(false, "http", "http", "Default", false)]
        [TestCase(false, "http", "http", "AppHarbor", false)]
        [TestCase(false, "http", "http", "Default,AppHarbor", false)]
        [TestCase(false, "http", "http", "Default,GoDaddy", false)]
        [TestCase(false, "http", "https", null, false)]
        [TestCase(false, "http", "https", "", false)]
        [TestCase(false, "http", "https", "Default", false)]
        [TestCase(false, "http", "https", "AppHarbor", true)]
        [TestCase(false, "http", "https", "Default,AppHarbor", false)]
        [TestCase(false, "http", "https", "AppHarbor,GoDaddy", false)]
        [TestCase(false, "https", "https", null, true)]
        [TestCase(false, "https", "https", "", true)]
        [TestCase(false, "https", "https", "Default", true)]
        [TestCase(false, "https", "https", "AppHarbor", true)]
        [TestCase(false, "https", "https", "Default,AppHarbor", true)]
        [TestCase(false, "https", "https", "AppHarbor,GoDaddy", true)]
        public void ValidatesHttpsConnection_GivenActionContext_ReturnResult(bool bypassHttps, string protocol, string headerProtocol, string providers, bool expected)
        {
            this._request = new HttpRequestMessage(new HttpMethod("GET"), new Uri(String.Format("{0}://localhost", protocol)));
            this._request.Headers.Add("X-Forwarded-Proto", headerProtocol);
            var actionContext = ContextUtil.GetActionContext(this._request);

            ApplicationServiceProviderType result;
            var asps = String.IsNullOrWhiteSpace(providers)
                           ? new List<ApplicationServiceProviderType> { ApplicationServiceProviderType.Default }
                           : providers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(p => Enum.TryParse(p, true, out result) ? result : ApplicationServiceProviderType.Unknown);

            var settings = Substitute.For<IRequireHttpsConfigurationSettingsProvider>();
            settings.BypassHttps.Returns(bypassHttps);
            settings.ApplicationServiceProviders.Returns(asps);

            this._helper = new RequireHttpsHelper(settings);
            this._helper.IsHttpsConnection(actionContext).Should().Be(expected);
        }

        #endregion Tests
    }
}