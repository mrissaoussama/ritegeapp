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
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;


                while (true)
                {
                    if (DataHub.DashboardDTO is not null)
                    {
                        DataHub.DashboardDTO.RecetteCaisse += 1;
                        DataHub.DashboardDTO.RecetteCaissier += 12;
                        DataHub.DashboardDTO.RecetteParking += 8;
                        DataHub.DashboardDTO.NbAdministrateur += 2;
                        DataHub.DashboardDTO.NbAutorite += 3;
                        DataHub.DashboardDTO.NbEgress += 5;
                        DataHub.DashboardDTO.NbTickets += 1;
                        DataHub.DashboardDTO.NbAbonne += 7;
                        DataHub.DashboardDTO.PlaceDisponible++;
                        DataHub.DashboardDTO.EtatCaisse = true;
                        DataHub.DashboardDTO.PlaceMax = 100;
                        DataHub.DashboardDTO.PlaceDisponible += 10;
                        DataHub.DashboardDTO.EtatCaisse = true;

                        if (DataHub.DashboardDTO.PlaceDisponible > DataHub.DashboardDTO.PlaceMax)
                        {

                            DataHub.DashboardDTO.PlaceDisponible = 0;

                        }


                        await _hubContext.Clients.All.SendAsync("GetDashboardData", DataHub.DashboardDTO);
                    }
                    Thread.Sleep(5000);
                }
            }).Start();
            //return Ok("gfrfdg");
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
