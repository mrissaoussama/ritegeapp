global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
using RitegeDomain.Database.Entities.Parking;
using RitegeDomain.Database.Entities.ParkingEntities;


namespace RitegeDomain.Database.IRepositories;
public interface ITicketRepository : IRepository<Ticket>
{

    public Task<IEnumerable<Ticket>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end);
    public Task<int> GetTodaysTicketsAsync(int IdParking,int idCaisse);
    public Task<Ticket> Add(DateTime dateHeureDebutStationnement, DateTime? dateHeureFinStationnement, string etatTicket, int? idTarifTicket, decimal? Tarif, int idBorneEntree,int? idBorneSortie, string logCaissier, bool? avectarif2);
 
}