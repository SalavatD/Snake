using Microsoft.AspNetCore.Mvc;
using System;

namespace SnakeServer.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class InitialiseController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Models.InitialiseResponseBody), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Models.GameLogic.gameLogic.InitialiseGame();
                return Ok(new Models.InitialiseResponseBody(Models.GameLogic.gameLogic.timeUntilNextTurnMilliseconds));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
