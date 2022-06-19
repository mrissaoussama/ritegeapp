using Microsoft.AspNetCore.SignalR;
using RitegeDomain.DTO;
using System.Diagnostics;

namespace RitegeServer.Hubs
{
    public class MobileClient
    {
        public string ConnectionId { get; set; }
        public string IdClient { get; set; }
        public int CurrentDashboardParkingId { get; set; }
        public int CurrentDoorParkingId { get; set; }
        public int CurrentTicketParkingId { get; set; }
        public bool IsListeningToEvents { get; set; }
        public int CurrentCashRegisterId { get; set; }
    }
    public class MobileClientHandler : IMobileClientHandler
    {
        private IHubContext<DataHub> _hubContext;

        public MobileClientHandler(IHubContext<DataHub> hubContext)
        {
            MobileClients = new Dictionary<string, List<MobileClient>>();
            _hubContext = hubContext;
        }
        public Dictionary<string, List<MobileClient>> MobileClients { get; set; }

        public void AddClient(string idSociete, string idClient, string connectionId)
        {
            if (MobileClients.Keys.Contains(idSociete))
            {
                MobileClients[idSociete].Add(
                    new MobileClient { ConnectionId = connectionId, IdClient = idClient });
            }
            else
            {
                MobileClients.Add(idSociete, new List<MobileClient> { new MobileClient { ConnectionId = connectionId, IdClient = idClient } });
            }
        }

        public void RemoveClient(string idSociete, string connectionId)
        {
            MobileClients[idSociete].RemoveAll(mobileClient => mobileClient.ConnectionId == connectionId);
        }

        public async Task SendTicketDataToListeningClients(InfoTicketDTO ticket, int idSociete, int idParking)
        {
            if (MobileClients.ContainsKey(idSociete.ToString()))
            {
                var listeningTicketClients = MobileClients[idSociete.ToString()]
                    .Where(client => client.CurrentTicketParkingId == idParking||client.CurrentTicketParkingId==0).
                    Select(mobileclient => mobileclient.ConnectionId).Distinct().ToList();

                await _hubContext.Clients.Users(listeningTicketClients).SendAsync("GetTicketData", ticket);
            }
        }
        public async Task SendDashboardDataToListeningClients(DashBoardDTO dashBoardDTO, int idSociete, int idParking,int? idCashRegister)
        {
            if (MobileClients.ContainsKey(idSociete.ToString()))
            {
   
                var listeningDashboardClients = MobileClients[idSociete.ToString()].
                    Where(client => client.CurrentDashboardParkingId == idParking)
                    .Select(mobileclient => mobileclient.ConnectionId).Distinct().ToList();
                await _hubContext.Clients.Users(listeningDashboardClients).SendAsync("GetDashboardData", dashBoardDTO);
            }
        }
        public async Task SendAlertToClients(EventDTO dto, int idSociete)
        {
            if (MobileClients.ContainsKey(idSociete.ToString()))
            {

                var listeningDashboardClients = MobileClients[idSociete.ToString()].Select(client=>client.ConnectionId).ToList();

                 await _hubContext.Clients.Users(listeningDashboardClients).SendAsync("GetAlertData", dto);
            }
        }
        public async Task SendEventToListeningClients(EventDTO dto, int idSociete)
        {
            if (MobileClients.ContainsKey(idSociete.ToString()))
            {

                var listeningDashboardClients = MobileClients[idSociete.ToString()].
                    Where(client => client.IsListeningToEvents == true)
                    .Select(mobileclient => mobileclient.ConnectionId).Distinct().ToList();
                await _hubContext.Clients.Users(listeningDashboardClients).SendAsync("GetEventData", dto);
            }
        }

        
        public void SetDashboardParking(string idSociete, string connectionId, int idparking)
        {
            Debug.WriteLine("parking changed to "+idparking);
            if (MobileClients.Keys.Contains(idSociete))
            {
                foreach (var mobileClient in MobileClients[idSociete].Where(client => client.ConnectionId == connectionId))
                {
                    mobileClient.CurrentDashboardParkingId = idparking;
                }
            }

        }
        public void SetDoorParking(string idSociete, string connectionId, int idparking)
        {
            Debug.WriteLine("door changed to " + idparking);

            if (MobileClients.Keys.Contains(idSociete))
            {
                foreach (var mobileClient in MobileClients[idSociete].Where(client => client.ConnectionId == connectionId))
                {
                    mobileClient.CurrentDoorParkingId = idparking;
                }
            }
        }
        public void SetCashRegister(string idSociete, string connectionId, int idCashRegister)
        {
            Debug.WriteLine("CashRegister changed to " + idCashRegister);

            if (MobileClients.Keys.Contains(idSociete))
            {
                foreach (var mobileClient in MobileClients[idSociete].Where(client => client.ConnectionId == connectionId))
                {
                    mobileClient.CurrentCashRegisterId = idCashRegister;
                }
            }
        }

        public void SetIsListeningToEvents(string idSociete, string connectionId, bool IsListening)
        {

            Debug.WriteLine("IsListening changed to " + IsListening);

            if (MobileClients.Keys.Contains(idSociete))
            {
                foreach (var mobileClient in MobileClients[idSociete].Where(client => client.ConnectionId == connectionId))
                {
                    mobileClient.IsListeningToEvents = IsListening;
                }
            }
        }

        public async Task SendDoorStateChangeToListeningClients(DoorData doorData, int idSociete,int idParking)
        {
            if (MobileClients.ContainsKey(idSociete.ToString()))
            {
                var listeningDashboardClients = MobileClients[idSociete.ToString()].
                    Where(client => client.CurrentDoorParkingId == idParking)
                    .Select(mobileclient => mobileclient.ConnectionId).Distinct().ToList();
                await _hubContext.Clients.Users(listeningDashboardClients).SendAsync("DoorStateChanged", doorData);
            }
        }
    }
}
