using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class GetOneByIdQuery : IRequest<Utilisateur>
{
    public long Id { get; set; }

}