namespace RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries; using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByDateQuery : IRequest<IEnumerable<Evenement>>
{
    public DateTime Date { get; set; }
    public bool AlertsOnly { get; set; }

}