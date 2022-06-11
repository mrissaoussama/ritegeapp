using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class SocieteRepository : GenericRepository<Societe>, ISocieteRepository
    {
        private string connectionString;
        public SocieteRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;
        }

        public async Task<decimal> GetCompanyEarningsByIdAsync(int idSociete)
        {
            decimal total = 0;
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT Sum(montant) as montantsociete from parkingdb.sessions where dateDebut>=@dateStart and (DateFin is NULL or DateFin<=@dateEnd) and idCaisse in(select idCaisse from parkingdb.caisse where idparking in (select idparking from dbo.parking where IdSociete=@idSociete))";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = idSociete;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime).Value = DateTime.Today.AddDays(1).AddTicks(-1);


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            total = Convert.ToDecimal(sdr["montantsociete"]);

                        }
                    }
                    con.Close();
                }
            }
            return total;
        }

        public async Task<Societe> GetOneByIdAsync(int id)
        {
            Societe Societe= new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Societe where idsociete=@id";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                           Societe=new Societe
                            { IdSociete = sdr.GetInt32("idSociete"),
                               NomSociete = sdr.GetString("NomSociete"),
                            } ;
                        }
                    }
                    con.Close();
                }
            }
            return Societe;
        }
        public async Task<Societe> GetOneByIdParkingAsync(int idParking)
        {
            Societe Societe = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT *  FROM [controleaccessdb].[dbo].[societe] where idsociete=(SELECT idsociete FROM [parkingdb].[dbo].[parking] where idparking=@idParking)";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = idParking;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Societe = new Societe
                            {
                                IdSociete = sdr.GetInt32("idSociete"),
                                NomSociete = sdr.GetString("NomSociete"),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Societe;
        }
        public async Task<Societe> GetOneByIdDoorAsync(int idDoor)
        {
            Societe Societe = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT *  FROM [controleaccessdb].[dbo].[societe] s where s.idsociete = (SELECT idsociete FROM parkingdb.dbo.parking p where idparking = (select idparking from[controleaccessdb].[controleaccessdb].door where iddoor = @idDoor))";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idDoor", SqlDbType.Int).Value = idDoor;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Societe = new Societe
                            {
                                IdSociete = sdr.GetInt32("idSociete"),
                                NomSociete = sdr.GetString("NomSociete"),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Societe;
        }

        public async Task<Societe> GetOneByNameAsync(string name)
        {
            Societe Societe = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Societe where nomsociete=@name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Societe = new Societe
                            {
                                IdSociete = sdr.GetInt32("idSociete"),
                                NomSociete = sdr.GetString("NomSociete"),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Societe;
        }




    }
}
