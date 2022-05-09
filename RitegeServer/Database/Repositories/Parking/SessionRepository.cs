using Microsoft.Data.SqlClient;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private string connectionString;


        public async Task<IEnumerable<Session>> GetAllByCaisseAndDateAsync(long? id, DateTime start, DateTime end)
        {
            List<Session> Sessions = new List<Session>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (id is null or 0)
                    query = "SELECT * FROM parkingdb.Sessions where dateStart>=@start and dateEnd<=@end";
                else
                    query = "SELECT * FROM parkingdb.Sessions where idCaisse=@idCaisse and dateStart>=@start and dateEnd<=@end";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@end", SqlDbType.DateTime2).Value = end;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Sessions.Add(new Session
                            {
                                IdSessions = Convert.ToInt32(sdr["IdSessions"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),
                                dateStart = Convert.ToDateTime(sdr["dateStart"]),
                                dateEnd = Convert.ToDateTime(sdr["dateEnd"]),
                                Montant = Convert.ToDecimal(sdr["Montant"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Sessions;
        }


        public async Task<IEnumerable<Session>> GetAllByIdAndDateAsync(long? id, DateTime start, DateTime end)
        {
            List<Session> Sessions = new List<Session>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (id is null or 0)
                    query = "SELECT * FROM parkingdb.Sessions where  dateStart>=@start and dateEnd<=@end";

                else
                    query = "SELECT * FROM parkingdb.Sessions where IdSessions=@IdSessions and dateStart>=@start and dateEnd<=@end";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdSessions", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@end", SqlDbType.DateTime2).Value = end;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Sessions.Add(new Session
                            {
                                IdSessions = Convert.ToInt32(sdr["IdSessions"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),
                                dateStart = Convert.ToDateTime(sdr["dateStart"]),
                                dateEnd = Convert.ToDateTime(sdr["dateEnd"]),
                                Montant = Convert.ToDecimal(sdr["Montant"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Sessions;
        }


        public SessionRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
