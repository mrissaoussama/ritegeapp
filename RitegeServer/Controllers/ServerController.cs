using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RitegeServer.Hubs;

namespace RitegeServer.ServerControllers
{
  
    [ApiController]
    [Route("Server")]
    public class ServerController : ControllerBase
    {
        private readonly IHubContext<DataHub> _hubContext;
        private readonly IMediator _mediator;

        public ServerController(IHubContext<DataHub> hubContext,IMediator mediator)
        {    _hubContext = hubContext;
            _mediator = mediator;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SendAlert")]
        public async Task<ActionResult<string>> SendAlert(ParkingEvent parkingEvent)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("DangerousEventReceived", parkingEvent);

                return Ok("sent alerts to " +0);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SendDelayedAlert")]
        public ActionResult<string> SendDelayedAlert(string key, string data, string eventtype)
        {
            try
            {

                return Ok("sent alerts to " +0);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SendNewToken")]
        public ActionResult<string> SendNewToken(string token)
        {
            try
            {
                return Ok("added");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddEvent")]
        public async Task<ActionResult<string>> AddEvent(RitegeDomain.Database.Commands.Parking.EvenementCommands.CreateEvenementCommand eventToAdd)
        {
            try
            {
                var response = await _mediator.Send(eventToAdd);

            
                if (Enum.IsDefined(typeof(AlertCodes), eventToAdd.TypeEvent))
                {
                    var parkingEvent = new ParkingEvent { DateEvent = eventToAdd.DateEvent, DescriptionEvent = eventToAdd.DescriptionEvent, ParkingId = eventToAdd.ParkingId, TypeEvent = eventToAdd.TypeEvent };
                    //var clientid = parkingEvent.ParkingId.ToString();
                    var clientid = 1;
                   await _hubContext.Clients.Group(clientid.ToString()).SendAsync("AlertReceived", parkingEvent);

                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ResetToken")]
        public async Task<ActionResult> ResetToken()
        {
            try
            {
              
                    await _hubContext.Clients.All.SendAsync("ResetToken");
                return Ok();
                }

            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

