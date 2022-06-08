using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Repositories
{
    public class BorneRepository : GenericRepository<Borne>, IBorneRepository
    {
        private string connectionString;

        public IConfiguration _configuration;
        public BorneRepository(IConfiguration config)
        {
            _configuration = config;
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<IEnumerable< Borne>> GetAllByIdParkingAsync(int id)
        {
            List<Borne> Bornes = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Borne where IdParking=@IdParking";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdParking", SqlDbType.Int).Value = id;
                    

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Bornes.Add(new Borne
                            {
                                IdBorne = Convert.ToInt32(sdr["IdBorne"]),
                                NomBorne = Convert.ToString(sdr["nomBorne"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),
                             
                                Sync = Convert.ToInt16(sdr["Sync"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Bornes;
        }

        public async Task<Borne> GetOneByNameAsync(string name)
        {
            Borne Borne = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Borne where nomBorne=@name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Borne = new Borne
                            {
                                IdBorne = Convert.ToInt32(sdr["IdBorne"]),
                                NomBorne = Convert.ToString(sdr["nomBorne"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),

                                Sync = Convert.ToInt16(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Borne;
        }
        public async Task<Borne> GetOneByIdAsync(int id)
        {
            Borne Borne = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Borne where idBorne=@id";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Borne = new Borne
                            {
                                IdBorne = Convert.ToInt32(sdr["IdBorne"]),
                                NomBorne = Convert.ToString(sdr["nomBorne"]),
                                Flux = Convert.ToString(sdr["Flux"]),
                                IdParking = Convert.ToInt32(sdr["IdParking"]),

                                Sync = Convert.ToInt16(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Borne;
        }



    }
}
