using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.BorneQueries; using RitegeDomain.Database.Entities.ParkingEntities;


public class GetOneByNameQuery : IRequest<Borne>
{
    public string Name { get; set; }

}
public class GetAllByIdParkingQuery : IRequest<IEnumerable<Borne>>
{
    public int Id { get; set; }

}
public class GetOneByIdQuery : IRequest<Borne>
{
    public int Id { get; set; }

}