using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;
using RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Affectationabonnement")]
    public class AffectationabonnementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AffectationabonnementController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAll")]
        public async Task<ActionResult<List<Affectationabonnement>>> GetAll()
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
        [Route("GetAllByAbonneId")]
        public async Task<ActionResult<List<Affectationabonnement>>> GetAllByAbonneIdAsync(long id)
        {
            var query = new GetAllByAbonneIdQuery { AbonneID = id };
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
        [Route("GetAllByAbonnementId")]
        public async Task<ActionResult<List<Affectationabonnement>>> GetAllByAbonnementIdAsync(long id)
        {
            var query = new GetAllByAbonnementIdQuery { AbonnementID = id };
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
        [Route("GetAllByIdWithDates")]
        public async Task<ActionResult<List<Affectationabonnement>>> GetAllByIdWithDatesAsync(long id, DateTime start, DateTime finish)
        {
            var query = new GetAllByIdAndDatesQuery { Id = id, StartDate = start, FinishDate = finish };
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
        [Route("GetAllByNameAndDates")]
        public async Task<ActionResult<List<Affectationabonnement>>> GetAllByNameAndDatesAsync(string? name, DateTime start, DateTime finish)
        {
            var query = new GetAllByNameAndDatesQuery { Name = name, StartDate = start, FinishDate = finish };
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
        public async Task<ActionResult<List<Affectationabonnement>>> GetOneByIdAsync(long id)
        {
            var query = new GetOneByIdQuery { Id = id };
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

