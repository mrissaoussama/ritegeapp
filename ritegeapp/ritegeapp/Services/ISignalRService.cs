using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ritegeapp.Services
{
    public interface ISignalRService
    {
        HubConnection HubConnection { get; set; }
        bool Initialized { get; set; }
        bool isListeningToDoors { get; }

        Task Connect();
        Task Disconnect();
        HubConnection GetHub();
        Task Initialize();
        Task ListenForAlerts();
        Task ListenForDashboardData(int? idparking, int? idcaisse);
        Task ListenForDoorData(int idParking);
        Task ListenForEventData();
        Task ListenForTicketData(int idParking);
        void StartAlertService();
        Task StopListeningForDashboardData();
        Task StopListeningForDoorData();
        Task StopListeningForEventData();
        Task StopListeningForTicketData();
    }
}