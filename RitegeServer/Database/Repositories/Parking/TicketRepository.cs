using Microsoft.Data.SqlClient;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    using RitegeDomain.Database.Entities.ParkingEntities;

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

        public async Task<Ticket> Add(DateTime dateHeureDebutStationnement, DateTime? dateHeureFinStationnement, string etatTicket, int? idTarifTicket, decimal? Tarif, int idBorneEntree, int? idBorneSortie, string logCaissier, bool? avectarif2)
        {
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "insert into parkingdb.Ticket(dateHeureDebutStationnement,dateHeureFinStationnement,etatTicket,idTarifTicket,Tarif,idBorneEntree,idBorneSortie,logCaissier,avectarif2) values(@dateHeureDebutStationnement,@dateHeureFinStationnement,@etatTicket,@idTarifTicket,@Tarif,@idBorneEntree,@idBorneSortie,@logCaissier,@avectarif2)";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dateHeureDebutStationnement", SqlDbType.DateTime2).Value = dateHeureDebutStationnement;
                    cmd.Parameters.Add("@dateHeureFinStationnement", SqlDbType.DateTime2).Value = dateHeureFinStationnement;
                    cmd.Parameters.Add("@etatTicket", SqlDbType.NVarChar).Value = etatTicket;
                    cmd.Parameters.Add("@Tarif", SqlDbType.Decimal).Value = Tarif;
                    cmd.Parameters.Add("@idTarifTicket", SqlDbType.Int).Value = idTarifTicket;
                    cmd.Parameters.Add("@idBorneEntree", SqlDbType.Int).Value = idBorneEntree;
                    cmd.Parameters.Add("@idBorneSortie", SqlDbType.Int).Value = idBorneSortie;
                    cmd.Parameters.Add("@logCaissier", SqlDbType.NVarChar).Value = logCaissier;
                    cmd.Parameters.Add("@avectarif2", SqlDbType.Int).Value = avectarif2;
                    con.Open();
                     await cmd.ExecuteNonQueryAsync();
                    con.Close();
                    var ticket = new Ticket
                    {
                        DateHeureDebutStationnement = dateHeureDebutStationnement,
                        DateHeureFinStationnement = dateHeureFinStationnement,
                        AvecTarif2 = avectarif2,
                        EtatTicket = etatTicket,
                        idBorneEntree = idBorneEntree,
                        idBorneSortie = idBorneSortie,
                        IdTarifTicket = idTarifTicket,
                        LogCaissier = logCaissier,
                        Tarif = Tarif
                    };
                    return ticket;
                }
            }
        }

        public async Task<int> GetTodaysTicketsAsync(int IdParking, int idCaisse)
        {
            int total = 0;
            using (SqlConnection con = new(connectionString))
            {
                string query;
            
                
                    query = "select  count(distinct idticket) as tickettotal from parkingdb.ticket t,parkingdb.sessions s where t.LogCaissier=s.logCaissier and t.dateHeureDebutStationnement >= @dateStart and(t.dateHeureFinStationnement <=@dateEnd or t.dateHeureFinStationnement is null) and t.logCaissier = (select top(1) logCaissier from parkingdb.sessions where idCaisse = @idCaisse)";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                        cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = DateTime.Today;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = DateTime.Today.AddDays(1).AddTicks(-1);

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            
                                total = Convert.ToInt32(sdr["tickettotal"]);
                        }
                    }
                    con.Close();
                }
            }
            return total;
        }

        public TicketRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
