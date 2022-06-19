using RitegeDomain.DTO;

namespace RitegeServer.Hubs
{
    public interface IMobileClientHandler
    {
        Dictionary<string, List<MobileClient>> MobileClients { get; set; }

        void AddClient(string idSociete, string idClient, string connectionId);
        void RemoveClient(string idSociete, string connectionId);
        Task SendTicketDataToListeningClients(InfoTicketDTO ticket, int idSociete, int idParking);
        Task SendDashboardDataToListeningClients(DashBoardDTO dashBoardDTO, int idSociete, int idParking,int? idCashRegister);
        Task SendEventToListeningClients(EventDTO dto, int idSociete);
        Task SendDoorStateChangeToListeningClients(DoorData doorData, int idSociete,int idParking);

        void SetCashRegister(string idSociete, string connectionId, int idCashRegister);
        void SetDashboardParking(string idSociete, string connectionId, int idparking);
        void SetDoorParking(string idSociete, string connectionId, int idparking);
        void SetIsListeningToEvents(string idSociete, string connectionId, bool IsListening);
        Task SendAlertToClients(EventDTO dto, int idSociete);
    }
}