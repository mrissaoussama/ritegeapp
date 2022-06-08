using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.DTO;
using RitegeDomain.Database.Entities.ParkingEntities;


namespace RitegeDomain.Database.Queries.ParkingDBQueries.InfoAbonnementDTOQueries;

public class InfoAbonnementDTOQuery : IRequest<IEnumerable<InfoAbonnementDTO>>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}