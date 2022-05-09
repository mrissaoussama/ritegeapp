using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetAllByCaisseAndDateQuery : IRequest<IEnumerable<Evenement>>
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public bool AlertsOnly { get; set; }

}