global using RitegeDomain.Database.Entities.ControleAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.QueryHandlers.Controller;
using RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Door")]
    public class DoorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<List<Door>>> GetOneByIdAsync(long id)
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
    }

}

