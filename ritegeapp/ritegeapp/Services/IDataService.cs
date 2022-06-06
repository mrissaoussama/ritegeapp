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
        Task<List<ParkingEvent>> GetAlertData(DateTime date);
        Task<List<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, string caissierName);
        Task<DashBoardDTO> GetDashboardData(int idparking);
        Task<T> GetData<T>(string DataURL, Dictionary<string, string> args);
        Task<List<ParkingEvent>> GetEventData(DateTime date);
        HttpClient GetHttpClient();
        Task<List<ParkingEvent>> GetLast10Events();
        Task<List<string>> GetParkingList();
        Task<List<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd);
        Task<string> GetToken();
        Task Initialize();
        Task<string> Login();
        Task NewTokenReceived(string token);
    }
}