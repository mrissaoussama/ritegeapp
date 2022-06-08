using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByCaisseAndDateQuery : IRequest<IEnumerable<Session>>
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}