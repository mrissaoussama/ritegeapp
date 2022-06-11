using AutoMapper;
using MediatR;
using RitegeDomain.DTO;

namespace RitegeDomain.Database.Queries;

public class DashboardDTOQuery : IRequest<DashBoardDTO>
{
    public int IdParking { get; set; }
    public int IdCaisse { get; set; }

}