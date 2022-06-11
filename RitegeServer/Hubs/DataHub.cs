using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
using RitegeServer.Services;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace RitegeServer.Hubs
{
 
    [Authorize]
    public class DataHub : Hub
    {
        IMobileClientHandler mobileClientHandler;
        public DataHub(IMobileClientHandler mobileClientHandler)
        {
            this.mobileClientHandler = mobileClientHandler;
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception?stopCalled)
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;

            mobileClientHandler.RemoveClient(IdSociete, Context.UserIdentifier);
                if (stopCalled is not null)
            Console.WriteLine(String.Format("Client {0} disconnected. exception {1}", Context.ConnectionId,stopCalled.Message));


            return base.OnDisconnectedAsync(stopCalled);
        }
        public override Task OnConnectedAsync()
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;
            var IdClient = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdClient").Value;
            Debug.WriteLine("new user with IdSociete={0}, IdClient{1}",IdSociete,IdClient);
          //  var userid = Context.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            Groups.AddToGroupAsync(Context.ConnectionId, IdSociete);
            mobileClientHandler.AddClient(IdSociete, IdClient, Context.UserIdentifier);
            return base.OnConnectedAsync();
        }
    
   
        public void SetDashboardParking(int idparking)
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;

            mobileClientHandler.SetDashboardParking(IdSociete.Trim(), Context.UserIdentifier, idparking);
        }
        public void SetTicketParking(int idparking)
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;

            mobileClientHandler.SetTicketParking(IdSociete.Trim(), Context.UserIdentifier, idparking);
        }
        public void SetCashRegister(int idCashRegister)
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;

            mobileClientHandler.SetCashRegister(IdSociete.Trim(), Context.UserIdentifier, idCashRegister);
        }
        public void SetIsListeningToEvents(bool IsListening)
        {
            var IdSociete = ((ClaimsIdentity)Context.User.Identity).Claims.First(x => x.Type == "IdSociete").Value;

            mobileClientHandler.SetIsListeningToEvents(IdSociete.Trim(), Context.UserIdentifier, IsListening);
        }
      
    }
}
