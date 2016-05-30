using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BGL.Web.Tests
{
    [TestClass]
    public class ConfigTestFixture
    {
        [TestMethod]
        public void Ensure_If_GitUserUrl_Null_Exception_Thrown()
        {
            var repoCount = Config.RepositoryCount;


            Assert.IsTrue(repoCount > 0);

        }
    }
}
