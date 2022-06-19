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
 
    public class WebClientHub : Hub
    {
        IWebClientHandler webClientHandler;
        public WebClientHub(IWebClientHandler webClientHandler)
        {
            this.webClientHandler = webClientHandler;
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception?stopCalled)
        {
            var IdSociete = Context.GetHttpContext().Request.Query["idSociete"];

            webClientHandler.RemoveWebClient(IdSociete, Context.UserIdentifier);
                if (stopCalled is not null)
            Console.WriteLine(String.Format("WebClient {0} disconnected. exception {1}", Context.ConnectionId,stopCalled.Message));


            return base.OnDisconnectedAsync(stopCalled);
        }
        public override Task OnConnectedAsync()
        {
            var IdSociete = Context.GetHttpContext().Request.Query["idSociete"];
            Debug.WriteLine("new webclient with IdSociete= "+IdSociete);
            Groups.AddToGroupAsync(Context.ConnectionId, IdSociete);
            webClientHandler.AddWebClient(IdSociete, Context.UserIdentifier);
            return base.OnConnectedAsync();
        }
    
   
      
      
    }
}
