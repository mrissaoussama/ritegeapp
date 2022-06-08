using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;

using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;

public class GetAllQuery : IRequest<IEnumerable<Affectationabonnement>>
{
}