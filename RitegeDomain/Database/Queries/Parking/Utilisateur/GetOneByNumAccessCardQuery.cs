using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;

public class GetOneByNumAccessCardQuery : IRequest<Utilisateur>
{
    public string Number { get; set; }

}