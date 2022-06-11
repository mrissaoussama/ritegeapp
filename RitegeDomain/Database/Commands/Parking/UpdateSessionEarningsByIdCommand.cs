namespace RitegeDomain.Database.Commands.Parking.SessionCommands; using RitegeDomain.Database.Entities.ParkingEntities;

public class UpdateSessionEarningsByIdCommand : IRequest<int>
{
    public int IdSessions { get; set; } // idCaisse

    public decimal? Montant { get; set; } // montant


}