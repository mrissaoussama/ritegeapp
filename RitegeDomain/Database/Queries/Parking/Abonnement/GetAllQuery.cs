using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.AbonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllQuery : IRequest<IEnumerable<Abonnement>>
{
}