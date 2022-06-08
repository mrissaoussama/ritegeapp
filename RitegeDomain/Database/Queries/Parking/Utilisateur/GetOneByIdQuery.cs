using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries; using RitegeDomain.Database.Entities.ParkingEntities;


public class GetOneByIdQuery : IRequest<Utilisateur>
{
    public long Id { get; set; }

}