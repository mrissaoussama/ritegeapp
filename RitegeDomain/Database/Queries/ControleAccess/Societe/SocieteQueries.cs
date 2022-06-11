using AutoMapper;
using MediatR;
using RitegeDomain.Database;
namespace RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByNameQuery : IRequest<Societe>
{
    public string Name { get; set; }

}

public class GetOneByIdQuery : IRequest<Societe>
{
    public int IdSociete { get; set; }

}
public class GetOneByIdParkingQuery : IRequest<Societe>
{
    public int IdParking { get; set; }

}
public class GetOneByIdDoorQuery : IRequest<Societe>
{
    public int IdDoor { get; set; }

}
public class GetCompanyEarningsByIdQuery : IRequest<decimal>
{
    public int IdSociete { get; set; }

}