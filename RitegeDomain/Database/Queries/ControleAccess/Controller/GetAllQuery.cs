using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Queries.ControleAccess.ControllerQueries;

public class GetAllQuery : IRequest<IEnumerable<Controller>>
{
}
