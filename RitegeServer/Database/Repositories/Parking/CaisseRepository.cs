using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RitegeDomain.Database.Entities.ParkingEntities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RitegeDomain.Database.Repositories
{
    public class CaisseRepository : GenericRepository<Caisse>, ICaisseRepository
    {
        private string connectionString;

        public IConfiguration _configuration;
        public CaisseRepository(IConfiguration config)
        {
            _configuration = config;
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<IEnumerable< Caisse>> GetAllByIdParkingAsync(int id)
        {
            List<Caisse> Caisses = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Caisse where IdParking=@IdParking";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdParking", SqlDbType.Int).Value = id;
                    

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Caisses.Add(new Caisse
                            {
                                IdCaisse = Convert.ToInt32(sdr["IdCaisse"]),
                                NomCaisse = Convert.ToString(sdr["nomCaisse"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),
                             
                                Sync = Convert.ToInt16(sdr["Sync"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Caisses;
        }

        public async Task<Caisse> GetOneByNameAsync(string name)
        {
            Caisse Caisse = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Caisse where nomCaisse=@name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Caisse = new Caisse
                            {
                                IdCaisse = Convert.ToInt32(sdr["IdCaisse"]),
                                NomCaisse = Convert.ToString(sdr["nomCaisse"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),

                                Sync = Convert.ToInt16(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Caisse;
        }
        public async Task<Caisse> GetOneByIdAsync(int id)
        {
            Caisse Caisse = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Caisse where idCaisse=@id";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Caisse = new Caisse
                            {
                                IdCaisse = Convert.ToInt32(sdr["IdCaisse"]),
                                NomCaisse = Convert.ToString(sdr["nomCaisse"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),

                                Sync = Convert.ToInt16(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Caisse;
        }


    }
}
