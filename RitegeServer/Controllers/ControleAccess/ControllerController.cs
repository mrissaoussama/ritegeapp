using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.QueryHandlers.Controller;
using RitegeDomain.Database.Queries.ControleAccess.ControllerQueries;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Controller")]
    public class ControllerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ControllerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAll")]
        public async Task<ActionResult<List<RitegeDomain.Database.Entities.ControleAccess.Controller>>> GetAllAsync()
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<RitegeDomain.Database.Entities.ControleAccess.Controller>> GetOneByIdAsync(long id)
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

