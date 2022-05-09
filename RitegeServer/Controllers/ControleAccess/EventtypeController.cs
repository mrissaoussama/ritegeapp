using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventtypeQueries;
using RitegeDomain.QueryHandlers.Eventtype;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Eventtype")]
    public class EventtypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventtypeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<List<Eventtype>>> GetOneByIdAsync(long id)
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
        [Route("GetAllQuery")]
        public async Task<ActionResult<List<Eventtype>>> GetAllAsync()
        {
            var query = new GetAllQuery();
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

