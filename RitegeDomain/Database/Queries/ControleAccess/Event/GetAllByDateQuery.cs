using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetAllByDateQuery : IRequest<IEnumerable<Event>>
{
    public DateTime Date { get; set; }
}
public class GetAllByDateAndIdDoorAndEventCodeQuery : IRequest<IEnumerable<Event>>
{
    public DateTime Date { get; set; }
    public int IdCaisse { get; set; }
    public int EventCode { get; set; }
}