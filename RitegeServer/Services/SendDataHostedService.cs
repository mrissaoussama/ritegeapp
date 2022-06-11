using Microsoft.AspNetCore.SignalR;
using RitegeServer.Hubs;

namespace RitegeServer.Services
{
    public class SendDataHostedService : IHostedService, IDisposable
    {
        private readonly IHubContext<DataHub> _hubContext;
        public SendDataHostedService(IHubContext<DataHub> hubContext)
        {
            _hubContext = hubContext;
            SendNewData();
        }
        public void Dispose()
        {
        }

        public void SendNewData()
        {
        
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
