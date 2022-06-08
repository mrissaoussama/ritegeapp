using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private string connectionString;
        public ClientRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;
        }

        public async Task<Client> GetOneByIdAsync(int id)
        {
            Client client= new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Client where idclient=@id";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                           client=new Client
                            { IdClient = sdr.GetInt32("idclient"),
                                IdSociete = sdr.GetInt32("idsociete"),
                                Email = sdr.GetString("email"),
                                MotDePasse = sdr.GetString("motdepasse"),
                            } ;
                        }
                    }
                    con.Close();
                }
            }
            return client;
        }

        public async Task<Client> GetOneByEmailAndPasswordAsync(string email, string password)
        {
            Client client = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM dbo.Client where email=@email and motdepasse=@motdepasse";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@motdepasse", SqlDbType.NVarChar).Value = password;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            client = new Client
                            {
                                IdClient = sdr.GetInt32("idclient"),
                                IdSociete = sdr.GetInt32("idsociete"),
                                Email = sdr.GetString("email"),
                                MotDePasse = sdr.GetString("motdepasse"),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return client;
        }




    }
}
