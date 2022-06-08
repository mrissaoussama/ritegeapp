using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByAbonneIdQuery : IRequest<IEnumerable<Affectationabonnement>>
{
    public long AbonneID { get; set; }
}