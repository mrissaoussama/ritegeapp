using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

using RitegeDomain.Database.Entities.Parking;
using RitegeDomain.Database.Entities.ParkingEntities;



public class GetAllByIdSociete : IRequest<IEnumerable<Parking>>
{
    public int IdSociete { get; set; }

}
public class GetOneByIdParkingQuery : IRequest<Parking>
{
    public int IdParking { get; set; }

}
public class GetOneByNomParkingQuery : IRequest<Parking>
{
    public string NomParking { get; set; }

}
public class GetParkingEarningsByIdQuery : IRequest<decimal>
{
    public int IdParking { get; set; }

}