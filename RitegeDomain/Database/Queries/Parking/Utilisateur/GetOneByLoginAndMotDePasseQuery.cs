using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class GetOneByLoginAndMotDePasseQuery : IRequest<Utilisateur>
{
    public string Login { get; set; }
    public string MotDePasse { get; set; }

}