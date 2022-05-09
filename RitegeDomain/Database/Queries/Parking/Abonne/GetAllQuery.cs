using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AbonneQueries;

public class GetAllQuery : IRequest<IEnumerable<Abonne>>
{
}