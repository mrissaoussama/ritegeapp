using Microsoft.Data.SqlClient;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private string connectionString;

        public async Task<IEnumerable<Ticket>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end)
        {
            List<Ticket> Tickets = new List<Ticket>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (id is null or 0)
                    query = "SELECT * FROM parkingdb.Ticket where dateHeureDebutStationnement>=@start and dateHeureFinStationnement<=@end";
                else
                    query = "SELECT * FROM parkingdb.Ticket where IdTicket=@IdTicket and dateHeureDebutStationnement>=@start and dateHeureFinStationnement<=@end";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    if (!(id is null or 0))
                        cmd.Parameters.Add("@IdTicket", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@end", SqlDbType.DateTime2).Value = end;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Tickets.Add(new Ticket
                            {
                                IdTicket = Convert.ToInt32(sdr["IdTicket"]),
                                DateHeureDebutStationnement = Convert.ToDateTime(sdr["DateHeureDebutStationnement"]),
                                DateHeureFinStationnement = Convert.ToDateTime(sdr["DateHeureFinStationnement"]),
                                EtatTicket = Convert.ToString(sdr["EtatTicket"]),
                                IdTarifTicket = Convert.ToInt32(sdr["IdTarifTicket"]),
                                idBorneEntree = Convert.ToInt32(sdr["idBorneEntree"]),
                                idBorneSortie = Convert.ToInt32(sdr["idBorneSortie"]),
                                AvecTarif2 = Convert.ToBoolean(sdr["AvecTarif2"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Tickets;
        }


        public TicketRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
