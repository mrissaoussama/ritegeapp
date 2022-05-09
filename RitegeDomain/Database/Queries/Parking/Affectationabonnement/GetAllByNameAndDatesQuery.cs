using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByNameAndDatesQuery : IRequest<IEnumerable<Affectationabonnement>>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}