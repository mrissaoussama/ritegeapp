namespace RitegeDomain.Database.Commands.Parking.TicketCommands; using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateSessionCommand : IRequest<Session>
{
    public int? idCaisse { get; set; } // idCaisse
    public string LogCaissier { get; set; } // logCaissier (length: 100)
    public DateTime? dateStart { get; set; } // dateStart
    public DateTime? dateEnd { get; set; } // dateEnd
    public decimal? Montant { get; set; } // montant


}