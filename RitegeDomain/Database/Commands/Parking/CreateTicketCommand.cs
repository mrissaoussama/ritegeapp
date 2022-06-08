namespace RitegeDomain.Database.Commands.Parking.TicketCommands; using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateTicketCommand : IRequest<Ticket>
{
    public DateTime DateHeureDebutStationnement { get; set; } // dateHeureDebutStationnement
    public DateTime? DateHeureFinStationnement { get; set; } // dateHeureFinStationnement
    public string EtatTicket { get; set; } // etatTicket (length: 100)
    public int? IdTarifTicket { get; set; } // idTarifTicket
    public decimal? Tarif { get; set; } // Tarif
    public int idBorneEntree { get; set; } // idBorneEntree
    public int? idBorneSortie { get; set; } // idBorneSortie
    public string LogCaissier { get; set; } // LogCaissier (length: 100)
    public bool? AvecTarif2 { get; set; } // avecTarif2


}