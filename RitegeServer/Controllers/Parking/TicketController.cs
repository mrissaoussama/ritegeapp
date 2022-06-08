using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.Parking;
using RitegeDomain.Database.Queries.ParkingDBQueries.TicketQueries;
using RitegeDomain.QueryHandlers.TicketQueryHandlers;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllByIdAndDate")]
        public async Task<ActionResult<List<Ticket>>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end)
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

    }

}

