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

public class GetOneByTicketLogCaissierAndTicketDates : IRequest<Session>
{
    public string LogCaissier { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
public class GetAllByIdAndDateQuery : IRequest<IEnumerable<Session>>
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
public class GetCurrentSessionQuery : IRequest<Session>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }

}