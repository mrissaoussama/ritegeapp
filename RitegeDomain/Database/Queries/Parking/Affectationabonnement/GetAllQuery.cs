using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllQuery : IRequest<IEnumerable<Affectationabonnement>>
{
}