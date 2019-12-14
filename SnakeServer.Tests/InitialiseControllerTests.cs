using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeServer.Controllers;

namespace SnakeServer.Tests
{
    [TestClass]
    public class InitialiseControllerTests
    {
        private InitialiseController _initialiseController;

        [TestInitialize]
        public void SetupContext()
        {
            _initialiseController = new InitialiseController();
        }

        [TestMethod]
        public void Get_GETRequest_200returned()
        {
            // Arrange:
            ObjectResult expected = new OkObjectResult(new object());
            System.Console.WriteLine(expected.Value.GetType());

            // Act:
            ObjectResult actual = _initialiseController.Get() as ObjectResult;

            // Assert:
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }
    }
}
