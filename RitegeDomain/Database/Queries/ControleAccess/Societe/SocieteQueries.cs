using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByNameQuery : IRequest<Societe>
{
    public string Name { get; set; }

}

public class GetOneByIdQuery : IRequest<Societe>
{
    public int IdSociete { get; set; }

}