using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.EventtypeQueries;

public class GetOneByIdQuery : IRequest<Eventtype>
{
    public long Id { get; set; }

}