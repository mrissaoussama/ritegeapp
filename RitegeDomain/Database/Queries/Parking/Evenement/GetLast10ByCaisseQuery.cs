using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetLast10ByCaisseQuery : IRequest<IEnumerable<Evenement>>
{
    public long Id { get; set; }
    public bool AlertsOnly { get; set; }

}