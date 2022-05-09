using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.SessionQueries;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Session")]
    public class SessionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllByIdAndDate")]
        public async Task<ActionResult<List<Session>>> GetAllByIdAndDateAsync(long id, DateTime start, DateTime end)
        {
            var query = new GetAllByIdAndDateQuery { Id = id, Start = start, End = end };
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllByCaisseAndDate")]
        public async Task<ActionResult<List<Session>>> GetAllByCaisseAndDateAsync(long id, DateTime start, DateTime end)
        {
            var query = new GetAllByCaisseAndDateQuery { Id = id, Start = start, End = end };
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}

