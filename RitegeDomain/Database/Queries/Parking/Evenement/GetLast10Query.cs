using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries; using RitegeDomain.Database.Entities.ParkingEntities;


public class GetLast10Query : IRequest<IEnumerable<Evenement>>
{
    public bool AlertsOnly { get; set; }

}