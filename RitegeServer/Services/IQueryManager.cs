using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.DTO;

namespace RitegeServer.Services
{
    public interface IQueryManager
    {
        Task<IEnumerable<InfoAbonnementDTO>> GetAbonnementData(DateTime dateStart, DateTime dateEnd, string? abonneName, int idSociete);
        Task<IEnumerable<EventDTO>> GetAlertData(DateTime dateStart, DateTime dateEnd, int idsociete);
        Task<IEnumerable<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, int? idCaissier, int idSociete);
        Task<Dictionary<int, string>> GetCashierList(int idSociete);
        Task<Dictionary<int, string>> GetCashierListByParking(int idparking);
        Task<Dictionary<int, string>> GetCashRegisterList(int idparking);
        Task<DashBoardDTO> GetDashboardData(int idParking, int idCaisse);
        Task<Dictionary<int, string>> GetDoors(int idParking);
        Task<IEnumerable<EventDTO>> GetEventData(DateTime dateStart, DateTime dateEnd, int idsociete);
        Task<Dictionary<int, string>> GetParkingList(int idSociete);
        Task<IEnumerable<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd, int idParking);
        Task<string?> Login(string login, string motdepasse);
    }
}