using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetLast10ByCaisseAndBorneQuery : IRequest<IEnumerable<Evenement>>
{
    public long idCaisse { get; set; }
    public long idBorne { get; set; }
    public bool AlertsOnly { get; set; }

}