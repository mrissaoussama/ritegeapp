using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.AbonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Abonnement")]
    public class AbonnementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AbonnementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAll")]
        public async Task<ActionResult<List<Abonnement>>> GetAllAsync()
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
        public async Task<ActionResult<List<Abonnement>>> GetOneByIdAsync(long id)
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

