using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeServer.Controllers;
using System.Collections.Generic;

namespace SnakeServer.Tests
{
    [TestClass]
    public class DirectionControllerTests
    {
        private DirectionController _directionController;

        [TestInitialize]
        public void SetupContext()
        {
            _directionController = new DirectionController();
        }

        [TestMethod]
        public void Post_validDirection_200returned()
        {
            // Arrange:
            ObjectResult expected =
                new OkObjectResult(new object());

            ICollection<Models.RequestBody> validDirection =
                new LinkedList<Models.RequestBody>();

            validDirection.Add(new Models.RequestBody() { direction = "Top" });
            validDirection.Add(new Models.RequestBody() { direction = "Bottom" });
            validDirection.Add(new Models.RequestBody() { direction = "Left" });
            validDirection.Add(new Models.RequestBody() { direction = "Right" });

            // Act:
            ICollection<ObjectResult> actuals =
                new LinkedList<ObjectResult>();
            foreach (var i in validDirection)
                actuals.Add(_directionController.Post(i) as ObjectResult);

            // Assert:
            foreach (var i in actuals)
                Assert.AreEqual(expected.StatusCode, i.StatusCode);
        }

        [TestMethod]
        public void Post_invalidDirection_400returned()
        {
            // Arrange:
            ObjectResult expected =
                new BadRequestObjectResult(new object());

            ICollection<Models.RequestBody> invalidDirection =
                new LinkedList<Models.RequestBody>();

            invalidDirection.Add(new Models.RequestBody() { direction = "Hello world!" });
            invalidDirection.Add(new Models.RequestBody() { direction = "1" });
            invalidDirection.Add(new Models.RequestBody() { direction = "11111" });
            invalidDirection.Add(new Models.RequestBody() { direction = "q" });
            invalidDirection.Add(new Models.RequestBody() { direction = "qqqqq" });
            invalidDirection.Add(new Models.RequestBody() { direction = "top" });
            invalidDirection.Add(new Models.RequestBody() { direction = "bottom" });
            invalidDirection.Add(new Models.RequestBody() { direction = "right" });
            invalidDirection.Add(new Models.RequestBody() { direction = "left" });

            // Act:
            ICollection<ObjectResult> actuals =
                new LinkedList<ObjectResult>();
            foreach (var i in invalidDirection)
                actuals.Add(_directionController.Post(i) as ObjectResult);

            // Assert:
            foreach (var i in actuals)
                Assert.AreEqual(expected.StatusCode, i.StatusCode);
        }
    }
}
