using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.UsersystemQueries;

public class GetOneByIdQuery : IRequest<Usersystem>
{
    public long Id { get; set; }

}