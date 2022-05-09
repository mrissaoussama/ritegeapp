using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class LoginQuery : IRequest<string?>
{
    public string Login { get; set; }
    public string MotDePasse { get; set; }

}