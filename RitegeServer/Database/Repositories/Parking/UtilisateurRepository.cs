using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RitegeDomain.Database.Entities.ParkingEntities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RitegeDomain.Database.Repositories
{
    public class UtilisateurRepository : GenericRepository<Utilisateur>, IRepositories.IUtilisateurRepository
    {
        private string connectionString;

        public IConfiguration _configuration;
        public UtilisateurRepository(IConfiguration config)
        {
            _configuration = config;
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<Utilisateur> GetOneByIdAsync(long id)
        {
            Utilisateur Utilisateur = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Utilisateur where IdUtilisateur=@IdUtilisateur";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IdUtilisateur", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateur = new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateur;
        }

        public async Task<Utilisateur> GetOneByLoginAndMotDePasseAsync(string login, string motdepasse)
        {

            Utilisateur Utilisateur = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Utilisateur where login=@login" +
                    " and motdepasse=@password";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = motdepasse;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateur = new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateur;
        }

        public async Task<Utilisateur> GetOneByNumAccessCardAsync(string number)
        {
            Utilisateur Utilisateur = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Utilisateur where numaccesscard=@numaccesscard";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@numaccesscard", SqlDbType.NVarChar).Value = number;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateur = new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateur;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllByIdParkingAsync(int id)
        {
            List<Utilisateur> Utilisateurs = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "select * from parkingdb.utilisateur where login in(select logcaissier from parkingdb.sessions where idCaisse in(select idcaisse from parkingdb.caisse c where IdParking = @idParking)) ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateurs.Add(new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateurs;
        }

        public async Task<Utilisateur> GetOneByLoginAsync(string login)
        {

            Utilisateur Utilisateur = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM parkingdb.Utilisateur where login=@login";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateur = new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateur;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllByIdSocieteAsync(int id)
        {
            List<Utilisateur> Utilisateurs = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "select * from parkingdb.utilisateur where login in(select logcaissier from parkingdb.sessions where idCaisse in(select idcaisse from parkingdb.caisse c where IdParking in(select IdParking from dbo.parking where idsociete=@idSociete)))  ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idSociete", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Utilisateurs.Add(new Utilisateur
                            {
                                IdUtilisateur = Convert.ToInt32(sdr["IdUtilisateur"]),
                                Nom = Convert.ToString(sdr["Nom"]),
                                Prenom = Convert.ToString(sdr["Prenom"]),
                                Login = Convert.ToString(sdr["Login"]),
                                MotDePasse = Convert.ToString(sdr["MotDePasse"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Utilisateurs;
        }
    }
}
