using RitegeDomain.DTO;

namespace RitegeDomain.Database.IRepositories;
public interface IDashboardDTORepository : IRepository<DashBoardDTO>
{
    public Task<DashBoardDTO> GetByIdParkingAndIdCashRegister(int idParking, int idCaisse);

}