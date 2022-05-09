using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.UsersystemQueries;

public class GetAllQuery : IRequest<IEnumerable<Usersystem>>
{
}