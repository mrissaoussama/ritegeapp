using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class GetOneByNumAccessCardQuery : IRequest<Utilisateur>
{
    public string Number { get; set; }

}