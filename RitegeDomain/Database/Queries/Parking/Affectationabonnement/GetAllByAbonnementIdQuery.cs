using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByAbonnementIdQuery : IRequest<IEnumerable<Affectationabonnement>>
{
    public long AbonnementID { get; set; }
}