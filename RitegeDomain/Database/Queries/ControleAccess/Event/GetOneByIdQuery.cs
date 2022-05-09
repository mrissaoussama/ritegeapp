using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetOneByIdQuery : IRequest<Event>
{
    public long Id { get; set; }

}