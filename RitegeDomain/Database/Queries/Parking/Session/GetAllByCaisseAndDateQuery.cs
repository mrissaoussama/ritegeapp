using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.SessionQueries;

public class GetAllByCaisseAndDateQuery : IRequest<IEnumerable<Session>>
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}