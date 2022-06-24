namespace RitegeServer.Hubs
{
    public interface IWebClientHandler
    {
        List<WebClient> WebClients { get; set; }

        void AddWebClient(string idSociete, string connectionId);
        Task ChangeDoorStateForParking(int idSociete, int idDoor,int idController, bool State);
        Task CloseDoorForParking(int idSociete, int idDoor);
        Task OpenDoorForParking(int idSociete, int idDoor);
        void RemoveWebClient(string idSociete, string connectionId);
    }
}