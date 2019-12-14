using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeServer.Controllers;
using SnakeServer.Models;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeServer.Tests
{
    [TestClass]
    public class GameboardControllerTests
    {
        private GameboardController _gameboardController;

        [TestInitialize]
        public void SetupContext()
        {
            _gameboardController = new GameboardController();
        }

        [TestMethod]
        public void Get_UninitializedResponse_ResponseBodyreturned()
        {
            // Arrange:
            ResponseBody expected = new ResponseBody(
                0, 0,
                new GameBoardSize(0, 0),
                new PointF(0, 0),
                new List<PointF>(),
                new List<PointF>(),
                0, 0, 0);

            // Act:
            ObjectResult actual = _gameboardController.Get() as ObjectResult;

            // Assert:
            Assert.AreEqual(expected.currentPosition, (actual.Value as ResponseBody).currentPosition);
            Assert.AreEqual(expected.gameBoardSize, (actual.Value as ResponseBody).gameBoardSize);
            Assert.AreEqual(expected.GameScore, (actual.Value as ResponseBody).GameScore);
            Assert.AreEqual(expected.GameStatus, (actual.Value as ResponseBody).GameStatus);
            Assert.AreEqual(expected.SnakeLength, (actual.Value as ResponseBody).SnakeLength);
            Assert.AreEqual(expected.timeUntilNextTurnMilliseconds, (actual.Value as ResponseBody).timeUntilNextTurnMilliseconds);
            Assert.AreEqual(expected.turnNumber, (actual.Value as ResponseBody).turnNumber);
        }
    }
}
