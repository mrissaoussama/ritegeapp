using AutoMapper;
using MediatR;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries;

public class InfoSessionsDTOQuery : IRequest<IEnumerable<InfoSessionsDTO>>
{
    public int? idCaissier { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}