using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
    using RitegeDomain.DTO;
using RitegeServer.Services;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace RitegeServer.Hubs
{
    public static class UserAndGroupHandler
    {
        public static Dictionary<string, List<string>> ConnectedIds = new Dictionary<string, List<string>>();
    }
    [Authorize]
    public class DataHub : Hub
    {
        //     public static IHubContext<DataHub> GlobalContext { get; private set; }


        public static List<InfoTicketDTO> ListDtoTicket { get; set; }
        public static DashBoardDTO DashboardDTO { get; set; }
        DataFilter dataFilter;

        public DataHub(

            //IHubContext<DataHub> ctx
            )
        {
            dataFilter = new DataFilter();
            setupticketdata();
     
            if (DashboardDTO == null)
            {
                DashboardDTO = new DashBoardDTO("parking", "Caisse1", "oussama mrissa", true, 15489, 8754, 4871, Flux.Entree, Flux.Sortie, 145, 123, 24, 100, 12, 54, 45);
            }
        }
        private void setupticketdata()
        {
            if (ListDtoTicket is null || ListDtoTicket.Count == 0)
            {
                ListDtoTicket = new List<InfoTicketDTO>();
                var data = new InfoTicketDTO("10410110810811110", "Borne1", 20, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                var data1 = new InfoTicketDTO("119111114108100", "Borne1", 45, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                var data2 = new InfoTicketDTO("11510199114101116", "Borne2", 15, DateTime.Today, DateTime.Today, TypeTicket.TicketStationnement);
                ListDtoTicket.Add(data);
                ListDtoTicket.Add(data2);
                ListDtoTicket.Add(data1);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception?stopCalled)
        {
            var groupid = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdUtilisateur").Value;
            UserAndGroupHandler.ConnectedIds[groupid].Remove(Context.ConnectionId);
                if (stopCalled is not null)
            Console.WriteLine(String.Format("Client {0} disconnected. exception {1}", Context.ConnectionId,stopCalled.Message));


            return base.OnDisconnectedAsync(stopCalled);
        }
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("new user");
            var groupid= ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdUtilisateur").Value;
            Groups.AddToGroupAsync(Context.ConnectionId, groupid);
            if (UserAndGroupHandler.ConnectedIds.Keys.Contains(groupid))
            { 
                UserAndGroupHandler.ConnectedIds[groupid].Add(Context.ConnectionId);
            }
            else
            {
                UserAndGroupHandler.ConnectedIds.Add(groupid, new List<string> { Context.ConnectionId });
            }
            return base.OnConnectedAsync();
        }
        public async Task GetTicketData(DateTime dateStart, DateTime dateEnd)
        {
            var filteredlist = dataFilter.FilterTicketDTO(ListDtoTicket, dateStart, dateEnd);
            await Clients.Caller.SendAsync("GetTicketData", filteredlist);
        }
       
        public async Task DangerousEventReceived(ParkingEvent parkingEvent)
        {
            await Clients.Group(((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdUtilisateur").Value).SendAsync("DangerousEventReceived", parkingEvent);

        }
    }
}
