using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByIdAndDatesQuery : IRequest<IEnumerable<Affectationabonnement>>
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}