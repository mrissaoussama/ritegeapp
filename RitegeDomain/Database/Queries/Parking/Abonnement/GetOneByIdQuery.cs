using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.Parking.AbonnementQueries;

public class GetOneByIdQuery : IRequest<Abonnement>
{
    public long Id { get; set; }

}