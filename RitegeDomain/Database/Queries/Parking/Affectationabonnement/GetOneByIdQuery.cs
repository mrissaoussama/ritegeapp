using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetOneByIdQuery : IRequest<Affectationabonnement>
{
    public long Id { get; set; }

}