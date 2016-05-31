using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airborne.Logging;
using FakeItEasy;
using Airborne.Services.ClientAdapter.Results;

namespace BGL.Services.Tests
{
    [TestClass]
    public class BaseServiceTestFixture
    {
        [TestMethod]
        public void Ensure_BaseService_TryExecute_Returns_Result_WithNoErrors()
        {
            var logger = A.Fake<ILogger>();

            var endpoint = new TestRequest(logger).TryExecuteSuccess();

            Assert.IsTrue(endpoint.Success);
            Assert.IsFalse(endpoint.Notifications.HasErrors());
            A.CallTo(() => logger.Error(new Exception())).WithAnyArguments().MustNotHaveHappened();
        }

        [TestMethod]
        public void Ensure_BaseService_TryExecute_Returns_Result_WithErrors()
        {
            var logger = A.Fake<ILogger>();

            var endpoint = new TestRequest(logger).TryExecuteFail();

            Assert.IsTrue(endpoint.Success);
            Assert.IsTrue(endpoint.Notifications.HasErrors());
            A.CallTo(() => logger.Error(new Exception())).WithAnyArguments().MustHaveHappened();
        }
    }

    internal class TestRequest : BaseService
    {
        public TestRequest(ILogger logger)
            :base(logger)
        {}

        public TestResult TryExecuteSuccess()
        {
            var request = new object();
            return TryExecute<TestResult>(request, (result) =>
            {
                result.Success = 1 < 2;
            });
        }

        public TestResult TryExecuteFail()
        {
            var request = new object();
            return TryExecute<TestResult>(request, (result) =>
            {
                result.Success = 1 < 2;
                throw new Exception("Error occurred.");
            });
        }
    }

    internal class TestResult : GenericServiceResult
    {
        public bool Success { get; set; }
    }
}
