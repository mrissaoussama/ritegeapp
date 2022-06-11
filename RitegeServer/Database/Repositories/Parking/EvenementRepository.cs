using Microsoft.Data.SqlClient;
using System.Data;
using RitegeServer;
namespace RitegeDomain.Database.Repositories
{
    using RitegeDomain.Database.Entities.ParkingEntities;

    public class EvenementRepository : GenericRepository<Evenement>, IEvenementRepository
    {
        private string connectionString;
        public EvenementRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<IEnumerable<Evenement>> GetAllByBorneAndDateAsync(long id, DateTime date, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT * FROM parkingdb.Evenement where idBorne=@idBorne and dateevent between @date and @dateend";
                else
                    query = "SELECT * FROM parkingdb.Evenement where idBorne=@idBorne and dateevent between @date and @dateend and typeevent " + AlertString.GetAlertSqlString();
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idBorne", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = date.Date;
                    cmd.Parameters.Add("@dateend", SqlDbType.DateTime2).Value = date.Date.AddDays(1).AddTicks(-1);

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<IEnumerable<Evenement>> GetAllByCaisseAndDateAsync(long id, DateTime date, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT * FROM parkingdb.Evenement where idCaisse=@idCaisse and  dateevent between @date and @dateend";
                else
                    query = "SELECT * FROM parkingdb.Evenement where idCaisse=@idCaisse and dateevent between @date and @dateend and typeevent " + AlertString.GetAlertSqlString();
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = date;
                    cmd.Parameters.Add("@dateend", SqlDbType.DateTime2).Value = date.Date.AddDays(1).AddTicks(-1);


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<IEnumerable<Evenement>> GetAllByCaisseAndBorneAndDateAsync(long idCaisse, long idBorne, DateTime date, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT * FROM parkingdb.Evenement where idBorne=@idBorne and idCaisse=@idCaisse and  dateevent between @date and @dateend";
                else
                    query = "SELECT * FROM parkingdb.Evenement where idBorne=@idBorne and idCaisse=@idCaisse and dateevent between @date and @dateend and typeevent " + AlertString.GetAlertSqlString();

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;
                    cmd.Parameters.Add("@idBorne", SqlDbType.Int).Value = idBorne;
                    cmd.Parameters.Add("@dateend", SqlDbType.DateTime2).Value = date.Date.AddDays(1).AddTicks(-1);

                    cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = date;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<IEnumerable<Evenement>> GetAllByDateAsync(DateTime date, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT * FROM parkingdb.Evenement where dateevent between @date and @dateend";
                else
                {
                    var alertCodes = AlertString.GetAlertSqlString();
                    query = "SELECT * FROM parkingdb.Evenement where dateevent between @date and @dateend and typeevent " + alertCodes;
                }
                using (SqlCommand cmd = new(query))
                {var dateend = date.Date.AddDays(1).AddTicks(-1); 
                    cmd.Connection = con;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = date;
                    cmd.Parameters.Add("@dateend", SqlDbType.DateTime2).Value = dateend;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<IEnumerable<Evenement>> GetLast10ByBorneAsync(long id, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where idBorne=@idBorne order by dateEvent desc";
                else
                { var alertCodes = AlertString.GetAlertSqlString();
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where idBorne=@idBorne order by dateEvent desc and typeevent " + alertCodes;
                } using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idBorne", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }


        public async Task<IEnumerable<Evenement>> GetLast10ByCaisseAndBorneAsync(long idCaisse, long idBorne, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where idBorne=@idBorne and idCaisse=@idCaisse order by dateEvent desc";
                else
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where idBorne=@idBorne and typeevent " + AlertString.GetAlertSqlString() + " order by dateEvent desc";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idBorne", SqlDbType.Int).Value = idBorne;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<IEnumerable<Evenement>> GetLast10ByCaisseAsync(long idCaisse, bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where  idCaisse=@idCaisse order by dateEvent desc";
                else
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where  idCaisse=@idCaisse and typeevent " + AlertString.GetAlertSqlString() + " order by dateEvent desc";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }
        public async Task<IEnumerable<Evenement>> GetLast10Async(bool AlertsOnly)
        {
            List<Evenement> Evenements = new List<Evenement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (AlertsOnly == false)
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement  order by dateEvent desc";
                else
                    query = "SELECT TOP(10) * FROM parkingdb.Evenement where  typeevent " + AlertString.GetAlertSqlString() + " order by dateEvent desc";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Evenements.Add(new Evenement
                            {
                                IdEvenement = Convert.ToInt64(sdr["IdEvenement"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DescriptionEvent = Convert.ToString(sdr["DescriptionEvent"]),
                                TypeEvent = Convert.ToString(sdr["TypeEvent"]),
                                idCaisse = Convert.ToInt32(sdr["idCaisse"]),
                                idBorne = Convert.ToInt32(sdr["idBorne"]),
                                LogCaissier = Convert.ToString(sdr["LogCaissier"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Evenements;
        }

        public async Task<int> Add(DateTime dateEvent, string descriptionEvent, string typeEvent, int? idCaisse, int? idBorne, string logCaissier)
        {
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "insert into parkingdb.Evenement(dateEvent,descriptionEvent,typeEvent,idCaisse,idBorne,logCaissier) values(@dateEvent,@descriptionEvent,@typeEvent,@idCaisse,@idBorne,@logCaissier)";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dateEvent", SqlDbType.DateTime2).Value = dateEvent;
                    cmd.Parameters.Add("@descriptionEvent", SqlDbType.NVarChar).Value = descriptionEvent;
                    cmd.Parameters.Add("@logCaissier", SqlDbType.NVarChar).Value = logCaissier;
                    cmd.Parameters.Add("@typeEvent", SqlDbType.NVarChar).Value = typeEvent;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;
                    cmd.Parameters.Add("@idBorne", SqlDbType.Int).Value = idBorne;
                    con.Open();
                   int rowsaffected= await cmd.ExecuteNonQueryAsync();
                    con.Close();
                    return rowsaffected;
                }
            }
        }

        public async Task<int> GetTodayAdministrateurAsync(int idParking, int idCaisse)
        {
            int total = 0;
            using (SqlConnection con = new(connectionString))
            {
                string query;


                query = "SELECT  count ( typeevent) as total,typeEvent from parkingdb.evenement  where typeevent like '%abonne%' and idCaisse =@idCaisse and dateevent between @dateStart and @dateEnd group by typeEvent";
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

                            total = Convert.ToInt32(sdr["total"]);
                        }
                    }
                    con.Close();
                }
            }
            return total;
        }

        public async Task<int> GetTodayAuthoriteAsync(int idParking, int idCaisse)
        {
            int total = 0;
            using (SqlConnection con = new(connectionString))
            {
                string query;


                query = "SELECT  count ( typeevent) as total,typeEvent from parkingdb.evenement  where typeevent like '%Authority%' and idCaisse =@idCaisse and dateevent between @dateStart and @dateEnd group by typeEvent";
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

                            total = Convert.ToInt32(sdr["total"]);
                        }
                    }
                    con.Close();
                }
            }
            return total;
        }

        public async Task<int> GetTodayAbonneAsync(int idParking, int idCaisse)
        {
            int total = 0;
            using (SqlConnection con = new(connectionString))
            {
                string query;


                query = "SELECT  count ( typeevent) as total,typeEvent from parkingdb.evenement  where typeevent like '%Personnel%' and idCaisse =@idCaisse and dateevent between @dateStart and @dateEnd group by typeEvent";
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

                            total = Convert.ToInt32(sdr["total"]);
                        }
                    }
                    con.Close();
                }
            }
            return total;
        }
    }
}
