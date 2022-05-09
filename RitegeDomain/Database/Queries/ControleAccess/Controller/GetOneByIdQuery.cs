global using AutoMapper;
global using MediatR;
global using RitegeDomain.Database.Entities.ControleAccess;
global using System.Threading;
namespace RitegeDomain.Database.Queries.ControleAccess.ControllerQueries;

public class GetOneByIdQuery : IRequest<RitegeDomain.Database.Entities.ControleAccess.Controller>
{
    public long Id { get; set; }

}