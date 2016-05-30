using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BGL.Services;

namespace BGL.Services.Tests
{
    [TestClass]
    public class ConfigTestFixture
    {
        [TestMethod]
        public void Ensure_If_GitUserUrl_Null_Exception_Thrown()
        {
            var errorThrown = false;

            try
            {
                var url = Config.GitUserUrl;
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(NullReferenceException));
                errorThrown = true;
            }

            Assert.IsTrue(errorThrown);
            
        }
    }
}
