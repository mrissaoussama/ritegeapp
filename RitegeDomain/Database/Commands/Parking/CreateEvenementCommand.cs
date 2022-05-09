namespace RitegeDomain.Database.Commands.Parking.EvenementCommands;
public class CreateEvenementCommand : IRequest<int>
{
    public int ClientId { get; set; }
    public int ParkingId { get; set; }
    public DateTime DateEvent { get; set; } 
    public string DescriptionEvent { get; set; } 
    public string TypeEvent { get; set; }
    public int? idCaisse { get; set; } 
    public int? idBorne { get; set; }
    public string LogCaissier { get; set; }

}