using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database.Queries.Parking;
using RitegeDomain.DTO;
using RitegeDomain.Model;
using RitegeServer.Services;

namespace RitegeServer.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("Parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ParkingController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetCashRegisterList")]
        public async Task<ActionResult<List<string>>> GetCashRegisterList()
        {
            try
            {
                List<string> s = new();
                s.Add("parking2");
                s.Add("parking3");
                s.Add("parking4");
                s.Add("parking5");
                s.Add("parking6");
                s.Add("parking7");
                s.Add("parking8");
                return s;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Route("GetLast10Events")]
        //public async Task<ActionResult<List<ParkingEvent>>> GetLast10Events()
        //{
        //    var query = new RitegeDomain.Database.Queries.Parking.EvenementQueries.GetLast10Query { };
        //    try
        //    {
        //        var response = await _mediator.Send(query);

        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetEventData")]
        public async Task<ActionResult<List<ParkingEvent>>> GetEventData(DateTime date)
        {
            var query = new RitegeDomain.Database.Queries.Parking.EvenementQueries.GetAllByDateQuery { Date = date, AlertsOnly = false };
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
        [Route("GetAlertData")]
        public async Task<ActionResult<List<ParkingEvent>>> GetAlertData(DateTime date)
        {
            var query = new RitegeDomain.Database.Queries.Parking.EvenementQueries.GetAllByDateQuery { Date = date.Date, AlertsOnly = true };
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
        [Route("GetAbonnementData")]
        public async Task<ActionResult<IEnumerable<InfoAbonnementDTO>>> GetAbonnementData(DateTime dateStart, DateTime dateEnd, string? abonneName)
        {
            //IRequest<IEnumerable<Affectationabonnement>> Affectationabonnementquery;

            //    if (!string.IsNullOrEmpty(abonneName))
            //        Affectationabonnementquery = new RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries.GetAllByIdAndDatesQuery { StartDate = dateStart, FinishDate = dateEnd };
            //    else
            //    {
            //        Affectationabonnementquery = new RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries.GetAllByNameAndDatesQuery { StartDate = dateStart, FinishDate = dateEnd,Name=abonneName };
            //    }
            var query = new RitegeDomain.Database.Queries.Parking.InfoAbonnementDTOQueries.InfoAbonnementDTOQuery { Name = abonneName, FinishDate = dateEnd, StartDate = dateStart };
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
        [Route("GetParkingList")]
        public async Task<ActionResult<List<string>>> GetParkingList()
        {
            try
            {
                List<string> s = new();
                s.Add("parking2");
                s.Add("parking3");
                s.Add("parking4");
                s.Add("parking5");
                s.Add("parking6");
                s.Add("parking7");
                s.Add("parking8");
                return s;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [Route("GetCashierData")]
            public async Task<ActionResult<IEnumerable<InfoSessionsDTO>>> GetCashierData(DateTime dateStart, DateTime dateEnd, string? caissierName)
            {
                try
                {
                //var ListDto = new List<InfoSessionsDTO>();
                //#region session
                //var data = new InfoSessionsDTO("Oussama Mrissa", "Caisse 1", DateTime.Today, DateTime.Today.AddHours(1));
                //var data5 = new InfoSessionsDTO("oussama41", "caissier", DateTime.Today, DateTime.Today.AddDays(1));
                //var data3 = new InfoSessionsDTO("oussama22", "caissier", DateTime.Today.AddDays(1), DateTime.Today.AddDays(1));
                //var data4 = new InfoSessionsDTO("oussama43", "caissier", DateTime.Today.AddDays(2), DateTime.Today.AddDays(2));
                //var data1 = new InfoSessionsDTO("oussama41", "caissieré", DateTime.Today, DateTime.Today);
                //var data2 = new InfoSessionsDTO("oussama52", "caissieré", DateTime.Today.AddMonths(-3), DateTime.Today.AddMonths(-2));
                //data.NbAbonne = 1111;
                //data.NbAdministratif = 411;
                //data.NbAutorite = 415;
                //data.NbTickets = 17;
                //data.Recette = 154;

                //data5.NbAbonne = 111;
                //data5.NbAdministratif = 411;
                //data5.NbAutorite = 415;
                //data5.NbTickets = 17;
                //data5.Recette = 5133;

                //data3.NbAbonne = 111;
                //data3.NbAdministratif = 411;
                //data3.NbAutorite = 415;
                //data3.NbTickets = 71;
                //data3.Recette = 5214;

                //data4.NbAbonne = 111;
                //data4.NbAdministratif = 141;
                //data4.NbAutorite = 4151;
                //data4.NbTickets = 71;
                //data4.Recette = 5114;

                //data2.NbAbonne = 111;
                //data2.NbAdministratif = 411;
                //data2.NbAutorite = 415;
                //data2.NbTickets = 71;
                //data2.Recette = 5104;

                //data1.NbAbonne = 111;
                //data1.NbAdministratif = 141;
                //data1.NbAutorite = 451;
                //data1.NbTickets = 71;
                //data1.Recette = 155;
                //ListDto.Add(data);
                //ListDto.Add(data4);
                //ListDto.Add(data3);
                //ListDto.Add(data2);
                //ListDto.Add(data5);
                //ListDto.Add(data1);
                //#endregion
                //var dataFilter = new DataFilter();
                //var list = dataFilter.FilterSessionDTO(ListDto, dateStart, dateEnd, caissierName);
                //return Ok(list);
                var query = new RitegeDomain.Database.Queries.Parking.InfoSessionsDTOQueries.InfoSessionsDTOQuery { Name = caissierName, FinishDate = dateEnd, StartDate = dateStart };
                var response = await _mediator.Send(query);
                int caisser = 1;
                var rand = new Random();
                foreach (var item in response)
                {
                    item.Caissier = "Caissier" + caisser;
                    item.NbAutorite = rand.Next(0, 2);
                    item.NbAdministratif = rand.Next(1, 4);
                    item.NbAbonne = rand.Next(5, 12);
                    item.NbTickets = rand.Next(5, 12) + item.NbAutorite + item.NbAdministratif + item.NbAbonne;
                    caisser++;
                }
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
            [Route("GetTicketData")]
            public async Task<ActionResult<IEnumerable<InfoTicketDTO>>> GetTicketData(DateTime dateStart, DateTime dateEnd)
            {
              
                //var ListDtoTicket = new List<InfoTicketDTO>();
                //var data = new InfoTicketDTO("10410110810811110", "Borne1", 20, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                //var data1 = new InfoTicketDTO("119111114108100", "Borne1", 45, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                //var data2 = new InfoTicketDTO("11510199114101116", "Borne2", 15, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                //ListDtoTicket.Add(data);
                //ListDtoTicket.Add(data2);
                //ListDtoTicket.Add(data1);
                //var dataFilter = new DataFilter();
                //var list = dataFilter.FilterTicketDTO(ListDtoTicket, dateStart, dateEnd);
                //return Ok(list);
                var query = new RitegeDomain.Database.Queries.Parking.InfoTicketDTOQueries.InfoTicketDTOQuery { FinishDate = dateEnd, StartDate = dateStart };
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
            [Route("GetDashboardData")]
            public async Task<ActionResult<DashBoardDTO>> GetDashboardData(int idParking)
            {

                try
                {
                    var DashboardDTO = new DashBoardDTO("parking3", "Caisse1", "oussama mrissa", true, 15489, 8754, 4871, Flux.Entree, Flux.Sortie, 145, 123, 24, 100, 12, 54, 45);

                    return Ok(DashboardDTO);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Login")]
        public async Task<ActionResult<string?>> Login(string login, string motdepasse)
        {
            try
            {
                var loginRequest = new RitegeDomain.Database.Queries.Parking.UtilisateurQueries.LoginQuery { Login = login, MotDePasse = motdepasse };



                var response = await _mediator.Send(loginRequest);
                System.Diagnostics.Debug.WriteLine("User Logged in. token: " + response);
                if (response is not null)
                return Ok(response);
                return BadRequest("Wrong Info");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetDoors")]
        public async Task<ActionResult<string?>> GetDoors()
        {
            try
            {
                return Ok(null);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    }
