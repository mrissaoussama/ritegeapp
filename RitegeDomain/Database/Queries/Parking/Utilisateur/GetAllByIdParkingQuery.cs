using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries; using RitegeDomain.Database.Entities.ParkingEntities;


public class GetAllByIdParkingQuery : IRequest<IEnumerable<Utilisateur>>
{
    public int IdParking { get; set; }

}
public class GetAllByIdSocieteQuery : IRequest<IEnumerable<Utilisateur>>
{
    public int IdSociete { get; set; }

}
