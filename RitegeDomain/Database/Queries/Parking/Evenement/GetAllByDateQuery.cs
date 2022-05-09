namespace RitegeDomain.Database.Queries.Parking.EvenementQueries;
public class GetAllByDateQuery : IRequest<IEnumerable<Evenement>>
{
    public DateTime Date { get; set; }
    public bool AlertsOnly { get; set; }

}