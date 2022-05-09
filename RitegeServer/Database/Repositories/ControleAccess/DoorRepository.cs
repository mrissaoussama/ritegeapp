using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class DoorRepository : GenericRepository<Door>, IDoorRepository
    {
        private string connectionString;



        public async Task<Door> GetOneByIdAsync(long id)
        {
            Door Door = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Door where IdDoor=@IdDoor";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdDoor", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Door = new Door
                            {
                                IdDoor = Convert.ToInt16(sdr["IdDoor"]),
                                DoorNumber = Convert.ToInt16(sdr["DoorNumber"]),
                                IdController = Convert.ToInt16(sdr["IdController"]),
                                IdPorte = Convert.ToInt16(sdr["IdPorte"]),
                                Canal = Convert.ToInt16(sdr["Canal"]),
                                IdSite = Convert.ToInt16(sdr["IdSite"]),
                                Stat = Convert.ToInt16(sdr["Stat"]),
                                DoorName = Convert.ToString(sdr["DoorName"]),
                                Flow = Convert.ToString(sdr["Flow"]),
                                TypeMateriel = Convert.ToString(sdr["TypeMateriel"]),
                                FonctionMateriel = Convert.ToString(sdr["FonctionMateriel"]),
                                HasSlaveReader = Convert.ToBoolean(sdr["HasSlaveReader"]),
                                Activated = Convert.ToBoolean(sdr["Activated"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Door;
        }



        public DoorRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;

        }
    }
}
