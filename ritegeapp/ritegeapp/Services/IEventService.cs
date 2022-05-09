using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ritegeapp.Services
{
    public interface IEventService
    {
        bool IsStarted { get; set; }
        void StartService();

        void StopService();
        public HubConnection GetHub();
        public  void SetHub(HubConnection hub);

    }
}
