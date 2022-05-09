global using RitegeDomain.Database.Entities.Parking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.EvenementQueries;
using RitegeDomain.QueryHandlers.EvenementQueryHandlers;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Evenement")]
    public class EvenementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EvenementController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllByDate")]
        public async Task<ActionResult<List<Evenement>>> GetAllByDateAsync(DateTime date)
        {
            var query = new GetAllByDateQuery { Date = date };
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
        [Route("GetAllByBorneAndDate")]
        public async Task<ActionResult<List<Evenement>>> GetAllByBorneAndDateAsync(long id, DateTime date, bool AlertsOnly)
        {
            var query = new GetAllByBorneAndDateQuery { Id = id, Date = date,AlertsOnly=AlertsOnly};
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
        public async Task<ActionResult<List<Evenement>>> GetAllByCaisseAndDateAsync(long id, DateTime date)
        {
            var query = new GetAllByCaisseAndDateQuery { Id = id, Date = date };
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
        [Route("GetAllByCaisseAndBorneAndDate")]
        public async Task<ActionResult<List<Evenement>>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, long idBorne, DateTime date)
        {
            var query = new GetAllByCaisseAndBorneAndDateQuery { idCaisse = idCaisse, idBorne = idBorne, Date = date };
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
        [Route("GetLast10ByBorne")]
        public async Task<ActionResult<List<Evenement>>> GetLast10ByBorne(long id)
        {
            var query = new GetLast10ByBorneQuery { Id = id };
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
        [Route("GetLast10ByCaisse")]
        public async Task<ActionResult<List<Evenement>>> GetLast10ByCaisse(long id)
        {
            var query = new GetLast10ByCaisseQuery { Id = id };
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
        [Route("GetLast10ByCaisseAndBorne")]
        public async Task<ActionResult<List<Evenement>>> GetLast10ByCaisseAndBorne(long idCaisse,long idBorne)
        {
            var query = new GetLast10ByCaisseAndBorneQuery {idCaisse  = idCaisse ,idBorne=idBorne};
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
        [Route("GetLast10")]
        public async Task<ActionResult<List<Evenement>>> GetLast10()
        {
            var query = new GetLast10Query { };
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

