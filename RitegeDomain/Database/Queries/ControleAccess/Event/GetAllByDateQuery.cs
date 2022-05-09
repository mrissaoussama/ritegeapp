using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
namespace RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetAllByDateQuery : IRequest<IEnumerable<Event>>
{
    public DateTime Date { get; set; }
}