using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeServer.Controllers;

namespace SnakeServer.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _homeController;
        private ViewResult _homePage;

        [TestInitialize]
        public void SetupContext()
        {
            _homeController = new HomeController();
            _homePage = _homeController.HomePage() as ViewResult;
        }

        [TestMethod]
        public void HomePage_ViewDataMessageReturned()
        {
            // Arrange:
            string expected = "Game start page";

            // Act:
            string actual = _homePage.ViewData["Message"] as string;

            // Assert:
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HomePage_ViewNameEqualHomePageReturned()
        {
            // Arrange:
            string expected = "HomePage";

            // Act:
            string actual = _homePage.ViewName;

            // Assert:
            Assert.AreEqual(expected, _homePage.ViewName);
        }

        [TestMethod]
        public void HomePage_ViewResultNotNullReturned()
        {
            // Arrange:

            // Act:
            var actual = _homePage;

            // Assert:
            Assert.IsNotNull(actual);
        }
    }
}
