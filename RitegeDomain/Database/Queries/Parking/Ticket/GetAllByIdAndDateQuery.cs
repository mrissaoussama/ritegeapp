using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.TicketQueries;

public class GetAllByIdAndDateQuery : IRequest<IEnumerable<Ticket>>
{
    public long? Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}