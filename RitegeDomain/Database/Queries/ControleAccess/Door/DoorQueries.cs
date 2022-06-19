using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

public class GetOneByIdQuery : IRequest<Door>
{
    public long Id { get; set; }

}
public class UpdateDoorStateByIdQuery : IRequest<int>
{
    public long IdDoor { get; set; }
    public bool Activated { get; set; }

}
public class GetAllByIdParkingQuery : IRequest<IEnumerable<Door>>
{
    public int IdParking { get; set; }
}
