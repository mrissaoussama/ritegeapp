using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ControleAccess.ClientQueries;

public class GetOneByEmailAndPasswordQuery : IRequest<Client>
{
    public string Email { get; set; }
    public string Password { get; set; }

}

public class GetOneByIdQuery : IRequest<Client>
{
    public int IdClient { get; set; }

}
public class GetOneByNameQuery : IRequest<Client>
{
    public string NameClient { get; set; }

}