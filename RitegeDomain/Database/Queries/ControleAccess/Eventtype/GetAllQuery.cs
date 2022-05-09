using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.EventtypeQueries;

public class GetAllQuery : IRequest<IEnumerable<Eventtype>>
{
}