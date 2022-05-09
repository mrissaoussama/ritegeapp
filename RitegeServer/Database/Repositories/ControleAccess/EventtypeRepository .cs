using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class EventtypeRepository : GenericRepository<Eventtype>, IEventtypeRepository
    {
        private string connectionString;
        public EventtypeRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;

        }

        public async Task<List<Eventtype>> GetAllAsync()
        {
            List<Eventtype> Eventtypes = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Eventtype";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Eventtypes.Add(new Eventtype
                            {
                                CodeTypeEvent = Convert.ToInt16(sdr["CodeTypeEvent"]),
                                DescEvent = Convert.ToString(sdr["DescEvent"]),
                                WithUser = Convert.ToBoolean(sdr["WithUser"]),
                                TypeAcces = Convert.ToString(sdr["TypeAcces"]),
                                KeyTypeEvent = Convert.ToString(sdr["KeyTypeEvent"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Eventtypes;
        }

        public async Task<Eventtype> GetOneByIdAsync(long id)
        {
            Eventtype Eventtype = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Eventtype where CodeTypeEvent=@CodeTypeEvent";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@CodeTypeEvent", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Eventtype = new Eventtype
                            {
                                CodeTypeEvent = Convert.ToInt16(sdr["CodeTypeEvent"]),
                                DescEvent = Convert.ToString(sdr["DescEvent"]),
                                WithUser = Convert.ToBoolean(sdr["WithUser"]),
                                TypeAcces = Convert.ToString(sdr["TypeAcces"]),
                                KeyTypeEvent = Convert.ToString(sdr["KeyTypeEvent"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Eventtype;
        }




    }
}
