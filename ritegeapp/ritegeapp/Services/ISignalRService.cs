using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ritegeapp.Services
{
    public interface ISignalRService
    {
        public bool Initialized { get; set; }
        public HubConnection HubConnection { get; set; }
        public HubConnection GetHub();
        Task Connect();
        Task Disconnect();
        Task Initialize();
        void ListenForAlerts();
        void StopListeningForNotImportantData();
    }
}