using AutoMapper;
using MediatR;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries;

public class InfoTicketDTOQuery : IRequest<IEnumerable<InfoTicketDTO>>
{
    public int IdParking { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}