using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Queries.ParkingDBQueries.AbonneQueries;

public class GetAllQuery : IRequest<IEnumerable<Abonne>>
{
}