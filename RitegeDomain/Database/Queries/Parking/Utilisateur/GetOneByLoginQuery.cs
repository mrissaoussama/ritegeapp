using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;

public class GetOneByLoginQuery : IRequest<Utilisateur>
{
    public string Login { get; set; }

}