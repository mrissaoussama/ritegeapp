global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;
using MediatR;
namespace RitegeDomain.Database.Queries.Parking.SessionQueries;

public class GetAllByIdAndDateQuery : IRequest<IEnumerable<Session>>
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}