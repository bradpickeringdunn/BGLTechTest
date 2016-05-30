using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BGL.Services.Api.Models.Dto;
using BGL.Web.Rules.Validation;
using BGL.Services.Api.Models.Result;

namespace BGL.Web.Tests.Rules.Validation
{
    [TestClass]
    public class ServiceResultValidationTestFixture
    {
        [TestMethod]
        public void Ensure_GetUserResult_Is_Valid()
        {
            var result = new GetUserResult()
            {
                User = new UserDto()
                {
                    AvatarUrl = "Null",
                    Name = "Name",
                    Location = "Location"
                }
            };

            Assert.IsTrue(result.IsValid());
        }
    }
}
