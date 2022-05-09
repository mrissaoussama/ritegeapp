using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries.Parking.InfoTicketDTOQueries;

public class InfoTicketDTOQuery : IRequest<IEnumerable<InfoTicketDTO>>
{
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}