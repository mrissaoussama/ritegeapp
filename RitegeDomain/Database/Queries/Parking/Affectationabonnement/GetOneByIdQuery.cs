using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;

public class GetOneByIdQuery : IRequest<Affectationabonnement>
{
    public long Id { get; set; }

}