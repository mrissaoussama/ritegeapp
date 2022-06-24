using Microsoft.AspNetCore.SignalR;
using RitegeDomain.DTO;
using System.Diagnostics;

namespace RitegeServer.Hubs
{
    public class WebClient
    {
        public string ConnectionId { get; set; }
        public string IdSociete { get; set; }
        public string IdParking { get; set; }

    }
    public class WebClientHandler : IWebClientHandler
    {
        private IHubContext<WebClientHub> _hubContext;

        public WebClientHandler(IHubContext<WebClientHub> hubContext)
        {
            WebClients = new List<WebClient>();
            _hubContext = hubContext;
        }
        public List<WebClient> WebClients { get; set; }

        public void AddWebClient(string idSociete, string connectionId)
        {
            if (!WebClients.Any(Societe => Societe.IdSociete == idSociete))
                WebClients.Add(new WebClient { ConnectionId = connectionId, IdSociete = idSociete });
        }

        public void RemoveWebClient(string idSociete, string connectionId)
        {
            WebClients.RemoveAll(WebClient => WebClient.ConnectionId == connectionId);
        }


        public async Task ChangeDoorStateForParking(int idSociete, int idDoor,int idController, bool State)
        {
            if (WebClients.Any(webclient => webclient.IdSociete == idSociete.ToString()))
            {

                var webclient = WebClients.Where(webclient => webclient.IdSociete == idSociete.ToString()).Select(webclient => webclient.ConnectionId).ToList();

                await _hubContext.Clients.Users(webclient).SendAsync("ChangeDoorState", idDoor,idController, State);
            }
        }
        public async Task OpenDoorForParking(int idSociete, int idDoor)
        {
            if (WebClients.Any(webclient => webclient.IdSociete == idSociete.ToString()))
            {

                var webclient = WebClients.Where(webclient => webclient.IdSociete == idSociete.ToString()).Select(webclient=>webclient.ConnectionId).ToList();

                await _hubContext.Clients.Users(webclient).SendAsync("OpenDoor", idDoor);
            }
        }
        public async Task CloseDoorForParking(int idSociete, int idDoor)
        {
            if (WebClients.Any(webclient => webclient.IdSociete == idSociete.ToString()))
            {

                var webclient = WebClients.Where(webclient => webclient.IdSociete == idSociete.ToString()).Select(webclient => webclient.ConnectionId).ToList();

                await _hubContext.Clients.Users(webclient).SendAsync("CloseDoor", idDoor);
            }
        }

    }
}
