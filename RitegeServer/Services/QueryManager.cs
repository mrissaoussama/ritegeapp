using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Queries.ParkingDBQueries;
using RitegeDomain.DTO;
using RitegeDomain.Model;
using RitegeServer.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace RitegeServer.Services
{
    public class QueryManager : IQueryManager
    {
        private readonly IMediator _mediator; public IConfiguration _configuration;

        public QueryManager(IMediator mediator, IConfiguration config)
        {
            _configuration = config;
            _mediator = mediator;
        }
        public async Task<Dictionary<int, string>> GetCashierList(int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries.GetAllByIdSocieteQuery { IdSociete = idSociete };

            var response = await _mediator.Send(query);
            Dictionary<int, string> cashierLoginList = new();
            foreach (var item in response)
            {
                cashierLoginList.Add(item.IdUtilisateur, item.Login);
            }
            return (cashierLoginList);
        }
        public async Task<Dictionary<int, string>> GetCashierListByParking(int idparking)
        {

            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries.GetAllByIdParkingQuery { IdParking = idparking };

            var response = await _mediator.Send(query);
            Dictionary<int, string> cashierLoginList = new();
            foreach (var item in response)
            {
                cashierLoginList.Add(item.IdUtilisateur, item.Login);
            }
            return (cashierLoginList);
        }
        public async Task<Dictionary<int, string>> GetCashRegisterList(int idparking)
        {

            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries.GetAllByIdParkingQuery { Id = idparking };

            var response = await _mediator.Send(query);
            Dictionary<int, string> cashRegisterList = new();
            foreach (var item in response)
            {
                cashRegisterList.Add(item.IdCaisse, item.NomCaisse);
            }
            return (cashRegisterList);
        }
        public async Task<Dictionary<int, string>> GetParkingList(int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries.GetAllByIdSociete { IdSociete = (idSociete) };
            var response = await _mediator.Send(query);
            Dictionary<int, string> ParkingList = new();
            foreach (var item in response)
            {
                ParkingList.Add(item.IdParking, item.NomParking);
            }
            return (ParkingList);
        }
        public async Task<IEnumerable<EventDTO>> GetEventData(DateTime dateStart, DateTime dateEnd,int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.GetAllByIdSocieteAndDateQuery { IdSociete = idSociete, DateStart = dateStart, DateEnd = dateEnd };

            var response = await _mediator.Send(query);
            return (response);
        }
        public async Task<IEnumerable<EventDTO>> GetAlertData(DateTime dateStart, DateTime dateEnd, int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.GetAlertsByIdSocieteAndDateQuery { IdSociete = idSociete, DateStart = dateStart, DateEnd = dateEnd };


            var response = await _mediator.Send(query);
            return (response);
        }
        public async Task<IEnumerable<InfoAbonnementDTO>> GetAbonnementData(DateTime dateStart, DateTime dateEnd, string? abonneName,int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.InfoAbonnementDTOQuery { Name = abonneName, FinishDate = dateEnd, StartDate = dateStart };
            var response = await _mediator.Send(query);
            return (response);
        }

        public async Task<IEnumerable<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, int? idCaissier, int idSociete)
        {

            var query = new RitegeDomain.Database.Queries.InfoSessionsDTOQuery { idCaissier = idCaissier, FinishDate = dateEnd, StartDate = dateStart };
            var response = await _mediator.Send(query);

            return (response);
        }
        public async Task<IEnumerable<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd, int idParking)
        {

            var query = new RitegeDomain.Database.Queries.InfoTicketDTOQuery { FinishDate = dateEnd, StartDate = dateStart, IdParking = idParking };
            var response = await _mediator.Send(query);
            return (response);
        }
        public async Task<DashBoardDTO> GetDashboardData(int idParking, int idCaisse)
        {
            DashBoardDTO dashBoardDTO = new DashBoardDTO();

            var MontantQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries.GetCurrentSessionQuery { IdParking = idParking, IdCaisse = idCaisse };
            var MontantResponse = await _mediator.Send(MontantQuery);
            if(MontantResponse.Montant is not null)
            dashBoardDTO.RecetteCaissier = (decimal)MontantResponse.Montant;
            if (MontantResponse.idCaisse is not null)
            {
                var CaissierQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries.GetOneByLoginQuery { Login = MontantResponse.LogCaissier };
                var CaissierResponse = await _mediator.Send(CaissierQuery);
                dashBoardDTO.NomPrenomCaissier = CaissierResponse.Prenom + " " + CaissierResponse.Nom;
            }

            var recetteParkingQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries.GetParkingEarningsByIdQuery { IdParking = idParking };
            var recetteParkingResponse= await _mediator.Send(recetteParkingQuery);
            dashBoardDTO.RecetteParking = recetteParkingResponse;

            var recettedayQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries.GetTodayEarningsByIdQuery { IdCaisse = idCaisse };
            var recettedayResponse = await _mediator.Send(recettedayQuery);
            dashBoardDTO.RecetteCaisse = recettedayResponse;

            var nbticketQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.TicketQueries.GetTodayTicketsQuery
            {
                IdParking = idParking,
                IdCaisse = idCaisse
            };
            var nbticketResponse = await _mediator.Send(nbticketQuery);
            dashBoardDTO.NbTickets = nbticketResponse;

            var nbAbonneQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries.GetTodayAbonneQuery
            {
                IdParking = idParking,
                IdCaisse = idCaisse
            };
            var nbAbonneResponse = await _mediator.Send(nbAbonneQuery);
            dashBoardDTO.NbAbonne = nbAbonneResponse;

            var nbAutoriteQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries.GetTodayAutoriteQuery
            {
                IdParking = idParking,
                IdCaisse = idCaisse
            };
            var nbAutoriteResponse = await _mediator.Send(nbAutoriteQuery);
            dashBoardDTO.NbAutorite = nbAutoriteResponse;

            var nbAdministrateurQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries.GetTodayAdministrateurQuery
            {
                IdParking = idParking,
                IdCaisse = idCaisse
            };
            var nbAdministrateurResponse = await _mediator.Send(nbAdministrateurQuery);
            dashBoardDTO.NbAdministrateur = nbAdministrateurResponse;

            var parkingQuery = new RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries.GetOneByIdParkingQuery
            {
                IdParking = idParking,
            };
            var parkingResponse = await _mediator.Send(parkingQuery);

            var EgressQuery= new RitegeDomain.Database.Queries.ControleAccess.EventQueries.GetAllByDateAndIdDoorAndEventCodeQuery            {
                
            };

            dashBoardDTO.PlaceMax = parkingResponse.CapaciteParking;
            dashBoardDTO.PlaceDisponible = parkingResponse.CapaciteParking-parkingResponse.PlacesOccupees;
            dashBoardDTO.Caisse = idCaisse.ToString();
            return (dashBoardDTO);
        }
        public async Task<string?> Login(string login, string motdepasse)
        {

            var loginRequest = new RitegeDomain.Database.Queries.ControleAccess.ClientQueries.GetOneByEmailAndPasswordQuery { Email = login, Password = motdepasse };
            var response = await _mediator.Send(loginRequest);
            if (response is not null && response.IdClient != 0)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("IdClient", response.IdClient.ToString()),
                        new Claim("Email", response.Email),
                        new Claim("IdSociete", response.IdSociete.ToString()),
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                    signingCredentials: signIn);
                System.Diagnostics.Debug.WriteLine("User Logged in. token: " + DateTime.Now.AddHours(int.Parse(_configuration["Jwt:Expires"])) + token);

                return (new JwtSecurityTokenHandler().WriteToken(token));
            }
            return null;
        }
        public async Task<List<DoorData>> GetDoors(int idParking)
        {

            var query = new RitegeDomain.Database.Queries.ControleAccess.DoorQueries.GetAllByIdParkingQuery { IdParking = idParking };
            var response = await _mediator.Send(query);
            List<DoorData> doorList = new();
            foreach(var item in response)
            {
                doorList.Add(new DoorData { DoorName=item.DoorName,DoorState= (bool)item.Activated,idDoor=item.IdDoor });
            }
            //Dictionary<int, string> doorList = new();
            //foreach (var item in response)
            //{
            //    cashRegisterList.Add(item.IdCaisse, item.NomCaisse);
            //}
            //return (cashRegisterList);
            return (doorList);
        }
    }
}
