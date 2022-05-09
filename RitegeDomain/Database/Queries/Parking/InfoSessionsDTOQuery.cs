using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries.Parking.InfoSessionsDTOQueries;

public class InfoSessionsDTOQuery : IRequest<IEnumerable<InfoSessionsDTO>>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}