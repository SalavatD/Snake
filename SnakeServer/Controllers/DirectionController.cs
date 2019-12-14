using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SnakeServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectionController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]Models.RequestBody body)
        {
            List<string> directions = new List<string> { "Top", "Bottom", "Left", "Right" };
            try
            {
                if (directions.Contains(body.direction))
                {
                    Models.GameLogic.gameLogic.ChangeMoving(body.direction);
                    return Ok(body.direction);
                }
                else
                    throw new Exception("Invalid direction.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
