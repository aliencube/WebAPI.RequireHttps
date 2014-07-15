using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Aliencube.WebApi.RequireHttps.Interfaces;
using NUnit.Framework;

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

        #endregion

        #region Tests

        [Test]
        public void Test()
        {
        }

        #endregion
    }
}
