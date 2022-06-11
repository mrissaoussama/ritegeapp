using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RitegeDomain.DTO;
using RitegeServer.Hubs;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Commands.Parking.EvenementCommands;
using RitegeDomain.Database.Commands.Parking.TicketCommands;
using RitegeDomain.Database.Commands.EventCommands;
using System.Linq;

namespace RitegeServer.ServerControllers
{
  
    [ApiController]
    [Route("Server")]
    public class ServerController : ControllerBase
    {
        IMobileClientHandler mobileClientHandler;
    
        private readonly IMediator _mediator;

        public ServerController(IHubContext<DataHub> hubContext,IMediator mediator, IMobileClientHandler mobileClientHandler)
        { 
            _mediator = mediator; 
            this.mobileClientHandler = mobileClientHandler;

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddEvent")]
        public async Task<ActionResult<string>> AddEvent(CreateEventCommand eventToAdd)
        {
            try
            {
                var response = await _mediator.Send(eventToAdd);

 var societequery = new RitegeDomain.Database.Queries.ControleAccess.SocieteQueries.GetOneByIdDoorQuery
                    {
                        IdDoor = eventToAdd.DoorNumber
                    };
                    Societe? societeResponse = await _mediator.Send(societequery);
                    var dto = new EventDTO
                    {
                        CodeEvent = eventToAdd.CodeEvent,
                        DateEvent = eventToAdd.DateEvent,
                        DoorNumber = eventToAdd.DoorNumber,
                        UserNumber = eventToAdd.UserNumber,
                        Flux = eventToAdd.Flux,
                        HeureEvent = eventToAdd.HeureEvent
                    };
                await mobileClientHandler.SendEventToListeningClients(dto, societeResponse.IdSociete);
                if (AlertString.AlertCodes.Contains(Convert.ToString( eventToAdd.CodeEvent)))
                {

                    await mobileClientHandler.SendAlertToClients(dto, societeResponse.IdSociete);

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
        [Route("AddTicket")]
        public async Task<ActionResult<string>> AddTicket(CreateTicketCommand ticketToAdd)
        {
            try
            {
                Ticket? AddTicketResponse = await _mediator.Send(ticketToAdd);
                var TicketBorneQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.BorneQueries.GetOneByIdQuery
                {
                    Id = ticketToAdd.idBorneEntree
                };
                Borne? TicketBorneResponse = await _mediator.Send(TicketBorneQuery);

                if (TicketBorneResponse.IdBorne!=0)
                {
                    var dto = new InfoTicketDTO();
                    dto.MontantPaye = (decimal)AddTicketResponse.Tarif;
                    dto.DateHeureEntree = AddTicketResponse.DateHeureDebutStationnement;
                    if(AddTicketResponse.DateHeureFinStationnement!=null)
                        dto.DateHeureSortie = (DateTime)AddTicketResponse.DateHeureFinStationnement;

                    dto.BorneEntree = TicketBorneResponse.NomBorne;

                    var getAssociatedSessionquery = new RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries.GetOneByTicketLogCaissierAndTicketDates
                    {
                        LogCaissier = AddTicketResponse.LogCaissier,
                        DateStart = AddTicketResponse.DateHeureDebutStationnement,
                        DateEnd = AddTicketResponse.DateHeureFinStationnement
                    };
                    Session? getAssociatedSessionResoponse = await _mediator.Send(getAssociatedSessionquery);

                    var updateSessionEarningsCommand= new RitegeDomain.Database.Commands.Parking.SessionCommands.UpdateSessionEarningsByIdCommand
                    {
                        IdSessions=getAssociatedSessionResoponse.IdSessions,Montant=ticketToAdd.Tarif
                    };
                    int? updateSessionEarningsResoponse = await _mediator.Send(updateSessionEarningsCommand);

                    var getParkingByIdQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries.GetOneByIdParkingQuery
                    {
                        IdParking = TicketBorneResponse.IdParking
                    };
                    Parking? getParkingByIdResponse = await _mediator.Send(getParkingByIdQuery);
                   await mobileClientHandler.SendTicketDataToListeningClients(dto, getParkingByIdResponse.IdSociete, getParkingByIdResponse.IdParking);
                    DashBoardDTO dash = new();
                    dash.RecetteParking = dto.MontantPaye;
                    dash.RecetteCaisse = dto.MontantPaye;
                    dash.RecetteCaissier = dto.MontantPaye;
                    dash.Caisse = getAssociatedSessionResoponse.idCaisse.ToString();
                    dash.NbTickets = 1;

                    await mobileClientHandler.SendDashboardDataToListeningClients(dash, getParkingByIdResponse.IdSociete, getParkingByIdResponse.IdParking,getAssociatedSessionResoponse.idCaisse);
                }
            
                return Ok(AddTicketResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SendAlert")]
        public async Task<ActionResult<string>> SendAlert(ParkingEvent parkingEvent)
        {
            try
            {
            //    await _hubContext.Clients.All.SendAsync("DangerousEventReceived", parkingEvent);

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
        [Route("ResetToken")]
        public async Task<ActionResult> ResetToken()
        {
            try
            {
              
              //      await _hubContext.Clients.All.SendAsync("ResetToken");
                return Ok();
                }

            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

