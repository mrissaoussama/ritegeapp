namespace RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries; using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByDateQuery : IRequest<IEnumerable<Evenement>>
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public bool AlertsOnly { get; set; }

}