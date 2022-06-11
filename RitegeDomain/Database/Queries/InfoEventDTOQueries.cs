using AutoMapper;
using MediatR;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries;

public class GetAllByIdSocieteAndDateQuery : IRequest<IEnumerable<EventDTO>>
{
    public int IdSociete { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}
public class GetAlertsByIdSocieteAndDateQuery : IRequest<IEnumerable<EventDTO>>
{
    public int IdSociete { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}