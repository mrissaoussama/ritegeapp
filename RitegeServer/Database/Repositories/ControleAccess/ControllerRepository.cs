using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class ControllerRepository : GenericRepository<Controller>, IControllerRepository
    {
        private string connectionString;
        public ControllerRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;
        }

        public async Task<List<Controller>> GetAllAsync()
        {
            List<Controller> Controllers = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Controllers";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Controllers.Add(new Controller
                            {
                                Code = Convert.ToInt16(sdr["Code"]),
                                Idnode = Convert.ToInt16(sdr["Idnode"]),
                                IdSite = Convert.ToInt16(sdr["IdSite"]),
                                Stat = Convert.ToInt32(sdr["Stat"]),
                                Port = Convert.ToInt16(sdr["Port"]),
                                Ipadr = Convert.ToString(sdr["Ipadr"]),
                                Macadr = Convert.ToString(sdr["Macadr"]),
                                ModelName = Convert.ToString(sdr["ModelName"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Controllers;
        }

        public async Task<Controller> GetOneByIdAsync(long id)
        {
            Controller Controller = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Controllers where Code=@Code";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Controller = new Controller
                            {
                                Code = Convert.ToInt16(sdr["Code"]),
                                Idnode = Convert.ToInt16(sdr["Idnode"]),
                                IdSite = Convert.ToInt16(sdr["IdSite"]),
                                Stat = Convert.ToInt32(sdr["Stat"]),
                                Port = Convert.ToInt16(sdr["Port"]),
                                Ipadr = Convert.ToString(sdr["Ipadr"]),
                                Macadr = Convert.ToString(sdr["Macadr"]),
                                ModelName = Convert.ToString(sdr["ModelName"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Controller;
        }




    }
}
