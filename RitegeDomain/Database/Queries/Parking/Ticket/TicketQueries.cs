using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Queries.ParkingDBQueries.TicketQueries;

public class GetAllByIdAndDateQuery : IRequest<IEnumerable<Ticket>>
{
    public long? Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
public class GetTodayTicketsQuery : IRequest<int>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }
}
