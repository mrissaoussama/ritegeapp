using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByAbonneIdQuery : IRequest<IEnumerable<Affectationabonnement>>
{
    public long AbonneID { get; set; }
}