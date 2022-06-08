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
        public async Task<string?> Login(string login, string motdepasse)
        {

            Utilisateur Utilisateur = await GetOneByLoginAndMotDePasseAsync(login, motdepasse);
            if (Utilisateur is not null && Utilisateur.IdUtilisateur!=0)
            {
                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("IdUtilisateur", Utilisateur.IdUtilisateur.ToString()),
                        new Claim("Login", Utilisateur.Login),
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:Expires"])),
                    signingCredentials: signIn);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else return null;
        }

    }
}
