using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AbonnementQueries;

public class GetAllQuery : IRequest<IEnumerable<Abonnement>>
{
}