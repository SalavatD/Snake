using Microsoft.AspNetCore.Mvc;
using System;

namespace SnakeServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameboardController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Models.ResponseBody), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                return Ok(Models.GameLogic.gameLogic.GameState());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
