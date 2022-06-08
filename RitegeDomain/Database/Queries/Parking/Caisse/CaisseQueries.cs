using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries; using RitegeDomain.Database.Entities.ParkingEntities;


public class GetByNameQuery : IRequest<Caisse>
{
    public string Name { get; set; }

}
public class GetAllByIdParkingQuery : IRequest<IEnumerable<Caisse>>
{
    public int Id { get; set; }

}
public class GetOneByIdQuery : IRequest<Caisse>
{
    public int Id { get; set; }

}