using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AbonneQueries;

public class GetOneByIdQuery : IRequest<Abonne>
{
    public long Id { get; set; }

}