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
                query = "SELECT * FROM controleaccessdb.Door where iddoor=@IdDoor";
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
                                IdParking = Convert.ToInt16(sdr["IdParking"]),

                            };
                        }
                    }
                    con.Close();
                }
            }
            return Door;
        }
        public async Task<Door> GetOneByIdSocieteAsync(long id)
        {
            Door Door = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Door where idparking in (select idparking from parkingdb.dbo.parking where idsociete=@id)";
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
                            IdParking =  Convert.ToInt16(sdr["IdParking"]),

                            };
                        }
                    }
                    con.Close();
                }
            }
            return Door;
        }

        public  async Task<IEnumerable<Door>> GetAllByIdParkingAsync(int idparking)
        {
            List<Door> Doors = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Door where idparking in (select idparking from parkingdb.dbo.parking where idsociete=@id)";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdDoor", SqlDbType.Int).Value = idparking;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Doors.Add(  new Door
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
                                IdParking = Convert.ToInt16(sdr["IdParking"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Doors;
        }

        public async Task<int> UpdateDoorStateByIdAsync(long id, bool state)
        {
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "UPDATE controleaccessdb.Door  SET Activated = @State where idDoor=@IdDoor";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdDoor", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@State", SqlDbType.Bit).Value = state;

                    con.Open();
                    //should be always 1
                    var affectedqueriesawait = await cmd.ExecuteNonQueryAsync();
                    con.Close();

                    return affectedqueriesawait;
                }
            }
        }

        public DoorRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;

        }
    }
}
