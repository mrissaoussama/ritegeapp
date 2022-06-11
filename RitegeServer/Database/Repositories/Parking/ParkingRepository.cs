using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RitegeDomain.Database.Repositories
{
    public class ParkingRepository : GenericRepository<Parking>, IParkingRepository
    {
        private string connectionString;

        public IConfiguration _configuration;
        public ParkingRepository(IConfiguration config)
        {
            _configuration = config;
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<Parking> GetOneByIdParkingAsync(int id)
        {
            Parking Parking = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Parking where IdParking=@IdParking";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdParking", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Parking = new Parking
                            {
                                IdParking = Convert.ToInt32(sdr["IdParking"]),
                                NomParking = Convert.ToString(sdr["NomParking"]),
                                FluxPayment = Convert.ToString(sdr["FluxPayment"]),
                                CapaciteParking = Convert.ToInt32(sdr["CapaciteParking"]),
                                IdSociete = Convert.ToInt32(sdr["IdSociete"]),
                                PlacesOccupees = Convert.ToInt32(sdr["PlacesOccupees"]),
                                DateHeureSauvegarde = Convert.ToDateTime(sdr["DateHeureSauvegarde"]),
                                
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Parking;
        }

        public async Task<Parking> GetOneByNomParkingAsync(string nomParking)
        

            {
                Parking Parking = new();
                using (SqlConnection con = new(connectionString))
                {
                    string query;
                    query = "SELECT * FROM dbo.Parking where NomParking=@NomParking";
                    using (SqlCommand cmd = new(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@NomParking", SqlDbType.Int).Value = nomParking;


                        con.Open();
                        using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await sdr.ReadAsync())
                            {
                                Parking = new Parking
                                {
                                    IdParking = Convert.ToInt32(sdr["IdParking"]),
                                    NomParking = Convert.ToString(sdr["NomParking"]),
                                    FluxPayment = Convert.ToString(sdr["FluxPayment"]),
                                    CapaciteParking = Convert.ToInt32(sdr["CapaciteParking"]),
                                    IdSociete = Convert.ToInt32(sdr["IdSociete"]),
                                    PlacesOccupees = Convert.ToInt32(sdr["PlacesOccupees"]),
                                    DateHeureSauvegarde = Convert.ToDateTime(sdr["DateHeureSauvegarde"]),

                                };
                            }
                        }
                        con.Close();
                    }
                }
                return Parking;
            }

            public async Task<IEnumerable<Parking>> GetAllByIdSocieteAsync(int idSociete)

        {
            List<Parking> Parkings = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Parking where idSociete=@idSociete";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idSociete", SqlDbType.Int).Value = idSociete;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Parkings.Add(new Parking
                            {
                                IdParking = Convert.ToInt32(sdr["IdParking"]),
                                NomParking = Convert.ToString(sdr["NomParking"]),
                                FluxPayment = Convert.ToString(sdr["FluxPayment"]),
                                CapaciteParking = Convert.ToInt32(sdr["CapaciteParking"]),
                                IdSociete = Convert.ToInt32(sdr["IdSociete"]),
                                PlacesOccupees = Convert.ToInt32(sdr["PlacesOccupees"]),
                                DateHeureSauvegarde = Convert.ToDateTime(sdr["DateHeureSauvegarde"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return Parkings;
        }

        public  async Task<decimal> GetParkingEarningsByIdAsync(int idParking)
        { decimal total=0;
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT Sum(montant) as montantparking from parkingdb.sessions where dateDebut>=@dateStart and (DateFin is NULL or DateFin<=@dateEnd) and idCaisse in(select idCaisse from parkingdb.caisse where idparking=@idParking)";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = idParking;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime).Value = DateTime.Today.AddDays(1).AddTicks(-1);


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            if (sdr["montantparking"] !=DBNull.Value)
                            total = Convert.ToDecimal(sdr["montantparking"]);
                          
                        }
                    }
                    con.Close();
                }
            }
            return total;
        }
    }
}
