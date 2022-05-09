using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventQueries;
using RitegeDomain.QueryHandlers.Event;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Event")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<List<Event>>> GetOneByIdAsync(long id)
        {
            var query = new GetOneByIdQuery() { Id = id };
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
        [Route("GetAllByDate")]
        public async Task<ActionResult<List<Event>>> GetAllByDateAsync(DateTime date)
        {
            var query = new GetAllByDateQuery() { Date = date };
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

