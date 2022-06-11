using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetTodayAdministrateurQuery : IRequest<int>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }
}
public class GetTodayAutoriteQuery : IRequest<int>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }
}
public class GetTodayAbonneQuery : IRequest<int>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }
}