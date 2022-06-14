using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RitegeDomain.Database.Queries.ParkingDBQueries;
using RitegeDomain.DTO;
using RitegeDomain.Model;
using RitegeServer.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RitegeServer.Controllers
{
   [Authorize]
    [ApiController]
    [Route("Parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IMediator _mediator; public IConfiguration _configuration;
        public IQueryManager queryManager;
        public ParkingController(IMediator mediator, IConfiguration config, IQueryManager manager)
        {
            queryManager = manager;
            _configuration = config;
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetCashierList")]
        public async Task<ActionResult<Dictionary<int, string>>> GetCashierList()
        {
            try
            {
                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);

                return Ok(await queryManager.GetCashierList(IdSociete));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetCashierListByParking")]
        public async Task<ActionResult<Dictionary<int, string>>> GetCashierListByParking(int idparking)
        {
            try
            {

                return Ok(await queryManager.GetCashierListByParking(idparking));
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
        public async Task<ActionResult<Dictionary<int, string>>> GetCashRegisterList(int idparking)
        {
            try
            {
                return Ok(await queryManager.GetCashRegisterList(idparking));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetEventData")]
        public async Task<ActionResult<List<EventDTO>>> GetEventData(DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);
                return Ok(await queryManager.GetEventData(dateStart, dateEnd, IdSociete));

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
        public async Task<ActionResult<List<EventDTO>>> GetAlertData(DateTime dateStart, DateTime dateEnd)
        {

            try
            {
                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);
                return Ok(await queryManager.GetAlertData(dateStart, dateEnd, IdSociete));

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

            try
            {
                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);

                return Ok(await queryManager.GetAbonnementData(dateStart, dateEnd, abonneName, IdSociete));

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

                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);

                return Ok(await queryManager.GetParkingList(IdSociete));

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
        public async Task<ActionResult<IEnumerable<InfoSessionsDTO>>> GetCashierData(DateTime dateStart, DateTime dateEnd, int? idCaissier)
        {
            try
            {
                var IdSociete = int.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type == "IdSociete").Value);
                return Ok(await queryManager.GetCashierData(dateStart, dateEnd, idCaissier, IdSociete));
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
        public async Task<ActionResult<IEnumerable<InfoTicketDTO>>> GetTicketData(DateTime dateStart, DateTime dateEnd, int idParking)
        {
            try
            {
                return Ok(await queryManager.GetTicketData(dateStart, dateEnd, idParking));

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
        public async Task<ActionResult<DashBoardDTO>> GetDashboardData(int idParking, int idCaisse)
        {

            try
            {
                return Ok(await queryManager.GetDashboardData(idParking, idCaisse));


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
                return Ok(await queryManager.Login(login, motdepasse));
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
        public async Task<ActionResult<string?>> GetDoors(int idParking)
        {
            try
            {
                return Ok(await queryManager.GetDoors(idParking));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
