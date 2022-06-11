using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ParkingEntities;
using System.Data;
using System.Diagnostics;

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

        public async Task<Session> GetCurrentSessionAsync(int idParking, int idCaisse)
        {
            Session session = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
           
                    query = "SELECT * from parkingdb.sessions where dateDebut>=@today and (DateFin<=@dateEnd or DateFin is NULL) and idCaisse=@idCaisse";
                using (SqlCommand cmd = new(query))
                {
                    var today = DateTime.Today;
                    var dateend=DateTime.Today.AddDays(1).AddTicks(-1);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@today", SqlDbType.DateTime2).Value = today;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = dateend;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            session=new Session
                            {
                                IdSessions = Convert.ToInt32(sdr["IdSessions"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),
                                dateStart = Convert.ToDateTime(sdr["dateDebut"]),

                            };
                            if (sdr["DateFin"] != DBNull.Value)
                                session.dateEnd = Convert.ToDateTime(sdr["DateFin"]);
                            if (sdr["Montant"] != DBNull.Value)
                                session.Montant = Convert.ToDecimal(sdr["Montant"]);
                        }
                    }
                    con.Close();
                }
            }
            return session;
        }

        public async Task<Session> GetOneByTicketLogCaissierAndTicketDatesAsync(string logCaissier, DateTime dateStart, DateTime? dateEnd)
        {
            Session session = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;

                query = "SELECT top 1 * from parkingdb.sessions s where s.logCaissier=(select top 1 logCaissier from parkingdb.ticket t where s.logCaissier=@logCaissier and t.dateHeureDebutStationnement = @dateStart and t.dateHeureFinStationnement =  @dateEnd) and dateDebut<= @dateStart and(DateFin >= @dateEnd or DateFin is null)";
                using (SqlCommand cmd = new(query))
                {
                    var today = DateTime.Today;
                    var dateend = DateTime.Today.AddDays(1).AddTicks(-1);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = dateStart;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = dateEnd;
                    cmd.Parameters.Add("@logCaissier", SqlDbType.NVarChar).Value = logCaissier;
                    Debug.WriteLine("SELECT top 1 * from parkingdb.sessions s where s.logCaissier=(select top 1 logCaissier from parkingdb.ticket t where t.logCaissier='{0}' and t.dateHeureDebutStationnement = '{1}' and t.dateHeureFinStationnement = '{2}') and dateDebut<= '{1}' and(DateFin >= '{2}' or DateFin is null)", logCaissier,dateStart,dateEnd);

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            session = new Session
                            {
                                IdSessions = Convert.ToInt32(sdr["IdSessions"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),
                                dateStart = Convert.ToDateTime(sdr["dateDebut"]),

                            };
                            if (sdr["DateFin"] != DBNull.Value)
                                session.dateEnd = Convert.ToDateTime(sdr["DateFin"]);
                            if (sdr["Montant"] != DBNull.Value)
                                session.Montant = Convert.ToDecimal(sdr["Montant"]);
                        }
                    }
                    con.Close();
                }
            }
            return session;
        }

        public async Task<int> UpdateSessionEarningsByIdAsync(int Idsessions, decimal? pricetoAdd)
        {
            Session session = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "update parkingdb.sessions  set montant =case when montant is null then @pricetoadd else montant + @pricetoadd end where idSessions = @idsessions";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@Idsessions", SqlDbType.Int).Value = Idsessions;
                    cmd.Parameters.Add("@pricetoAdd", SqlDbType.Decimal).Value = pricetoAdd;
                    
                    con.Open();
                    //should be always 1
                    var affectedqueriesawait =cmd.ExecuteNonQueryAsync();
                    con.Close();

                    return 1;
                }
            }
        }

        public SessionRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
