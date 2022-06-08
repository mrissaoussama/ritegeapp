using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;

public class LoginQuery : IRequest<string?>
{
    public string Login { get; set; }
    public string MotDePasse { get; set; }

}