using RitegeDomain.DTO;
using RitegeDomain.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ritegeapp.Services
{
    public interface IDataService
    {
        Task<List<InfoAbonnementDTO>> GetAbonnementData(DateTime dateStart, DateTime dateEnd, string abonneName);
        Task<List<EventDTO>> GetAlertData(DateTime dateStart, DateTime dateEnd);
        Task<List<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, int? idCaissier);
        Task ChangeDoorState(int idDoor,int idController,bool IsOpen);

        Task<Dictionary<int, string>> GetCashierList();
        Task<Dictionary<int, string>> GetCashRegisterList(int idParking);
        Task<List<DoorData>> GetDoorList(int idParking);
        Task<DashBoardDTO> GetDashboardData(int idParking, int idCaisse);
        Task<T> GetData<T>(string DataURL, Dictionary<string, string> args);
        Task<List<EventDTO>> GetEventData(DateTime dateStart, DateTime dateEnd);
        HttpClient GetHttpClient();
        Task<List<ParkingEvent>> GetLast10Events();
        Task<Dictionary<int, string>> GetParkingList();
        Task<List<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd, int idParking);
        Task<string> GetToken();
        Task Initialize();
        Task<string> Login(string email,string password);
        Task NewTokenReceived(string token);
        Task Disconnect();
    }
}