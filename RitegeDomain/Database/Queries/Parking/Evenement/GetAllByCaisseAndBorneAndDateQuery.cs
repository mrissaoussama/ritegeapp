using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByCaisseAndBorneAndDateQuery : IRequest<IEnumerable<Evenement>>
{
    public long idCaisse { get; set; }
    public long idBorne { get; set; }
    public DateTime Date { get; set; }
    public bool AlertsOnly { get; set; }

}