using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database.Queries.ParkingDBQueries;
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
        [Route("GetCashierList")]
        public async Task<ActionResult<List<string>>> GetCashierList(string? parkingname)
        { List<string> s = new();
            try
            {if (string.IsNullOrEmpty(parkingname))
                {
                    s.Add("Caissier1");
                    s.Add("Caissier3");
                    s.Add("Caissier4");
                    s.Add("Caissier5");
                    s.Add("Caissier6");
                    s.Add("Caissier7");
                    s.Add("Caissier8");
                    return s;
                }
            if(parkingname == "parking")
                {
                    s.Add("Caissier6");
                    s.Add("Caissier7");
                    s.Add("Caissier8");
                    return s;
                }
            if(parkingname=="parking1")
                {
                    s.Add("Caissier1");
                    s.Add("Caissier3");
                    s.Add("Caissier4");
                    s.Add("Caissier5");
                    return s;

                }
                if (parkingname == "parking3")
                {
                    s.Add("Caissier1");
                    s.Add("Oussama Mrissa");
                    s.Add("Caissier4");
                    s.Add("Caissier5");
                    return s;

                }
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
        [Route("GetCashRegisterList")]
        public async Task<ActionResult<List<string>>> GetCashRegisterList(string? parkingname)
        {
            List<string> s = new();
            try
            {

                if (parkingname == "parking1")
                {
                    s.Add("Caisse_21");
                    s.Add("Caisse_32");
                    s.Add("Caisse_13");
                    return s;

                }
                if (parkingname == "parking3")
                {
                    s.Add("Caisse_1");
                    s.Add("Caisse_2");
                    s.Add("Caisse_3");
                    return s;

                }
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
            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries.GetAllByDateQuery { Date = date, AlertsOnly = false };
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
            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries.GetAllByDateQuery { Date = date.Date, AlertsOnly = true };
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
            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.InfoAbonnementDTOQueries.InfoAbonnementDTOQuery { Name = abonneName, FinishDate = dateEnd, StartDate = dateStart };
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
              
                var query = new RitegeDomain.Database.Queries.ParkingDBQueries.InfoSessionsDTOQueries.InfoSessionsDTOQuery { Name = caissierName, FinishDate = dateEnd, StartDate = dateStart };
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
                var query = new RitegeDomain.Database.Queries.ParkingDBQueries.InfoTicketDTOQueries.InfoTicketDTOQuery { FinishDate = dateEnd, StartDate = dateStart };
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
                    var DashboardDTO = new DashBoardDTO("parking3", "Caisse_1", "oussama mrissa", true, 500, 150, 840, Flux.Entree, Flux.Sortie, 145, 15, 40, 100, 5, 2, 45);

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
                var loginRequest = new RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries.LoginQuery { Login = login, MotDePasse = motdepasse };



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
