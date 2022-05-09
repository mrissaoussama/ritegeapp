using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

public class GetOneByIdQuery : IRequest<Door>
{
    public long Id { get; set; }

}