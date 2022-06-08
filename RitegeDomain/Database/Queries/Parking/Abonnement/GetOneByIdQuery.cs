using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.AbonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetOneByIdQuery : IRequest<Abonnement>
{
    public long Id { get; set; }

}