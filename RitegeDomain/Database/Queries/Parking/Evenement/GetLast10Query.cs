using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetLast10Query : IRequest<IEnumerable<Evenement>>
{
    public bool AlertsOnly { get; set; }

}