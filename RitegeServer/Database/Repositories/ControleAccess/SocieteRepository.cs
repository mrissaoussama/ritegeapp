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
